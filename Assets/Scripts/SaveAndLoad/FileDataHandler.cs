using System;
using System.IO;
using UnityEngine;

public class FileDataHandler 
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string _path, string _fileName)
    {
        dataDirPath = _path;
        dataFileName = _fileName;
    }

    public void Save(GameData _data)
    {
        string dataPath = Path.Combine(dataDirPath, dataFileName);
        
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dataPath));

            string dataToStore = JsonUtility.ToJson(_data, true);

            using(FileStream stream =new FileStream(dataPath, FileMode.Create))
            {
                using(StreamWriter writer =new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error on trying to save data to file:" + dataPath + "\n" + e);
        }

    }

    public GameData Load()
    {
        string dataPath = Path.Combine(dataDirPath, dataFileName);
        GameData data = null;

        if (File.Exists(dataPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(dataPath, FileMode.Open))
                {
                    using(StreamReader reader=new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                data = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Load data error to path:!"+dataPath +"\n"+e);
            }

        }

        return data;
    }
}
