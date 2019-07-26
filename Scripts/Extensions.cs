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
using System.Text.RegularExpressions;
using Dicom;

namespace MyExtensions
{
    public static class MyExtensions
    {
        public static string DecodeKR(this string name)
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

        public static IEnumerable<string> GetColumn(this IEnumerable<Iqueryable> dataset, string item)
        {
            var result = dataset.Select(s => s.GetData(item));
            return result;
        }

        public static IEnumerable<Iqueryable> SelectRow(this IEnumerable<Iqueryable> dataset, (string text, string item) input)
        {
            var result = dataset.Where(s => s.GetData(input.item).Equals(input.text));
            return result;
        }
    }
}