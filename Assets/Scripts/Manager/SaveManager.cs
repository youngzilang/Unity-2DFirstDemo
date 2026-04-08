using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private GameData gameData;

    private List<ISaveManager> saveManagers;

    private FileDataHandler dataHandler;

    private string fileName="gameData";

    [SerializeField]private bool encryptData;

    public event Action OnGameLoaded;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
       instance = this;

        saveManagers = FindAllSaveManagers();

        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName,encryptData);

        Invoke("LoadGame", .1f);
    }

    private void Start()
    {
        
    }

    [ContextMenu("删除存档数据!")]
    public void DeleteGameData()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName,encryptData);
        dataHandler.Delete();
    }


    public void NewGame()
    {
        gameData =new GameData();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();

        if (gameData == null)
        {
            Debug.Log("没有找到存档!");
            NewGame();
        }

        foreach(ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }

        OnGameLoaded?.Invoke();
    }

    public void SaveGame()
    {
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagers);
    }

    public bool HaveDataOrNot()
    {
        if (dataHandler.Load() != null) return true;
        return false;
    }

}
