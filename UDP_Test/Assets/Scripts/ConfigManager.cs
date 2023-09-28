using UnityEngine;
using System.IO;

public class ConfigManager : MonoBehaviour
{
    private static ConfigManager _instance;
    public static ConfigManager GetInstance() => _instance;
    string configFileName = "/Config" + "\\config.txt";
    void Awake()
    {
        //DontDestroyOnLoad(this);
        if (_instance == null) { _instance = this; }
        else
        {
            Destroy(this);
        }
    }


    #region Getters
    public int GetValue(string p_id)
    {
        string path = $"{Application.streamingAssetsPath}\\{configFileName}";
        StreamReader reader = new StreamReader(path);

        int m_count = File.ReadAllLines(path).Length;

        string[] linesRead = File.ReadAllLines(path);

        foreach (var line in linesRead)
        {
            line.Trim(' ');
            var lineSplit = line.Split('=');

            if (lineSplit[0].Equals(p_id) || lineSplit[0].Contains(p_id))
            {
                return int.Parse(lineSplit[1]);
            }
        }
        reader.Close();
        Debug.LogAssertion("No Data Found");
        return -1;
    }

    public bool GetBool(string p_id)
    {
        string path = $"{Application.streamingAssetsPath}\\{configFileName}"; StreamReader reader = new StreamReader(path);

        int m_count = File.ReadAllLines(path).Length;
        string[] linesRead = File.ReadAllLines(path);

        foreach (var line in linesRead)
        {
            line.Trim(' ');
            var lineSplit = line.Split('=');

            if (lineSplit[0].Equals(p_id) || lineSplit[0].Contains(p_id))
            {
                return lineSplit[1].Equals("true");
            }
        }
        reader.Close();
        Debug.LogAssertion("No Data Found");
        return false;
    }

    public string GetStringValue(string p_id)
    {
        string path = $"{Application.streamingAssetsPath}\\{configFileName}";
        StreamReader reader = new StreamReader(path);

        int m_count = File.ReadAllLines(path).Length;

        string[] linesRead = File.ReadAllLines(path);

        foreach (var line in linesRead)
        {
            line.Trim(' ');
            var lineSplit = line.Split('=');

            if (lineSplit[0].Equals(p_id) || lineSplit[0].Contains(p_id))
            {
                return lineSplit[1];
            }
        }

        reader.Close();
        Debug.LogAssertion($"No Data Found for ID:{p_id}");
        return "";
    }

    public float GetFloatValue(string p_id)
    {
        string path = $"{Application.streamingAssetsPath}\\{configFileName}";
        StreamReader reader = new StreamReader(path);

        int m_count = File.ReadAllLines(path).Length;

        string[] linesRead = File.ReadAllLines(path);

        foreach (var line in linesRead)
        {
            line.Trim(' ');
            var lineSplit = line.Split('=');
            if (lineSplit[0].Equals(p_id) || lineSplit[0].Contains(p_id))
            {
                return float.Parse(lineSplit[1]);
            }
        }
        reader.Close();
        return -1;
    }
    #endregion

    private void GenerateFile(string path)
    {
        if (!File.Exists(path))
            File.CreateText(path);
    }

    private void WriteHeader(string p_data)
    {
        StreamWriter writer = new StreamWriter(p_data, true);
        string header = $"Location,Device,Experience,Language,LogTime";
        writer.WriteLine(header);
        writer.Close();
    }

}


