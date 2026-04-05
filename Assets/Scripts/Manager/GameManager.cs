using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour,ISaveManager
{
    public static GameManager instance;
    [SerializeField] private CheckPoint[] checkPoints;

    private void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        else instance = this;
    }

    private void Start()
    {
        checkPoints = FindObjectsOfType<CheckPoint>();
    }

    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData _data)
    {
        foreach(KeyValuePair<string,bool> keyValuePair in _data.checkPoints)
        {
            foreach(CheckPoint checkPoint in checkPoints)
            {
                if (keyValuePair.Key == checkPoint.id && keyValuePair.Value == true) checkPoint.ActiveCheckPoint();
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.checkPoints.Clear();

        foreach(CheckPoint checkPoint in checkPoints)
        {
            _data.checkPoints.Add(checkPoint.id, checkPoint.active);
        }
    }
}
