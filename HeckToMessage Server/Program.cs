using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace ServerProgram
{
    //This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

    internal class Srv
    {
        private static readonly TcpListener Server = new TcpListener(IPAddress.Any, 7897);
        private static readonly List<TcpClient> Clients = new List<TcpClient>();
        private static Thread PerClientThread;
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions();
        private class AccDatas
        {
            public AccDatas(List<string> usernames, List<string> passwords, List<string> ips)
            {
                Usernames = usernames;
                Passwords = passwords;
                Ips = ips;
            }
            public List<string> Usernames { get; set; }
            public List<string> Passwords { get; set; }
            public List<string> Ips { get; set; }
        }

        private static string StripExtended(string arg)
        {
            StringBuilder buffer = new StringBuilder(arg.Length);
            foreach (char ch in arg)
            {
                ushort num = Convert.ToUInt16(ch);
                {
                    if ((num >= 32u))
                    {
                        buffer.Append(ch);
                    }
                }
            }
            return buffer.ToString();
        }



        private static void PerClient(object client)
        {
            TcpClient Client = (TcpClient)client;
            NetworkStream NS = Client.GetStream();
            byte[] Account_Buffer = new byte[1024];
            NS.Read(Account_Buffer, 0, 1024);
            string[] ACCOUT = StripExtended(Encoding.Unicode.GetString(Account_Buffer)).Split('¿');
            if (ACCOUT[0] == "DATAACCOUNT")
            {
                Console.WriteLine("User trying to login, username: " + ACCOUT[2] + " | Ip:" + ((IPEndPoint)Client.Client.RemoteEndPoint).Address.ToString());
                //check account in json
                string jsonf = File.ReadAllText(@"C:\Users\costin\AppData\Roaming\.HeckToMsg\Accounts.json");
                AccDatas data;
                data = new AccDatas(new List<string>(), new List<string>(), new List<string>());
                data = JsonSerializer.Deserialize<AccDatas>(jsonf);

                if (data.Usernames.Exists(e => e == ACCOUT[2]))
                {
                    Console.WriteLine("connection loginning in " + ACCOUT[2] + ". Ip: " + ((IPEndPoint)Client.Client.RemoteEndPoint).Address.ToString());
                    int index = data.Usernames.IndexOf(ACCOUT[2]);
                    if (data.Passwords.IndexOf(ACCOUT[1]) == index)
                    {
                        byte[] ERR_SUCCESS = Encoding.Unicode.GetBytes("ERR_SUCCESS");
                        NS.Write(ERR_SUCCESS, 0, ERR_SUCCESS.Length);
                        Console.WriteLine("User connected to account successfuly.");

                        string Username = ACCOUT[2];


                        GroupsData data1 = new GroupsData
                        {
                            Groups = new List<Group>(),
                            UsersDatas = new List<UserData>()
                        };
                        data1 = JsonSerializer.Deserialize<GroupsData>(File.ReadAllText(@"C:\Users\costin\AppData\Roaming\.HeckToMsg\GroupData.json"), options);
                        // Heck To Message
                        UserData userData = data1.UsersDatas.Find(e => e.User == Username);

                        //give joined groups
                        List<string> joinedgroups = new List<string>();
                        for (int i = 0; i < userData.JoinedGroups.Count; i++)
                        {
                            joinedgroups.Add(userData.JoinedGroups[i]);
                        }

                        //give public groups
                        List<Group> publicgroups = new List<Group>();
                        for (int i = 0; i < data1.Groups.Count; i++)
                        {
                            if (data1.Groups[i].IsPublic == true)
                            {
                                publicgroups.Add(data1.Groups[i]);
                            }
                        }

                        List<string> Ownedgroups = new List<string>();
                        for (int i = 0; i < data1.Groups.FindAll(e => e.Owner == Username).Count; i++)
                        {
                            Ownedgroups.Add(data1.Groups.FindAll(e => e.Owner == Username)[i].Name);
                            Ownedgroups.Add(data1.Groups.FindAll(e => e.Owner == Username)[i].UniqueID);
                        }

                        //send that info by first converting it to json
                        string json1 = JsonSerializer.Serialize(joinedgroups);
                        string json2 = JsonSerializer.Serialize(publicgroups);

                        byte[] JsonBuff1 = Encoding.Unicode.GetBytes(json1);
                        byte[] JsonBuff2 = Encoding.Unicode.GetBytes(json2);

                        string json3 = JsonSerializer.Serialize(Ownedgroups);
                        byte[] JsonBuff3 = Encoding.Unicode.GetBytes(json3);

                        NS.Write(JsonBuff1, 0, JsonBuff1.Length);
                        Thread.Sleep(1000);
                        NS.Write(JsonBuff2, 0, JsonBuff2.Length);
                        Thread.Sleep(1000);
                        NS.Write(JsonBuff3, 0, JsonBuff3.Length);

                        while (Client.Connected)
                        {
                            try
                            {
                                //Read data
                                byte[] ReceivedBuffer = new byte[10000];
                                NS.Read(ReceivedBuffer, 0, ReceivedBuffer.Length);
                                string Json = StripExtended(Encoding.Unicode.GetString(ReceivedBuffer));
                                Console.WriteLine(Json);
                                Packet Packet = JsonSerializer.Deserialize<Packet>(Json);
                                string RawJson = File.ReadAllText(@"C:\Users\costin\AppData\Roaming\.HeckToMsg\GroupData.json");

                                if (Packet.Type == "CREATEGROUP")
                                {
                                    string GroupName = Packet.Obj1.ToString();
                                    string UUID = Guid.NewGuid().ToString("N");
                                    GroupsData groups = JsonSerializer.Deserialize<GroupsData>(RawJson);

                                    if (groups.Groups.Exists(e => e.Name == GroupName))
                                    {
                                        byte[] Buffer = Encoding.Unicode.GetBytes("CREATEGROUP_EXISTS");
                                        Thread.Sleep(500);
                                        NS.Write(Buffer, 0, Buffer.Length);
                                    }
                                    else
                                    {
                                        groups.UsersDatas.Find(e => e.User == Username).JoinedGroups.Add(GroupName);
                                        groups.Groups.Add(new Group(GroupName, UUID, Username, bool.Parse(Packet.Obj2.ToString())));
                                        RawJson = JsonSerializer.Serialize(groups, options);
                                        File.WriteAllText(@"C:\Users\costin\AppData\Roaming\.HeckToMsg\GroupData.json", RawJson);
                                        byte[] Buffer = Encoding.Unicode.GetBytes("CREATEGROUP_SUCESS");
                                        Thread.Sleep(500);
                                        NS.Write(Buffer, 0, Buffer.Length);
                                    }
                                }
                                else if (Packet.Type == "JOINGROUP")
                                {
                                    string UUID = Packet.Obj1.ToString();
                                    GroupsData groups = JsonSerializer.Deserialize<GroupsData>(RawJson);
                                    if (groups.Groups.Exists(e => e.UniqueID == UUID))
                                    {
                                        string ReJson = JsonSerializer.Serialize(groups.Groups.Find(e => e.UniqueID == UUID));
                                        byte[] Buffer = Encoding.Unicode.GetBytes("JOINGROUP_SUCCESS" + ReJson);
                                        Thread.Sleep(500);
                                        NS.Write(Buffer, 0, Buffer.Length);

                                        groups.UsersDatas.Find(e => e.User == Username).JoinedGroups.Add(groups.Groups.Find(e => e.UniqueID == UUID).Name);
                                        string Json2 = JsonSerializer.Serialize(groups);
                                        File.WriteAllText(@"C:\Users\costin\AppData\Roaming\.HeckToMsg\GroupData.json", Json2);
                                    }
                                    else
                                    {
                                        byte[] Buffer = Encoding.Unicode.GetBytes("JOINGROUP_INVALID");
                                        Thread.Sleep(500);
                                        NS.Write(Buffer, 0, Buffer.Length);
                                    }
                                }
                                else if (Packet.Type == "CHANGECHAT")
                                {
                                    string GroupName = Packet.Obj1.ToString();
                                    GroupsData groups = JsonSerializer.Deserialize<GroupsData>(RawJson);
                                    if (groups.UsersDatas.Find(e => e.User == Username).JoinedGroups.Exists(e => e == GroupName))
                                    {
                                        byte[] Buffer = Encoding.Unicode.GetBytes("CHANGECHAT_SUCCESS");
                                        Thread.Sleep(500);
                                        NS.Write(Buffer, 0, Buffer.Length);
                                        Console.WriteLine("changed chat lol");
                                    }
                                    else
                                    {
                                        Console.WriteLine(Username + ((IPEndPoint)Client.Client.RemoteEndPoint).Address.ToString() + " Specified to change chat to a group that he is not in!");
                                        byte[] Buffer = Encoding.Unicode.GetBytes("CHANGECHAT_WRONGGROUP");
                                        Thread.Sleep(500);
                                        NS.Write(Buffer, 0, Buffer.Length);
                                    }
                                }
                                else if (Packet.Type == "SENDMSG")
                                {
                                    string GroupName = Packet.Obj1.ToString();
                                    string Message = Packet.Obj2.ToString();
                                    string NewMsg = Username + ":" + Message;

                                    Packet packet = new Packet
                                    {
                                        Type = "MESSAGE",
                                        Obj1 = GroupName,
                                        Obj2 = NewMsg
                                    };

                                    GroupsData groups = JsonSerializer.Deserialize<GroupsData>(RawJson);

                                    if (groups.Groups.Exists(e => e.Name == GroupName) && groups.UsersDatas.Find(e => e.User == Username).JoinedGroups.Exists(e => e == GroupName))
                                    {
                                        Console.WriteLine(GroupName + "|" + NewMsg);
                                        byte[] Buffer = Encoding.Unicode.GetBytes("SENDMSG_OK");
                                        Thread.Sleep(500);
                                        NS.Write(Buffer, 0, Buffer.Length);
                                        for (int i = 0; i < Clients.Count; i++)
                                        {
                                            if (Clients[i] != Client)
                                            {
                                                NetworkStream NS2 = Clients[i].GetStream();
                                                byte[] Buffer2 = Encoding.Unicode.GetBytes(JsonSerializer.Serialize(packet));
                                                NS2.Write(Buffer2, 0, Buffer2.Length);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(Username + ((IPEndPoint)Client.Client.RemoteEndPoint).Address.ToString() + " tried to send a message in a group he is not in/doesn't exist!");
                                        byte[] Buffer = Encoding.Unicode.GetBytes("SENDMSG_BAD");
                                        Thread.Sleep(500);
                                        NS.Write(Buffer, 0, Buffer.Length);
                                    }
                                }


                            }
                            catch (IOException)
                            {
                                Console.WriteLine(Username + " " + ((IPEndPoint)Client.Client.RemoteEndPoint).Address.ToString() + " left the session.");
                            }
                        }

                    }
                    else //wrong password
                    {
                        byte[] ERR_WRONGPASS = Encoding.Unicode.GetBytes("ERR_WRONGPASS");
                        NS.Write(ERR_WRONGPASS, 0, ERR_WRONGPASS.Length);
                        Console.WriteLine("connection's password is invalid.");
                        Client.Close();
                        Clients.Remove(Client);
                        return;
                    }
                }
                else //doesn't exist
                {
                    byte[] ERR_NOEXIST = Encoding.Unicode.GetBytes("ERR_NOEXIST");
                    NS.Write(ERR_NOEXIST, 0, ERR_NOEXIST.Length);
                    Console.WriteLine("connection's account doesn't exist.");
                    Client.Close();
                    Clients.Remove(Client);
                    return;
                }
            }
            else if (ACCOUT[0] == "NEWACCOUNT")
            {
                Console.WriteLine("User trying to create, username: " + ACCOUT[2] + " | Ip:" + ((IPEndPoint)Client.Client.RemoteEndPoint).Address.ToString());
                //create account in json

                string jsonf = File.ReadAllText(@"C:\Users\costin\AppData\Roaming\.HeckToMsg\Accounts.json");

                AccDatas data;
                data = new AccDatas(new List<string>(), new List<string>(), new List<string>());
                data = JsonSerializer.Deserialize<AccDatas>(jsonf);

                if (data.Usernames.Exists(e => e == ACCOUT[2]))
                {
                    byte[] ERR_DOUBLE = Encoding.Unicode.GetBytes("ERR_DOUBLE");
                    NS.Write(ERR_DOUBLE, 0, ERR_DOUBLE.Length);
                    Console.WriteLine("connection's account creation's username is Conflicting. sent an ERR_DOUBLE.");
                    Client.Close();
                    Clients.Remove(Client);
                    return;
                }

                if (data.Ips.FindAll(e => e == ((IPEndPoint)Client.Client.RemoteEndPoint).Address.ToString()).Count == 3)
                {
                    byte[] ERR_TOOMANYACCS = Encoding.Unicode.GetBytes("ERR_TOOMANYACCS");
                    NS.Write(ERR_TOOMANYACCS, 0, ERR_TOOMANYACCS.Length);
                    Console.WriteLine("connection has created 3 accounts in a row. sent an ERR-TOOMANYACCS.");
                    Client.Close();
                    Clients.Remove(Client);
                    return;
                }

                data.Usernames.Add(ACCOUT[2]);
                data.Passwords.Add(ACCOUT[1]);
                data.Ips.Add(((IPEndPoint)Client.Client.RemoteEndPoint).Address.ToString());

                GroupsData data1 = new GroupsData();
                data1 = JsonSerializer.Deserialize<GroupsData>(File.ReadAllText(@"C:\Users\costin\AppData\Roaming\.HeckToMsg\GroupData.json"), options);

                UserData userData = new UserData(new List<string>(), ACCOUT[2]);
                data1.UsersDatas.Add(userData);

                string json2 = JsonSerializer.Serialize(data1, options);
                string json = JsonSerializer.Serialize(data, options);
                File.WriteAllText(@"C:\Users\costin\AppData\Roaming\.HeckToMsg\Accounts.json", json);
                File.WriteAllText(@"C:\Users\costin\AppData\Roaming\.HeckToMsg\GroupData.json", json2);
                byte[] ERR_SUCCESS = Encoding.Unicode.GetBytes("ERR_SUCCESS");
                NS.Write(ERR_SUCCESS, 0, ERR_SUCCESS.Length);
                Console.WriteLine("connection account has been made with success.");
                Client.Close();
                Clients.Remove(Client);
                return;//This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

            }
            else
            {
                Console.WriteLine("new connection sent an invalid response, closing the connection. IP: " + ((IPEndPoint)Client.Client.RemoteEndPoint).Address.ToString());
                Client.Close();
                Clients.Remove(Client);
            }
        }

        private static void Main()
        {
            options.WriteIndented = true;
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Server.Start();
            Console.WriteLine("Listening");
            while (true)
            {
                TcpClient Cl = Server.AcceptTcpClient();
                Clients.Add(Cl);
                PerClientThread = new Thread(new ParameterizedThreadStart(PerClient));
                PerClientThread.Start(Clients[Clients.Count - 1]);
            }
        }
        public class Group
        {
            public Group(string name, string uniqueID, string owner, bool isPublic)
            {
                Name = name;
                UniqueID = uniqueID;
                Owner = owner;
                IsPublic = isPublic;
            }
            public string Name { get; set; }
            public string UniqueID { get; set; }
            public string Owner { get; }
            public bool IsPublic { get; }
        }
        public class UserData
        {
            public UserData(List<string> joinedgroups, string user)
            {
                JoinedGroups = joinedgroups;
                User = user;
            }
            public List<string> JoinedGroups { get; set; }
            public string User { get; set; }

        }
        public class GroupsData
        {
            public GroupsData()
            {
                Groups = new List<Group>();
                UsersDatas = new List<UserData>();
            }
            public List<Group> Groups { get; set; }

            public List<UserData> UsersDatas { get; set; }
        }
        public class Packet
        {
            public string Type { get; set; }
            public object Obj1 { get; set; }
            public object Obj2 { get; set; }
            public object Obj3 { get; set; }
        }
    }
}

//This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.
