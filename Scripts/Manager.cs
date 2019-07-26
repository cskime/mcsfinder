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

//SearchDCM()
//{ CMlist.SearchCOlujnmn("이름").equal("dd")
//    }

public interface Iqueryable
{
    string GetData(string item);
    Dictionary<string, string> GetDataset();
    bool IsExist(string item);
}

public class MCS : Iqueryable
{
    public Dictionary<string, string> dataset = new Dictionary<string, string>();

    public MCS(string filepath, string studydate, string id, string name, string filesize)
    {
        dataset["이름"] = name;
        dataset["ID"] = id;
        dataset["촬영일자"] = studydate;
        dataset["mcs 경로"] = filepath;
        dataset["용량"] = filesize;
    }

    public string GetData(string item)
    {
        string value;
        bool isExist = dataset.TryGetValue(item, out value);
        return isExist ? value : string.Empty;
    }

    public void SetData(string item, string value)
    {
        if (dataset.ContainsKey(item))
            dataset[item] = value;
    }

    public Dictionary<string, string> GetDataset()
    {
        return dataset;
    }

    public bool IsExist(string item)
    {
        if (dataset.ContainsKey(item))
            return true;
        else
            return false;
    }
}

public class DCM : Iqueryable
{
    public Dictionary<string, string> dataset = new Dictionary<string, string>();

    public DCM(string id, string name, string birth, string studydate, string dcmpath)
    {
        dataset["이름"] = name;
        dataset["ID"] = id;
        dataset["생년월일"] = birth;
        dataset["촬영일자"] = studydate;
        dataset["dcm 경로"] = dcmpath;
    }

    public string GetData(string item)
    {
        string value;
        bool isExist = dataset.TryGetValue(item, out value);
        return isExist ? value : string.Empty;
    }

    public Dictionary<string, string> GetDataset()
    {
        return dataset;
    }


    public bool IsExist(string item)
    {
        if (dataset.ContainsKey(item))
            return true;
        else
            return false;
    }
}