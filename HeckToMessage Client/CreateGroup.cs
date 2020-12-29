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
    public partial class CreateGroup : Form
    {
        public TcpClient Client;
        public string Username;
        public Main main1;
        public CreateGroup(string username, TcpClient client, Main main)
        {
            InitializeComponent();
            Client = client;
            Username = username;
            main1 = main;
        }

        private void CreateBut_Click(object sender, EventArgs e)
        {
            bool Public = AppearPublic.Checked;
            string Title = GroupName.Text;
            if (Title != "")
            {
                Main.Packet Data = new Main.Packet
                {
                    Type = "CREATEGROUP",
                    Obj1 = Title,
                    Obj2 = Public
                };

                byte[] Buffer = Encoding.Unicode.GetBytes(JsonSerializer.Serialize(Data));
                Client.GetStream().Write(Buffer, 0, Buffer.Length);
                Thread.Sleep(1000);
                string i = main1.InvalidPacketRedirect;
                if (i == "CREATEGROUP_EXISTS")
                {
                    Problem.Text = "Group name exists!";
                }
                else if (i == "CREATEGROUP_SUCESS")
                {
                    main1.JoinedGrups.Items.Add(Title);
                    if (Public)
                    {
                        main1.PublicGrups.Items.Add(Title);
                    }
                    main1.CurrentGroup = Title;
                    this.Close();
                }
                else
                {
                    Problem.Text = "unknown error! contact the staff.";
                }

            }
            else
            {
                Problem.Text = "Group name can't be empty!";
            }
        }

        private void CancelBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
