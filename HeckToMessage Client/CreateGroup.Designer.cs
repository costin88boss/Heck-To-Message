namespace HeckToMessage_Client
{
    partial class CreateGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateGroup));
            this.NewGroupTitle = new System.Windows.Forms.Label();
            this.GroupNameLabel = new System.Windows.Forms.Label();
            this.GroupName = new System.Windows.Forms.TextBox();
            this.AppearPublic = new System.Windows.Forms.CheckBox();
            this.CreateBut = new System.Windows.Forms.Button();
            this.CancelBut = new System.Windows.Forms.Button();
            this.AgreeText = new System.Windows.Forms.Label();
            this.Problem = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NewGroupTitle
            // 
            this.NewGroupTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.NewGroupTitle.Location = new System.Drawing.Point(0, 0);
            this.NewGroupTitle.Name = "NewGroupTitle";
            this.NewGroupTitle.Size = new System.Drawing.Size(509, 36);
            this.NewGroupTitle.TabIndex = 0;
            this.NewGroupTitle.Text = "Create a new Group";
            this.NewGroupTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GroupNameLabel
            // 
            this.GroupNameLabel.AutoSize = true;
            this.GroupNameLabel.Location = new System.Drawing.Point(12, 157);
            this.GroupNameLabel.Name = "GroupNameLabel";
            this.GroupNameLabel.Size = new System.Drawing.Size(67, 13);
            this.GroupNameLabel.TabIndex = 1;
            this.GroupNameLabel.Text = "Group Name";
            // 
            // GroupName
            // 
            this.GroupName.Location = new System.Drawing.Point(12, 173);
            this.GroupName.MaxLength = 30;
            this.GroupName.Name = "GroupName";
            this.GroupName.Size = new System.Drawing.Size(121, 20);
            this.GroupName.TabIndex = 3;
            // 
            // AppearPublic
            // 
            this.AppearPublic.AutoSize = true;
            this.AppearPublic.Location = new System.Drawing.Point(12, 100);
            this.AppearPublic.Name = "AppearPublic";
            this.AppearPublic.Size = new System.Drawing.Size(121, 17);
            this.AppearPublic.TabIndex = 4;
            this.AppearPublic.Text = "Appear in public List";
            this.AppearPublic.UseVisualStyleBackColor = true;
            // 
            // CreateBut
            // 
            this.CreateBut.Location = new System.Drawing.Point(422, 251);
            this.CreateBut.Name = "CreateBut";
            this.CreateBut.Size = new System.Drawing.Size(75, 23);
            this.CreateBut.TabIndex = 5;
            this.CreateBut.Text = "Create";
            this.CreateBut.UseVisualStyleBackColor = true;
            this.CreateBut.Click += new System.EventHandler(this.CreateBut_Click);
            // 
            // CancelBut
            // 
            this.CancelBut.Location = new System.Drawing.Point(12, 251);
            this.CancelBut.Name = "CancelBut";
            this.CancelBut.Size = new System.Drawing.Size(75, 23);
            this.CancelBut.TabIndex = 6;
            this.CancelBut.Text = "Cancel";
            this.CancelBut.UseVisualStyleBackColor = true;
            this.CancelBut.Click += new System.EventHandler(this.CancelBut_Click);
            // 
            // AgreeText
            // 
            this.AgreeText.AutoSize = true;
            this.AgreeText.Location = new System.Drawing.Point(323, 66);
            this.AgreeText.Name = "AgreeText";
            this.AgreeText.Size = new System.Drawing.Size(174, 104);
            this.AgreeText.TabIndex = 7;
            this.AgreeText.Text = resources.GetString("AgreeText.Text");
            // 
            // Problem
            // 
            this.Problem.AutoSize = true;
            this.Problem.Location = new System.Drawing.Point(93, 261);
            this.Problem.Name = "Problem";
            this.Problem.Size = new System.Drawing.Size(0, 13);
            this.Problem.TabIndex = 8;
            // 
            // CreateGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 286);
            this.Controls.Add(this.Problem);
            this.Controls.Add(this.AgreeText);
            this.Controls.Add(this.CancelBut);
            this.Controls.Add(this.CreateBut);
            this.Controls.Add(this.AppearPublic);
            this.Controls.Add(this.GroupName);
            this.Controls.Add(this.GroupNameLabel);
            this.Controls.Add(this.NewGroupTitle);
            this.Name = "CreateGroup";
            this.Text = "Create a Group";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NewGroupTitle;
        private System.Windows.Forms.Label GroupNameLabel;
        private System.Windows.Forms.TextBox GroupName;
        private System.Windows.Forms.CheckBox AppearPublic;
        private System.Windows.Forms.Button CreateBut;
        private System.Windows.Forms.Button CancelBut;
        private System.Windows.Forms.Label AgreeText;
        private System.Windows.Forms.Label Problem;
    }
}