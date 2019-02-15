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

        string[] colList = { "경로", "날짜", "ID", "이름", "용량" };

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lstFileView.Items.Count < 1)
            {
                MessageBox.Show("목록이 비었습니다.");
                return;
            }

            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Add();            // Sheet1이 포함된 빈 workbook 생성
            Excel.Worksheet ws1 = wb.Worksheets.Item["Sheet1"];       // Sheet1 가져오기
            
            // 열 이름 붙이기
            for (int i = 1; i <= colList.Length; i++)
                ws1.Cells[1, i] = colList[i - 1];

            // 엑셀 테이블
            for(int row = 1; row <= lstFileView.Items.Count; row++)
            {
                for(int col = 1; col <= 5; col++)
                {
                    if (checkbox.Checked)
                    {
                        ws1.Cells[row + 1, col] = patients[row - 1].data[col - 1];
                    }
                    else
                    {
                        ws1.Cells[row + 1, col] = lstFileView.Items[row - 1].SubItems[col - 1].Text;
                    }
                }
            }

            Excel.Range range = ws1.Range[ws1.Cells[1, 1], ws1.Cells[colList.Length, lstFileView.Items.Count]];
            range.EntireColumn.AutoFit();


            //Excel.Range range = ws1.Cells[1, 1];
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

        List<Patient> patients;

        private void FindFile(string dir, string word)
        {
            // 파일 목록 저장
            // 선택한 directory의 하위 dir 파일까지 모두 검색함
            string[] files = Directory.GetFiles(dir, word, SearchOption.AllDirectories);
            patients = new List<Patient>();

            // 파일 개수만큼 리스트에 추가
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(files[i]);
                string name = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                string date = String.Empty;
                string id = String.Empty;

                // 숫자 매칭
                string number = Regex.Match(name, "[0-9]{8,}").Value;
                if (number.Length == 8)
                    date = number.Substring(0, 8);
                else
                    date = number;
                
                // 글자 매칭
                string text = Regex.Match(name, @"[a-zA-Z]+(\s|_)?[a-zA-Z]+").Value;
                if (text.Length != 0)
                    name = text;
                else
                    name = String.Empty;

                // patient list 생성
                patients.Add(new Patient(fileInfo.FullName, date, id, name, fileInfo.Length.ToString()));

                // Table 생성 ; 경로 / 날짜 / 이름 / ID / 파일크기
                ListViewItem item = new ListViewItem(fileInfo.FullName);

                item.SubItems.Add(date);
                item.SubItems.Add(id);
                item.SubItems.Add(name);
                item.SubItems.Add(fileInfo.Length.ToString());

                lstFileView.Items.Add(item);
            }

            // Dataset
            var dataset = patients
                .Where(patient => !String.IsNullOrEmpty(patient.data[1]) && !String.IsNullOrEmpty(patient.data[3]))
                .Select(patient => patient).ToList();

            // filling list
            var fill = patients
                .Select(patient => {
                    var _data = dataset.Where(d => (d.data[1] == patient.data[1]) || (d.data[3] == patient.data[3])).Take(1).ToList();
                    if (_data.Count() != 0 )
                    {
                        patient.data[1] = _data[0].data[1];
                        patient.data[3] = _data[0].data[3];
                    }
                    return patient;
                }).ToList();
            patients = fill;
        }
    }

    public class Patient
    {
        public string[] data = new string[5];

        public Patient(string filepath, string date, string id, string name, string filesize)
        {
            data[0] = filepath;
            data[1] = date;
            data[2] = id;
            data[3] = name;
            data[4] = filesize;
        }
    }
}
