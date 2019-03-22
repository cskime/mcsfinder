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
using Dicom;
using MCSFinder;
using Excel = Microsoft.Office.Interop.Excel;
using Manager;

//SearchDCM()
//{ CMlist.SearchCOlujnmn("이름").equal("dd")
//    }

namespace Manager
{
    public static class Iquery
    {
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

        //public static IEnumerable<Iqueryable> Fill(this IEnumerable<Iqueryable> dataset, IEnumerable<Iqueryable> origin)
        //{

        //}
    }

    public class Patient
    {
        List<MCS> mcsset;
        List<DCM> dcmset;

        public Patient(List<MCS> mcsset, List<DCM> dcmset)
        {
            this.mcsset = mcsset;
            this.dcmset = dcmset;
        }
    }

    public interface Iqueryable
    {
        string GetData(string item);
        Dictionary<string, string> GetDataset();
        bool ExistColumn(string item);
    }

    public class MCS : Iqueryable
    {
        public string[] data = new string[5];
        public Dictionary<string, string> dataset = new Dictionary<string, string>();

        public MCS(string filepath, string studydate, string id, string name, string filesize)
        {
            data[0] = filepath;
            data[1] = studydate;
            data[2] = id;
            data[3] = name;
            data[4] = filesize;

            dataset["이름"] = name;
            dataset["ID"] = id;
            dataset["생년월일"] = null;
            dataset["촬영일자"] = studydate;
            dataset["mcs 경로"] = filepath;
            dataset["dcm 경로"] = null;
            dataset["용량"] = filesize;
        }

        public string GetData(string item)
        {
            string value = dataset[item];
            return String.IsNullOrEmpty(value) ? "" : dataset[item];
        }

        public Dictionary<string, string> GetDataset()
        {
            return dataset;
        }

        public bool ExistColumn(string item)
        {
            if (dataset.ContainsKey(item))
                return true;
            else
                return false;
        }
    }

    public class DCM : Iqueryable
    {
        public string[] data = new string[5];
        public Dictionary<string, string> dataset = new Dictionary<string, string>();

        public DCM(string id, string name, string birth, string studydate, string dcmpath)
        {
            data[0] = id;
            data[1] = name;
            data[2] = birth;
            data[3] = studydate;
            data[4] = dcmpath;

            dataset["이름"] = name;
            dataset["ID"] = id;
            dataset["생년월일"] = birth;
            dataset["촬영일자"] = studydate;
            dataset["mcs 경로"] = null;
            dataset["dcm 경로"] = dcmpath;
            dataset["용량"] = null;
        }

        public string GetData(string item)
        {
            string value = dataset[item];
            return String.IsNullOrEmpty(value) ? "" : dataset[item];
        }

        public Dictionary<string, string> GetDataset()
        {
            return dataset;
        }


        public bool ExistColumn(string item)
        {
            if (dataset.ContainsKey(item))
                return true;
            else
                return false;
        }
    }
}