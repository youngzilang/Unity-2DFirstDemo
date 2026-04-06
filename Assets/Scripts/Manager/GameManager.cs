using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour,ISaveManager
{
    public static GameManager instance;
    private CheckPoint[] checkPoints;

    [Header("丢失货币")]
    [SerializeField] private GameObject lostCurrencyPrefab;
    public int lostCurrencyAmount;
    [SerializeField] private float lostCurrencyX;
    [SerializeField] private float lostCurrencyY;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void RestartScene()
    {
        SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData _data)
    {
        StartCoroutine(LoadDelay(_data));
    }

    IEnumerator LoadDelay(GameData _data)
    {
        yield return new WaitForSeconds(.5f);
        FindAllCheckPoints();
        LoadLostCurrency(_data);
        LoadCheckPoints(_data);
        PlacePlayerToClosestCheckPoint(_data);
    }

    public void SaveData(ref GameData _data)
    {
        _data.lostCurrencyAmount = lostCurrencyAmount;
        _data.lostCurrencyX = PlayerManager.instance.player.transform.position.x;
        _data.lostCurrencyY = PlayerManager.instance.player.transform.position.y;
        _data.closestCheckPointId = FindClosestCheckPoint().id;
        _data.checkPoints.Clear();

        FindAllCheckPoints();

        foreach(CheckPoint checkPoint in checkPoints)
        {
            _data.checkPoints.Add(checkPoint.id, checkPoint.active);
        }
    }
    private void PlacePlayerToClosestCheckPoint(GameData _data)
    {
        if (_data.closestCheckPointId == null) return;

        //找到最近复活点位置，把玩家传送到此
        foreach (CheckPoint checkPoint in checkPoints)
        {
            if (checkPoint.id == _data.closestCheckPointId) PlayerManager.instance.player.transform.position = checkPoint.transform.position;
        }
    }
    public void FindAllCheckPoints()
    {
        checkPoints = FindObjectsOfType<CheckPoint>(true);
    }
    private void LoadLostCurrency(GameData _data)
    {
        lostCurrencyAmount = _data.lostCurrencyAmount;
        lostCurrencyX = _data.lostCurrencyX;
        lostCurrencyY = _data.lostCurrencyY;

        if (lostCurrencyAmount > 0)
        {
            GameObject newLostCurrency = Instantiate(lostCurrencyPrefab,new Vector3(lostCurrencyX,lostCurrencyY),Quaternion.identity);
            newLostCurrency.GetComponent<CurrencyLost>().currency = lostCurrencyAmount;
        }

        lostCurrencyAmount = 0;
    }
    private void LoadCheckPoints(GameData _data)
    {
        foreach (KeyValuePair<string, bool> keyValuePair in _data.checkPoints)
        {
            foreach (CheckPoint checkPoint in checkPoints)
            {
                if (keyValuePair.Key == checkPoint.id && keyValuePair.Value == true) checkPoint.ActiveCheckPoint();
            }
        }
    }
    private CheckPoint FindClosestCheckPoint()
    {
        CheckPoint closestCheckPoint = null;
        float minDistance = Mathf.Infinity;

        foreach(CheckPoint checkPoint in checkPoints)
        {
            float distance = Vector2.Distance(checkPoint.transform.position, PlayerManager.instance.player.transform.position);

            if (distance < minDistance && checkPoint.active == true)
            {
                minDistance = distance;
                closestCheckPoint = checkPoint;
            }

        }

        return closestCheckPoint;
    }

}
