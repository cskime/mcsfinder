# mcsfinder
프로젝트에서 dicom 파일을 읽는 방법, 결과를 Excel 파일로 저장하는 방법 등을 설명합니다.

## DICOM Read
dicom 파일에서 환자 정보를 가져오려면 dicom library를 사용해서 파일을 읽은 다음 태그로 해당 값을 가져와야 합니다. dicom library는 몇 가지가 있는데 여기서는 `fo-dicom`을 사용했습니다. 라이브러리는 NuGet 패키지에서 fo-dicom으로 검색하여 받을 수 있습니다.
``` c#
using Dicom;
```
> fo-dicom : https://github.com/fo-dicom/fo-dicom
> API Document : https://fo-dicom.github.io/html/632e5303-a1e0-492f-8f6a-8b78e9037c40.htm

dicom 파일은 아래 과정을 통해 읽어올 수 있습니다.
1. 디렉토리에 파일을 읽어옵니다.
``` C#
string[] files = Directory.GetFiles(path, "*.dcm", SearchOption.AllDirectories);
```
2. dicom 형식이 아니면 `DicomFile`로 열 수 없기 때문에 필터링을 거칩니다.
``` c#
files = files
    .Where(file =>
    {
        FileInfo info = new FileInfo(file);
        DicomFile dicom = DicomFile.Open(info.FullName);
        return dicom != null;
    });
```
3. `DicomFile`의 `Dataset`속성의 `GetValue<T>()` 메서드를 사용해서 값을 가져올 수 있습니다. `GetValue()`는 `DicomTag`와 `index`를 각각 인자로 받는데, `index`는 해당 tag에 해당하는 값들 중 몇 번쨰 값을 가져오는지 결정하는 값입니다. id, 이름, 날짜 같은 정보들은 tag 당 값을 하나만 가져서 `index`에 0을 넣습니다.
> dicom tag : https://fo-dicom.github.io/html/5bd45e2b-d2e5-7b18-b651-44f29938d908.htm

``` c#
...
.Select(file =>
{
    FileInfo info = new FileInfo(file);
    DicomFile dicom = DicomFile.Open(info.FullName);

    string name = dicom.Dataset.GetValue<string>(DicomTag.PatientName, 0).DecodeKorean().Replace('^', ' ');
    string id = dicom.Dataset.GetValue<string>(DicomTag.PatientID, 0);
    string birth = dicom.Dataset.GetValue<string>(DicomTag.PatientBirthDate, 0);
    string studydate = dicom.Dataset.GetValue<string>(DicomTag.StudyDate, 0);
    string filepath = Path.GetDirectoryName(info.FullName);

    ...
}).ToArray();
```

## Excel
NuGet 패키지에서 `Microsoft.Office.Interop.Excel`을 설치하면 excel 파일을 읽고 쓸 수 있습니다. `System.Windows.Form.Application`과 `Microsoft.Office.Interop.Excel.Application`을 구분해서 사용해야 합니다.
``` c#
using Excel = Microsoft.Office.Interop.Excel;
...
Excel.Application app = new Excel.Application();      // open/create excel file
Excel.Workbook wb = app.Workbooks.Add();              // create workbook
Excel.Worksheet ws1 = wb.Worksheets.Item["Sheet1"];   // open sheet
```
> Microsoft.Office.Interop.Excel Namespace :  https://docs.microsoft.com/en-us/dotnet/api/microsoft.office.interop.excel?view=excel-pia

`Worksheet`의 `Cells`를 사용하면 cell에 접근해서 데이터를 읽고 쓸 수 있습니다. `Cells`는 `Range` 타입으로 row, column index를 사용해 특정 셀에 접근할 수 있습니다. `Range`는 index를 사용하면 특정 위치의 셀을 받을 수 있고, 셀을 사용하면 시작 셀부터 끝나는 셀 까지의 범위를 선택합니다.
```C#
...
// winform의 listview를 사용했을 때 Items와 SubItems가 row, column에 대응됨
for (int row = 1; row <= rowCount; row++)
{
    for (int col = 1; col <= colCount; col++)
        ws1.Cells[row + 1, col] = lstResultView.Items[row - 1].SubItems[col - 1].Text;
}
```

파일로 저장할 때는 `SaveFileDialog`를 사용해서 저장 경로를 쉽게 선택할 수 있도록 했습니다. `SaveAs()` 메서드로 workbook을 저장하고 `Close()` 메서드로 작업이 끝난 workbook을 닫습니다.
``` c#
SaveFileDialog saveFileDlg = new SaveFileDialog();
saveFileDlg.Title = "파일 저장";
saveFileDlg.OverwritePrompt = true;
saveFileDlg.Filter = "Excel File(*.xlsx)|.xlsx";

if (saveFileDlg.ShowDialog() == DialogResult.OK)
{
    wb.SaveAs(saveFileDlg.FileName);
    wb.Close();
}
```
> Read Excel File : https://coderwall.com/p/app3ya/read-excel-file-in-c
> Save File Dialog : https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.savefiledialog?view=netframework-4.8


## Extensions
mcs 파일로부터 이름을 파싱할 때, 이름이 깨져 있는 파일이 몇 개 있습니다. `string` 타입의 이름에서 `DecodeKorean()` 함수를 사용하면 깨진 한글을 복구할 수 있습니다. 이미 한글이거나 한글이 아닌 다른 문자는 그대로 유지됩니다.
``` C#
namespace MyExtensions
{
    public static class MyExtensions
    {
        public static string DecodeKorean(this string name)
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
    }
}

// text : mcs 파일 이름에서 파싱한 텍스트
string name = text.DecodeKorean();
```
