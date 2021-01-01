using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.Json;
using System.Collections.Generic;
using System.Security.Cryptography;
//This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace HeckToMessage_Client
{
    public partial class Main : Form
    {
        public string Username;
        public TcpClient Client;
        public List<Group> PublicGroups;
        public string CurrentGroup;
        public Thread Loop;
        public List<GroupChat> GroupChats;
        public string InvalidPacketRedirect;
        public List<string> Groupsowned;
        public Main(string username, TcpClient client)
        {
            InitializeComponent();
            Username = username;
            Client = client;
            Init();
        }
        private void Init() //this will request a bunch of data from the server.
        {
            byte[] JoinedBuff = new byte[10000];
            byte[] PublicBuff = new byte[10000];

            NetworkStream NS = Client.GetStream();

            NS.Read(JoinedBuff, 0, JoinedBuff.Length);
            NS.Read(PublicBuff, 0, PublicBuff.Length);

            string Json1 = StripExtended(Encoding.Unicode.GetString(JoinedBuff));
            string Json2 = StripExtended(Encoding.Unicode.GetString(PublicBuff));

            List<string> JoinedGroups = JsonSerializer.Deserialize<List<string>>(Json1);
            List<Group> PublicGroups = JsonSerializer.Deserialize<List<Group>>(Json2);

            for (int i = 0; i < JoinedGroups.Count; i++)
            {
                JoinedGrups.Items.Add(JoinedGroups[i]);
            }

            for (int i = 0; i < PublicGroups.Count; i++)
            {
                PublicGrups.Items.Add(PublicGroups[i].Name);
            }
            this.PublicGroups = PublicGroups;

            byte[] OwnerGroups = new byte[10000];
            NS.Read(OwnerGroups, 0, OwnerGroups.Length);
            string Json3 = StripExtended(Encoding.Unicode.GetString(OwnerGroups));
            Groupsowned = JsonSerializer.Deserialize<List<string>>(Json3);

            Loop = new Thread(new ThreadStart(Cl_loop));
            Loop.Start();
        }

        public void Cl_loop()
        {
            while (Client.Connected)
            {
                try
                {
                    //data stuff
                    byte[] Superbuffer = new byte[1000000];
                    NetworkStream NS = Client.GetStream();
                    NS.Read(Superbuffer, 0, Superbuffer.Length);
                    string OutJson = StripExtended(Encoding.Unicode.GetString(Superbuffer));
                    try
                    {
                        Packet packet = JsonSerializer.Deserialize<Packet>(OutJson);
                        if (packet.Type == "GROUPTEXTDATA")
                        {
                            string Group = packet.Obj1.ToString();
                            string TextData = packet.Obj2.ToString();
                             if (GroupChats.Exists(e => e.GroupName == Group)){
                                GroupChats.Find(e => e.GroupName == Group).TextData += TextData;
                            }
                            else
                            {
                                GroupChats.Add(new GroupChat(Group, TextData));
                            }
                             if (CurrentGroup == Group)
                            {
                                Chat.Text = GroupChats.Find(e => e.GroupName == Group).TextData;
                            }
                        }
                    }
                    catch (JsonException)
                    {
                        InvalidPacketRedirect = OutJson;
                    }

                }
                catch
                {

                    throw;
                }
            }
            /*                    MethodInvoker inv = delegate
                    {

                    };
                    Invoke(inv);*/
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
        public static string StripExtended(string arg)
        {
            StringBuilder buffer = new StringBuilder(arg.Length); //Max length
            foreach (char ch in arg)
            {
                ushort num = Convert.ToUInt16(ch);//In .NET, chars are UTF-16
                //The basic characters have the same code points as ASCII, and the extended characters are bigger
                if (num >= 32u)
                {
                    buffer.Append(ch);
                }
            }
            return buffer.ToString();
        }
        private void CreateGroup_Click(object sender, EventArgs e)
        {
            CreateGroup createGroup = new CreateGroup(Username, Client, this);
            createGroup.ShowDialog();
        }

        private void JoinGroup_Click(object sender, EventArgs e)
        {
            JoinGroup joingroup = new JoinGroup(Username, Client, this);
            joingroup.ShowDialog();
        }

        private void JoinedGrups_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = JoinedGrups.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string SelectedGroup = JoinedGrups.Items[index].ToString();
                Packet packet = new Packet
                {
                    Type = "CHANGECHAT",
                    Obj1 = SelectedGroup
                };
                byte[] Buffer = Encoding.Unicode.GetBytes(JsonSerializer.Serialize(packet));
                Client.GetStream().Write(Buffer, 0, Buffer.Length);
                Thread.Sleep(1000);
                if (InvalidPacketRedirect == "CHANGECHAT_SUCCESS")
                {
                    CurrentGroup = SelectedGroup;
                    CurrentGroupTitle.Text = SelectedGroup;
                    if (Groupsowned.Contains(SelectedGroup))
                    {
                        ID.Text = Groupsowned[Groupsowned.IndexOf(SelectedGroup) + 1];
                    }
                }
                else if (InvalidPacketRedirect == "CHANGECHAT_WRONGGROUP")
                {
                    Version.Text += "ERROR: Hacker detected!";
                }
                else
                {
                    Version.Text += "ERROR: unknown, contact staff.";
                }
            }
        }

        private void JoinedGrups_MouseClick(object sender, MouseEventArgs e)
        {
            int index = JoinedGrups.IndexFromPoint(e.Location);
            if (index == System.Windows.Forms.ListBox.NoMatches)
            {
                JoinedGrups.ClearSelected();
            }
        }

        private void UserInput_Leave(object sender, EventArgs e)
        {
            if (UserInput.Text == "")
            {
                UserInput.Text = "Type your message here..";
                UserInput.ForeColor = Color.Gray;
            }
        }

        private void UserInput_MouseDown(object sender, MouseEventArgs e)
        {
            if (UserInput.Text == "Type your message here..")
            {
                UserInput.Text = "";
                UserInput.ForeColor = Color.Black;
            }
        }//This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.

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

        public class Packet
        {
            public string Type { get; set; }
            public object Obj1 { get; set; }
            public object Obj2 { get; set; }
            public object Obj3 { get; set; }
        }

        private void PublicGrups_MouseClick(object sender, MouseEventArgs e)
        {
            int index = PublicGrups.IndexFromPoint(e.Location);
            if (index == System.Windows.Forms.ListBox.NoMatches)
            {
                JoinedGrups.ClearSelected();
            }
        }

        private void PublicGrups_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = PublicGrups.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string SelectedGroup = PublicGrups.Items[index].ToString();

            }
        }
        public class GroupChat
        {
            public GroupChat(string Groupname, string Textdata)
            {
                GroupName = Groupname;
                TextData = Textdata;
            }
            public string GroupName { get; set; }
            public string TextData { get; set; }
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                
                Packet packet = new Packet
                {
                    Type = "SENDMSG",
                    Obj1 = CurrentGroup,
                    Obj2 = UserInput.Text
                };
                string Json = JsonSerializer.Serialize(packet);
                byte[] Buffer = Encoding.Unicode.GetBytes(Json);
                Client.GetStream().Write(Buffer, 0, Buffer.Length);
                UserInput.Text = "";
                if (InvalidPacketRedirect == "SENDMSG_OK")
                {
                    Chat.Text += "\n" + Username + ":" + UserInput.Text;
                }
                else if (InvalidPacketRedirect == "SENDMSG_BAD")
                {
                    //idk
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
//This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.
