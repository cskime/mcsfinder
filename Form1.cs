﻿using System;
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
using Dicom;
using Manager;

using Excel = Microsoft.Office.Interop.Excel;

namespace MCSFinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            mcsprogress.Style = ProgressBarStyle.Continuous;
            mcsprogress.Minimum = 0;
            mcsprogress.Step = 1;
            mcsprogress.Value = 0;

            dcmprogress.Style = ProgressBarStyle.Continuous;
            dcmprogress.Minimum = 0;
            dcmprogress.Step = 1;
            dcmprogress.Value = 0;
            
            foreach (var item in searchCheckBox.Items)
                searchset.Add(item.ToString(), false);

            foreach (var item in resultCheckBox.Items)
                resultset.Add(item.ToString(), false);
        }

        Dictionary<string, bool> searchset = new Dictionary<string, bool>();
        Dictionary<string, bool> resultset = new Dictionary<string, bool>();

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lstResultView.Items.Count < 1)
            {
                MessageBox.Show("목록이 비었습니다.");
                return;
            }

            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Add();            // Sheet1이 포함된 빈 workbook 생성
            Excel.Worksheet ws1 = wb.Worksheets.Item["Sheet1"];       // Sheet1 가져오기
            
            // Columns Header
            int colIndex = 1;
            foreach (ColumnHeader column in lstResultView.Columns)
            {
                ws1.Cells[1, colIndex] = column.Text;
                colIndex++;
            }


            // 테이블 구성
            int rowCount = lstResultView.Items.Count;
            int colCount = lstResultView.Columns.Count;
            for (int row = 1; row <= rowCount; row++)
            {
                for (int col = 1; col <= colCount; col++)
                    ws1.Cells[row + 1, col] = lstResultView.Items[row - 1].SubItems[col - 1].Text;
            }
            
            Excel.Range range = ws1.Range[ws1.Cells[1, 1], ws1.Cells[rowCount + 1, colCount]];
            range.EntireColumn.AutoFit();

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
            if (!checkDCM.Checked && !checkMCS.Checked)
            {
                MessageBox.Show("검색할 파일을 먼저 선택하세요");
                return;
            }

            // 폴더브라우저 객체 생성
            using (var dir = new FolderBrowserDialog())
            {
                DialogResult result = dir.ShowDialog();
                if (result == DialogResult.OK && !String.IsNullOrEmpty(dir.SelectedPath))
                {
                    this.txtDir.Text = dir.SelectedPath.Trim(); //선택한 디렉토리 경로
                                                                // mcs, dcm 검색
                    
                    if (checkMCS.Checked)
                        FindFile(txtDir.Text.Trim(), "*.mcs");

                    if (checkDCM.Checked)
                        FindDCM(txtDir.Text.Trim(), "*.dcm");
                }
            }
        }

        private List<string> FindSet(string inputItem, string input, string outputItem)
        {
            var inputDCM = dcmdata.SearchDCM(inputItem, input).ToList();
            var outputDCM = inputDCM.SearchDCM(outputItem).ToList();
            if (outputDCM.Count == 0)
            {
                var inputMCS = mcsdata.SearchMCS(inputItem, input).ToList();
                var outputMCS = inputMCS.SearchMCS(outputItem).ToList();
                return outputMCS;
            }
            else
                return outputDCM;
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 검색을 위한 체크
            if (txtDir.Text.Trim().Equals(""))
            {
                MessageBox.Show("검색할 디렉토리를 입력해주세요.");
                return;
            }

            if (!searchset.ContainsValue(true))
            {
                MessageBox.Show("검색 대상을 선택하세요.");
                return;
            }

            if (!resultset.ContainsValue(true))
            {
                MessageBox.Show("찾을 결과를 선택하세요.");
                return;
            }

            if (String.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                MessageBox.Show("검색어를 입력해주세요.");
                return;
            }
            else
            { 
                string input = txtSearch.Text.Trim();
                string inputItem = String.Empty;
                string outputItem = String.Empty;
                List<string> output = new List<string>();

                // input matching data
                foreach (var s in searchCheckBox.CheckedItems)
                    inputItem = s.ToString();
                foreach (var s in resultCheckBox.CheckedItems)
                    outputItem = s.ToString();

                output = FindSet(inputItem, input, outputItem);

                if (output.Count == 0)
                {
                    MessageBox.Show("찾는 결과가 없습니다.");
                    return;
                }

                lstResultView.Clear();
                lstResultView.Columns.Add(inputItem, 150);
                lstResultView.Columns.Add(outputItem, 150);
                foreach (var val in output)
                {
                    ListViewItem item = new ListViewItem(input);
                    item.SubItems.Add(val);
                    lstResultView.Items.Add(item);
                }
            }
        }

        // filepath, id, name, nameKr, birth, date, filesize)
        List<MCS> mcsdata = new List<MCS>();
        private void FindFile(string dir, string word)
        {
            // 파일 목록 저장
            // 선택한 directory의 하위 dir 파일까지 모두 검색함
            string[] files = Directory.GetFiles(dir, word, SearchOption.AllDirectories);
            
            mcsprogress.Maximum = files.Length;

            // 파일 개수만큼 리스트에 추가
            for (int i = 0; i < files.Length; i++)
            {
                mcsprogress.PerformStep();
                FileInfo fileInfo = new FileInfo(files[i]);
                string name = Path.GetFileNameWithoutExtension(fileInfo.Name);
                string date = String.Empty;
                string id = String.Empty;

                // 숫자 매칭
                string number = Regex.Match(name, "[0-9]{8,}").Value;
                if (number.Length == 8)
                    date = number.Substring(0, 8);
                else
                    date = number;
                
                // 글자 매칭
                string text = Regex.Match(name, @"[^0-9\-]+(\s|_)?[^(0-9)]+").Value;
                if (text.Length != 0)
                    name = decodeKR(text);
                else
                    name = String.Empty;

                // patient list 생성
                mcsdata.Add(new MCS(fileInfo.FullName, date, id, name, fileInfo.Length.ToString()));
            }

            // Dataset
            var dataset = mcsdata
                .Where(patient => !String.IsNullOrEmpty(patient.data[1]) && !String.IsNullOrEmpty(patient.data[3]))
                .Select(patient => patient).ToList();

            // filling list
            var fill = mcsdata
                .Select(patient => {
                    var _data = dataset.Where(d => (d.data[1] == patient.data[1]) || (d.data[3] == patient.data[3])).Take(1).ToList();
                    if (_data.Count() != 0 )
                    {
                        patient.data[1] = _data[0].data[1];     // birth
                        patient.data[3] = _data[0].data[3];     // name
                    }
                    return patient;
                }).ToList();
            mcsdata = fill;
        }

        static DicomTag[] dcmTags = {
                DicomTag.PatientID,
                DicomTag.PatientName,
                DicomTag.PatientBirthDate,
                DicomTag.StudyDate
        };
        List<DCM> dcmdata = new List<DCM>();
        private void FindDCM(string dir, string word)
        {
            // 파일 목록 저장
            // 선택한 directory의 하위 dir 파일까지 모두 검색함
            string[] files = Directory.GetFiles(dir, word, SearchOption.AllDirectories);
            string[] tags = new string[dcmTags.Length];
            

            dcmprogress.Maximum = files.Length;
            for (int i = 0; i < files.Length; i++)
            { 
                dcmprogress.PerformStep();
                FileInfo fileInfo = new FileInfo(files[i]);

                // Make DCM File Dataset
                var file = DicomFile.Open(fileInfo.FullName);
                if (file != null)
                {
                    // Get Tags
                    for (int t = 0; t < dcmTags.Length; t++)
                    {
                        try
                        {
                            tags[t] = file.Dataset.GetValue<string>(dcmTags[t], 0);
                        }
                        catch
                        {
                            tags[t] = "";
                        }
                    }
                    tags[1] = decodeKR(tags[1]);    // 한글이름 디코딩
                    tags[1] = tags[1].Replace('^', ' ');
                    
                    if (i == 0)
                        dcmdata.Add(new DCM(tags[0], tags[1], tags[2], tags[3], Path.GetDirectoryName(fileInfo.FullName)));
                    else
                    {
                        // 두 번째 data부터는 id, 이름, 생년월일, 촬영일자가 다른 데이터만 추가시킴
                        var result = dcmdata
                            .Where(d => d.data[0].Equals(tags[0]) && d.data[1].Equals(tags[1]) && d.data[2].Equals(tags[2]) && d.data[3].Equals(tags[3]))
                            .ToList();

                        if (result.Count < 1)
                            dcmdata.Add(new DCM(tags[0], tags[1], tags[2], tags[3], Path.GetDirectoryName(fileInfo.FullName)));
                    }
                }
            }               
        }

        public string decodeKR(string name)
        {
            // 한글 디코딩
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            Decoder euckr = Encoding.GetEncoding(51949).GetDecoder();
            byte[] isoByte = iso.GetBytes(name);
            char[] decodename;
            int charCount = euckr.GetCharCount(isoByte, 0, isoByte.Length);
            decodename = new char[charCount];
            int charDecodedCount = euckr.GetChars(isoByte, 0, isoByte.Length, decodename, 0);
            return new string(decodename);
        }

        public enum Language
        {
            Korean = 0,
            English = 1,
            Number = 2,
            Unknown = 3
        }

        public Language languageType(string input)
        {
            if (Regex.IsMatch(input, "[가-힣]{2,4}"))
                return Language.Korean;
            else if (Regex.IsMatch(input, @"[a-zA-Z]+(\s|_)?[a-zA-Z]+"))
                return Language.English;
            else if (Regex.IsMatch(input, "[0-9]+"))
                return Language.Number;
            else
                return Language.Unknown;
        }
        
        private void searchCheckBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < searchCheckBox.Items.Count; i++)
                {
                    if (i != e.Index)
                        searchCheckBox.SetItemChecked(i, false);    // 다른 것들 체크 해제
                }
                searchset[searchCheckBox.Items[e.Index].ToString()] = true;
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                searchset[searchCheckBox.Items[e.Index].ToString()] = false;
            }
        }

        private void resultCheckBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < resultCheckBox.Items.Count; i++)
                {
                    if (i != e.Index)
                        resultCheckBox.SetItemChecked(i, false);    // 다른 것들 체크 해제
                }
                resultset[resultCheckBox.Items[e.Index].ToString()] = true;
            }
            else if (e.NewValue == CheckState.Unchecked)
                resultset[resultCheckBox.Items[e.Index].ToString()] = false;
        }
    }
}
