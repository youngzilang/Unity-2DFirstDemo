using System;
using System.IO;
using UnityEngine;

public class FileDataHandler 
{
    private string dataDirPath = "";
    private string dataFileName = "";

    private bool encryptData = false;
    private readonly string key = "Young";

    public FileDataHandler(string _path, string _fileName,bool _encryptData)
    {
        dataDirPath = _path;
        dataFileName = _fileName;
        encryptData = _encryptData;
    }

    public void Save(GameData _data)
    {
        string dataPath = Path.Combine(dataDirPath, dataFileName);
        
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dataPath));

            string dataToStore = JsonUtility.ToJson(_data, true);

            //if (encryptData) dataToStore = EncryptAndDecrypt(dataToStore);

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

                //if (encryptData) dataToLoad = EncryptAndDecrypt(dataToLoad);

                data = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Load data error to path:!"+dataPath +"\n"+e);
            }

        }

        return data;
    }

    public void Delete()
    {
        string dataPath = Path.Combine(dataDirPath, dataFileName);

        if (File.Exists(dataPath)) File.Delete(dataPath);
    }

    public string EncryptAndDecrypt(string _data)
    {
        string transformData = "";

        for (int i = 0; i < _data.Length; i++)
        {
            transformData += _data[i] ^ key[i % key.Length];
        }

        return transformData;
    }
}
