namespace HeckToMessage_Client
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Problemtext = new System.Windows.Forms.Label();
            this.Company = new System.Windows.Forms.Label();
            this.Version = new System.Windows.Forms.Label();
            this.CreateGroup = new System.Windows.Forms.Button();
            this.JoinGroup = new System.Windows.Forms.Button();
            this.JoinedGrups = new System.Windows.Forms.ListBox();
            this.PublicGrups = new System.Windows.Forms.ListBox();
            this.UserInput = new System.Windows.Forms.TextBox();
            this.Chat = new System.Windows.Forms.TextBox();
            this.CurrentGroupTitle = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Problemtext
            // 
            this.Problemtext.AutoSize = true;
            this.Problemtext.Location = new System.Drawing.Point(12, 376);
            this.Problemtext.Name = "Problemtext";
            this.Problemtext.Size = new System.Drawing.Size(0, 13);
            this.Problemtext.TabIndex = 7;
            // 
            // Company
            // 
            this.Company.AutoSize = true;
            this.Company.Font = new System.Drawing.Font("Nasalization", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Company.Location = new System.Drawing.Point(784, 491);
            this.Company.Name = "Company";
            this.Company.Size = new System.Drawing.Size(118, 18);
            this.Company.TabIndex = 9;
            this.Company.Text = "Costinos™";
            // 
            // Version
            // 
            this.Version.AutoSize = true;
            this.Version.Font = new System.Drawing.Font("Malgun Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Version.Location = new System.Drawing.Point(12, 496);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(47, 13);
            this.Version.TabIndex = 10;
            this.Version.Text = "Beta 4.2 (OPEN SOURCE) [broken]";
            // 
            // CreateGroup
            // 
            this.CreateGroup.Location = new System.Drawing.Point(742, 347);
            this.CreateGroup.Name = "CreateGroup";
            this.CreateGroup.Size = new System.Drawing.Size(160, 23);
            this.CreateGroup.TabIndex = 11;
            this.CreateGroup.Text = "Create Group";
            this.CreateGroup.UseVisualStyleBackColor = true;
            this.CreateGroup.Click += new System.EventHandler(this.CreateGroup_Click);
            // 
            // JoinGroup
            // 
            this.JoinGroup.Location = new System.Drawing.Point(742, 376);
            this.JoinGroup.Name = "JoinGroup";
            this.JoinGroup.Size = new System.Drawing.Size(160, 23);
            this.JoinGroup.TabIndex = 12;
            this.JoinGroup.Text = "Join Group";
            this.JoinGroup.UseVisualStyleBackColor = true;
            this.JoinGroup.Click += new System.EventHandler(this.JoinGroup_Click);
            // 
            // JoinedGrups
            // 
            this.JoinedGrups.FormattingEnabled = true;
            this.JoinedGrups.Location = new System.Drawing.Point(12, 12);
            this.JoinedGrups.Name = "JoinedGrups";
            this.JoinedGrups.Size = new System.Drawing.Size(160, 329);
            this.JoinedGrups.TabIndex = 13;
            this.JoinedGrups.MouseClick += new System.Windows.Forms.MouseEventHandler(this.JoinedGrups_MouseClick);
            this.JoinedGrups.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.JoinedGrups_MouseDoubleClick);
            // 
            // PublicGrups
            // 
            this.PublicGrups.FormattingEnabled = true;
            this.PublicGrups.Location = new System.Drawing.Point(742, 12);
            this.PublicGrups.Name = "PublicGrups";
            this.PublicGrups.Size = new System.Drawing.Size(160, 329);
            this.PublicGrups.TabIndex = 14;
            this.PublicGrups.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PublicGrups_MouseClick);
            this.PublicGrups.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PublicGrups_MouseDoubleClick);
            // 
            // UserInput
            // 
            this.UserInput.ForeColor = System.Drawing.Color.Gray;
            this.UserInput.Location = new System.Drawing.Point(178, 347);
            this.UserInput.MaxLength = 200;
            this.UserInput.Name = "UserInput";
            this.UserInput.Size = new System.Drawing.Size(558, 20);
            this.UserInput.TabIndex = 16;
            this.UserInput.Text = "Type your message here..";
            this.UserInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserInput_KeyDown);
            this.UserInput.Leave += new System.EventHandler(this.UserInput_Leave);
            this.UserInput.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UserInput_MouseDown);
            // 
            // Chat
            // 
            this.Chat.Location = new System.Drawing.Point(178, 12);
            this.Chat.MaxLength = 2147483647;
            this.Chat.Multiline = true;
            this.Chat.Name = "Chat";
            this.Chat.ReadOnly = true;
            this.Chat.Size = new System.Drawing.Size(558, 329);
            this.Chat.TabIndex = 17;
            // 
            // CurrentGroupTitle
            // 
            this.CurrentGroupTitle.AutoSize = true;
            this.CurrentGroupTitle.Location = new System.Drawing.Point(12, 441);
            this.CurrentGroupTitle.Name = "CurrentGroupTitle";
            this.CurrentGroupTitle.Size = new System.Drawing.Size(0, 13);
            this.CurrentGroupTitle.TabIndex = 19;
            // 
            // ID
            // 
            this.ID.Location = new System.Drawing.Point(12, 376);
            this.ID.MaxLength = 100;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Size = new System.Drawing.Size(270, 20);
            this.ID.TabIndex = 20;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 518);
            this.Controls.Add(this.ID);
            this.Controls.Add(this.CurrentGroupTitle);
            this.Controls.Add(this.Chat);
            this.Controls.Add(this.UserInput);
            this.Controls.Add(this.PublicGrups);
            this.Controls.Add(this.JoinedGrups);
            this.Controls.Add(this.JoinGroup);
            this.Controls.Add(this.CreateGroup);
            this.Controls.Add(this.Company);
            this.Controls.Add(this.Version);
            this.Controls.Add(this.Problemtext);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Heck To Message";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Problemtext;
        private System.Windows.Forms.Label Company;
        private System.Windows.Forms.Label Version;
        private System.Windows.Forms.Button CreateGroup;
        private System.Windows.Forms.Button JoinGroup;
        private System.Windows.Forms.TextBox UserInput;
        public System.Windows.Forms.ListBox JoinedGrups;
        public System.Windows.Forms.TextBox Chat;
        public System.Windows.Forms.ListBox PublicGrups;
        private System.Windows.Forms.Label CurrentGroupTitle;
        private System.Windows.Forms.TextBox ID;
    }
}

