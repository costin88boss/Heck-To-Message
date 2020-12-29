namespace HeckToMessage_Client
{
    partial class JoinGroup
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
            this.CancelBut = new System.Windows.Forms.Button();
            this.JoinBut = new System.Windows.Forms.Button();
            this.JoinGroupTitle = new System.Windows.Forms.Label();
            this.IdLabel = new System.Windows.Forms.Label();
            this.Info = new System.Windows.Forms.Label();
            this.UniqueIdText = new System.Windows.Forms.TextBox();
            this.Problem = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CancelBut
            // 
            this.CancelBut.Location = new System.Drawing.Point(12, 171);
            this.CancelBut.Name = "CancelBut";
            this.CancelBut.Size = new System.Drawing.Size(75, 23);
            this.CancelBut.TabIndex = 0;
            this.CancelBut.Text = "Cancel";
            this.CancelBut.UseVisualStyleBackColor = true;
            this.CancelBut.Click += new System.EventHandler(this.CancelBut_Click);
            // 
            // JoinBut
            // 
            this.JoinBut.Location = new System.Drawing.Point(273, 171);
            this.JoinBut.Name = "JoinBut";
            this.JoinBut.Size = new System.Drawing.Size(75, 23);
            this.JoinBut.TabIndex = 1;
            this.JoinBut.Text = "Join";
            this.JoinBut.UseVisualStyleBackColor = true;
            this.JoinBut.Click += new System.EventHandler(this.JoinBut_Click);
            // 
            // JoinGroupTitle
            // 
            this.JoinGroupTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.JoinGroupTitle.Location = new System.Drawing.Point(0, 0);
            this.JoinGroupTitle.Name = "JoinGroupTitle";
            this.JoinGroupTitle.Size = new System.Drawing.Size(360, 23);
            this.JoinGroupTitle.TabIndex = 2;
            this.JoinGroupTitle.Text = "Join a Group";
            this.JoinGroupTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IdLabel
            // 
            this.IdLabel.AutoSize = true;
            this.IdLabel.Location = new System.Drawing.Point(12, 70);
            this.IdLabel.Name = "IdLabel";
            this.IdLabel.Size = new System.Drawing.Size(53, 13);
            this.IdLabel.TabIndex = 3;
            this.IdLabel.Text = "Unique Id";
            // 
            // Info
            // 
            this.Info.AutoSize = true;
            this.Info.Location = new System.Drawing.Point(231, 44);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(117, 91);
            this.Info.TabIndex = 4;
            this.Info.Text = "In order to join a group,\r\nyou will need to get the\r\nunique join id of it.\r\n\r\nalt" +
    "ernatively, you can\r\njoin one of the public\r\ngroups in the public list.";
            // 
            // UniqueIdText
            // 
            this.UniqueIdText.Location = new System.Drawing.Point(12, 86);
            this.UniqueIdText.Name = "UniqueIdText";
            this.UniqueIdText.Size = new System.Drawing.Size(119, 20);
            this.UniqueIdText.TabIndex = 5;
            // 
            // Problem
            // 
            this.Problem.AutoSize = true;
            this.Problem.Location = new System.Drawing.Point(93, 181);
            this.Problem.Name = "Problem";
            this.Problem.Size = new System.Drawing.Size(0, 13);
            this.Problem.TabIndex = 6;
            // 
            // JoinGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 206);
            this.Controls.Add(this.Problem);
            this.Controls.Add(this.UniqueIdText);
            this.Controls.Add(this.Info);
            this.Controls.Add(this.IdLabel);
            this.Controls.Add(this.JoinGroupTitle);
            this.Controls.Add(this.JoinBut);
            this.Controls.Add(this.CancelBut);
            this.Name = "JoinGroup";
            this.Text = "Join a Group";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelBut;
        private System.Windows.Forms.Button JoinBut;
        private System.Windows.Forms.Label JoinGroupTitle;
        private System.Windows.Forms.Label IdLabel;
        private System.Windows.Forms.Label Info;
        private System.Windows.Forms.TextBox UniqueIdText;
        private System.Windows.Forms.Label Problem;
    }
}