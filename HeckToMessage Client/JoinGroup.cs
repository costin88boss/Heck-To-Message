using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeckToMessage_Client
{
    public partial class JoinGroup : Form
    {
        public TcpClient Client;
        public string Username;
        public Main main1;
        public JoinGroup(string username, TcpClient client, Main main)
        {
            InitializeComponent();
            Client = client;
            Username = username;
            main1 = main;
        }

        private void JoinBut_Click(object sender, EventArgs e)
        {
            string ID = UniqueIdText.Text;

            if (ID != "")
            {
                Main.Packet Data = new Main.Packet
                {
                    Type = "JOINGROUP",
                    Obj1 = ID
                };
                byte[] Buffer = Encoding.Unicode.GetBytes(JsonSerializer.Serialize(Data));
                Client.GetStream().Write(Buffer, 0, Buffer.Length);
                Thread.Sleep(1000);
                string i = main1.InvalidPacketRedirect;
                if (i == "JOINGROUP_INVALID")
                {
                    Problem.Text = "Invalid ID!";
                }
                else if (i.StartsWith("JOINGROUP_SUCCESS"))
                {
                    Main.Group group = JsonSerializer.Deserialize<Main.Group>(i.Remove(0, 17));
                    main1.JoinedGrups.Items.Add(group.Name);
                    main1.CurrentGroup = group.Name;
                    this.Close();
                }
                else
                {
                    Problem.Text = "unknown error! contact the staff.";
                }
            }
            else
            {
                Problem.Text = "Empty ID!";
            }
        }

        private void CancelBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
