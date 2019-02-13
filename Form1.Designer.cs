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
            this.btnSave = new System.Windows.Forms.Button();
            this.dirLabel = new System.Windows.Forms.Label();
            this.resultLabel = new System.Windows.Forms.Label();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.btnDir = new System.Windows.Forms.Button();
            this.txtWord = new System.Windows.Forms.TextBox();
            this.lstFileView = new System.Windows.Forms.ListView();
            this.colPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblRegex = new System.Windows.Forms.Label();
            this.txtRegex = new System.Windows.Forms.TextBox();
            this.checkbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(754, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(158, 28);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Excel 저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dirLabel
            // 
            this.dirLabel.AutoSize = true;
            this.dirLabel.Location = new System.Drawing.Point(12, 11);
            this.dirLabel.Name = "dirLabel";
            this.dirLabel.Size = new System.Drawing.Size(86, 18);
            this.dirLabel.TabIndex = 1;
            this.dirLabel.Text = "검색 경로";
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(12, 45);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(86, 18);
            this.resultLabel.TabIndex = 1;
            this.resultLabel.Text = "찾을 파일";
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(104, 6);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(401, 28);
            this.txtDir.TabIndex = 2;
            // 
            // btnDir
            // 
            this.btnDir.Location = new System.Drawing.Point(511, 6);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(75, 28);
            this.btnDir.TabIndex = 3;
            this.btnDir.Text = "찾기";
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // txtWord
            // 
            this.txtWord.Location = new System.Drawing.Point(104, 40);
            this.txtWord.Name = "txtWord";
            this.txtWord.Size = new System.Drawing.Size(196, 28);
            this.txtWord.TabIndex = 4;
            this.txtWord.Text = "*.mcs";
            // 
            // lstFileView
            // 
            this.lstFileView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPath,
            this.colDate,
            this.colID,
            this.colName,
            this.colSize});
            this.lstFileView.GridLines = true;
            this.lstFileView.Location = new System.Drawing.Point(15, 74);
            this.lstFileView.Name = "lstFileView";
            this.lstFileView.Size = new System.Drawing.Size(900, 474);
            this.lstFileView.TabIndex = 5;
            this.lstFileView.UseCompatibleStateImageBehavior = false;
            this.lstFileView.View = System.Windows.Forms.View.Details;
            // 
            // colPath
            // 
            this.colPath.Text = "경로";
            this.colPath.Width = 300;
            // 
            // colDate
            // 
            this.colDate.Text = "날짜";
            this.colDate.Width = 100;
            // 
            // colID
            // 
            this.colID.Text = "ID";
            this.colID.Width = 80;
            // 
            // colName
            // 
            this.colName.Text = "이름";
            this.colName.Width = 120;
            // 
            // colSize
            // 
            this.colSize.Text = "파일 용량";
            this.colSize.Width = 130;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(592, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(156, 62);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblRegex
            // 
            this.lblRegex.AutoSize = true;
            this.lblRegex.Location = new System.Drawing.Point(317, 45);
            this.lblRegex.Name = "lblRegex";
            this.lblRegex.Size = new System.Drawing.Size(66, 18);
            this.lblRegex.TabIndex = 7;
            this.lblRegex.Text = "Reg Ex";
            // 
            // txtRegex
            // 
            this.txtRegex.Location = new System.Drawing.Point(389, 40);
            this.txtRegex.Name = "txtRegex";
            this.txtRegex.Size = new System.Drawing.Size(197, 28);
            this.txtRegex.TabIndex = 8;
            this.txtRegex.Text = "-";
            // 
            // checkbox
            // 
            this.checkbox.AutoSize = true;
            this.checkbox.Location = new System.Drawing.Point(755, 45);
            this.checkbox.Name = "checkbox";
            this.checkbox.Size = new System.Drawing.Size(130, 22);
            this.checkbox.TabIndex = 9;
            this.checkbox.Text = "빈칸 채우기";
            this.checkbox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 560);
            this.Controls.Add(this.checkbox);
            this.Controls.Add(this.txtRegex);
            this.Controls.Add(this.lblRegex);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lstFileView);
            this.Controls.Add(this.txtWord);
            this.Controls.Add(this.btnDir);
            this.Controls.Add(this.txtDir);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.dirLabel);
            this.Controls.Add(this.btnSave);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label dirLabel;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.TextBox txtWord;
        private System.Windows.Forms.ListView lstFileView;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colPath;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.Label lblRegex;
        private System.Windows.Forms.TextBox txtRegex;
        private System.Windows.Forms.CheckBox checkbox;
    }
}

