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


namespace MCSFinder
{ 
    public static class MCSFinder
    {
        public static IEnumerable<DCM> SearchDCM(this IEnumerable<DCM> dataset, string item, string input)
        {
            return dataset.Where(d => d.getData(item).Equals(input));
        }

        public static IEnumerable<MCS> SearchMCS(this IEnumerable<MCS> dataset, string item, string input)
        {
            return dataset.Where(d => d.getData(item).Equals(input)).ToList();
        }

        public static IEnumerable<string> SearchDCM(this IEnumerable<DCM> dataset, string item)
        {
            return dataset.Select(dcm => dcm.getData(item)).ToList();
        }

        public static IEnumerable<string> SearchMCS(this IEnumerable<MCS> dataset, string item)
        {
            return dataset.Select(dcm => dcm.getData(item)).ToList();
        }
    }
}


namespace Manager
{
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

    interface DataStorage
    {
        string getData(string item);
    }

    public class MCS : DataStorage
    {
        public string[] data = new string[5];
        Dictionary<string, string> dataset = new Dictionary<string, string>();

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

        public string getData(string item)
        {
            return dataset[item];
        }
    }

    public class DCM : DataStorage
    {
        public string[] data = new string[5];
        Dictionary<string, string> dataset = new Dictionary<string, string>();

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

        public string getData(string item)
        {
            return dataset[item];
        }
    }
}