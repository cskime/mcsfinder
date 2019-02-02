using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

using Excel = Microsoft.Office.Interop.Excel;

namespace MCSFinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] colList = { "날짜", "이름", "경로", "용량", "종류" };

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lstFileView.Items.Count < 1)
            {
                MessageBox.Show("목록이 비었습니다.");
                return;
            }

            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook wb = excelApp.Workbooks.Add();            // Sheet1이 포함된 빈 workbook 생성
            Excel.Worksheet ws1 = wb.Worksheets.Item["Sheet1"];       // Sheet1 가져오기
            //Excel.Worksheet ws2 = wb.Worksheets.Add(After: ws1);        // ws1 뒤에 새 워크시트 생성

            // 열 이름 붙이기
            for (int i = 1; i <= colList.Length; i++)
                ws1.Cells[1, i] = colList[i - 1];
            
            // 항목 채워넣기
            for(int row = 1; row <= lstFileView.Items.Count; row++)
            {
                for(int col = 1; col <= 5; col++)
                {
                    ws1.Cells[row + 1, col] = lstFileView.Items[row - 1].SubItems[col - 1].Text;
                }
            }

            //Excel.Range range = ws2.Cells[1, 1];
            //range.Formula = "=SUM(Sheet1!$A$1:$E$5)";   // 셀에 수식 기록

            //range = ws2.Cells[2, 1];
            //range.Formula = "=AVERAGE(Sheet1!A1:E5)";

            SaveFileDialog saveFileDlg = new SaveFileDialog();
            saveFileDlg.Title = "파일 저장";
            saveFileDlg.OverwritePrompt = true;
            saveFileDlg.Filter = "Excel File(*.xlsx)|.xlsx";

            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                wb.SaveAs(Filename: saveFileDlg.FileName);
                wb.Close();
            }
        }

        private void btnDir_Click(object sender, EventArgs e)
        {
            // 폴더브라우저 객체 생성
            FolderBrowserDialog dir = new FolderBrowserDialog();
            if (dir.ShowDialog() == DialogResult.OK)
            {
                this.txtDir.Text = dir.SelectedPath.Trim(); //선택한 디렉토리 경로
            }

            // 포커스 단어 입력창으로 이동
            txtWord.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 검색을 위한 체크
            if (txtWord.Text.Trim().Equals(""))
            {
                MessageBox.Show("검색단어를 입력해주세요.");
                return;
            }

            if (txtDir.Text.Trim().Equals(""))
            {
                MessageBox.Show("검색할 디렉토리를 입력해주세요.");
                return;
            }
            // 검색
            lstFileView.Items.Clear();
            FindFile(txtDir.Text.Trim(), txtWord.Text.Trim());

            txtWord.Focus();
        }

        private void FindFile(string dir, string word)
        {
            // 파일 목록 저장
            // 선택한 directory의 하위 dir 파일까지 모두 검색함
            string[] files = Directory.GetFiles(dir, word, SearchOption.AllDirectories);
            
            // 파일 개수만큼 리스트에 추가
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(files[i]);

                Regex regex = new Regex(txtRegex.Text);

                // 입력 기호로 split -> 일치하는 기호가 없다면 원래 파일명 하나 반환
                // 파일 이름이 날짜-이름.mcs 형식이거나 특정한 규칙 없이 만들어져 있음. 전자는 vals가 2개, 후자는 1개의 item을 가짐
                string[] vals = regex.Split(Path.GetFileNameWithoutExtension(fileInfo.FullName));

                // 경로 / 날짜 / 이름 / 파일크기 / 종류 
                ListViewItem item = new ListViewItem(fileInfo.FullName);
                switch (vals.Length)
                {
                    case 1:
                        item.SubItems.Add("");
                        item.SubItems.Add(vals[0]);
                        break;
                    case 2:
                        item.SubItems.Add(vals[0]);
                        item.SubItems.Add(vals[1]);
                        break;
                }
                item.SubItems.Add(fileInfo.Length.ToString());
                item.SubItems.Add(fileInfo.Extension.ToString());
                lstFileView.Items.Add(item);

            }
        }
    }
}
