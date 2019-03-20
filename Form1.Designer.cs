namespace MCSFinder
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.dirLabel = new System.Windows.Forms.Label();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.btnDir = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.mcsprogress = new System.Windows.Forms.ProgressBar();
            this.dcmprogress = new System.Windows.Forms.ProgressBar();
            this.searchCheckBox = new System.Windows.Forms.CheckedListBox();
            this.resultCheckBox = new System.Windows.Forms.CheckedListBox();
            this.lstResultView = new System.Windows.Forms.ListView();
            this.btnSave = new System.Windows.Forms.Button();
            this.checkDCM = new System.Windows.Forms.CheckBox();
            this.checkMCS = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // dirLabel
            // 
            this.dirLabel.AutoSize = true;
            this.dirLabel.Location = new System.Drawing.Point(11, 9);
            this.dirLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dirLabel.Name = "dirLabel";
            this.dirLabel.Size = new System.Drawing.Size(57, 12);
            this.dirLabel.TabIndex = 1;
            this.dirLabel.Text = "검색 경로";
            // 
            // txtDir
            // 
            this.txtDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDir.Location = new System.Drawing.Point(78, 4);
            this.txtDir.Margin = new System.Windows.Forms.Padding(2);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(512, 21);
            this.txtDir.TabIndex = 2;
            // 
            // btnDir
            // 
            this.btnDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDir.Location = new System.Drawing.Point(592, 4);
            this.btnDir.Margin = new System.Windows.Forms.Padding(2);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(60, 21);
            this.btnDir.TabIndex = 3;
            this.btnDir.Text = "...";
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(270, 30);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 50);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(13, 109);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(147, 21);
            this.txtSearch.TabIndex = 16;
            // 
            // mcsprogress
            // 
            this.mcsprogress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mcsprogress.Location = new System.Drawing.Point(334, 101);
            this.mcsprogress.Name = "mcsprogress";
            this.mcsprogress.Size = new System.Drawing.Size(314, 23);
            this.mcsprogress.TabIndex = 18;
            // 
            // dcmprogress
            // 
            this.dcmprogress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dcmprogress.Location = new System.Drawing.Point(334, 51);
            this.dcmprogress.Name = "dcmprogress";
            this.dcmprogress.Size = new System.Drawing.Size(314, 23);
            this.dcmprogress.TabIndex = 19;
            // 
            // searchCheckBox
            // 
            this.searchCheckBox.CheckOnClick = true;
            this.searchCheckBox.FormattingEnabled = true;
            this.searchCheckBox.Items.AddRange(new object[] {
            "이름",
            "ID",
            "생년월일",
            "촬영일자"});
            this.searchCheckBox.Location = new System.Drawing.Point(13, 30);
            this.searchCheckBox.Name = "searchCheckBox";
            this.searchCheckBox.Size = new System.Drawing.Size(147, 68);
            this.searchCheckBox.TabIndex = 23;
            this.searchCheckBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.searchCheckBox_ItemCheck);
            // 
            // resultCheckBox
            // 
            this.resultCheckBox.CheckOnClick = true;
            this.resultCheckBox.FormattingEnabled = true;
            this.resultCheckBox.Items.AddRange(new object[] {
            "이름",
            "ID",
            "생년월일",
            "촬영일자",
            "mcs 경로",
            "dcm 경로",
            "용량"});
            this.resultCheckBox.Location = new System.Drawing.Point(164, 30);
            this.resultCheckBox.Name = "resultCheckBox";
            this.resultCheckBox.Size = new System.Drawing.Size(100, 100);
            this.resultCheckBox.TabIndex = 24;
            this.resultCheckBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.resultCheckBox_ItemCheck);
            // 
            // lstResultView
            // 
            this.lstResultView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstResultView.Location = new System.Drawing.Point(12, 136);
            this.lstResultView.Name = "lstResultView";
            this.lstResultView.Size = new System.Drawing.Size(638, 258);
            this.lstResultView.TabIndex = 25;
            this.lstResultView.UseCompatibleStateImageBehavior = false;
            this.lstResultView.View = System.Windows.Forms.View.Details;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(270, 84);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 45);
            this.btnSave.TabIndex = 37;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // checkDCM
            // 
            this.checkDCM.AutoSize = true;
            this.checkDCM.Location = new System.Drawing.Point(334, 30);
            this.checkDCM.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkDCM.Name = "checkDCM";
            this.checkDCM.Size = new System.Drawing.Size(52, 16);
            this.checkDCM.TabIndex = 38;
            this.checkDCM.Text = "DCM";
            this.checkDCM.UseVisualStyleBackColor = true;
            // 
            // checkMCS
            // 
            this.checkMCS.AutoSize = true;
            this.checkMCS.Location = new System.Drawing.Point(334, 80);
            this.checkMCS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkMCS.Name = "checkMCS";
            this.checkMCS.Size = new System.Drawing.Size(52, 16);
            this.checkMCS.TabIndex = 39;
            this.checkMCS.Text = "MCS";
            this.checkMCS.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 413);
            this.Controls.Add(this.checkMCS);
            this.Controls.Add(this.checkDCM);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lstResultView);
            this.Controls.Add(this.resultCheckBox);
            this.Controls.Add(this.searchCheckBox);
            this.Controls.Add(this.dcmprogress);
            this.Controls.Add(this.mcsprogress);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnDir);
            this.Controls.Add(this.txtDir);
            this.Controls.Add(this.dirLabel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(677, 452);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label dirLabel;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ProgressBar mcsprogress;
        private System.Windows.Forms.ProgressBar dcmprogress;
        private System.Windows.Forms.CheckedListBox searchCheckBox;
        private System.Windows.Forms.CheckedListBox resultCheckBox;
        private System.Windows.Forms.ListView lstResultView;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox checkDCM;
        private System.Windows.Forms.CheckBox checkMCS;
    }
}

