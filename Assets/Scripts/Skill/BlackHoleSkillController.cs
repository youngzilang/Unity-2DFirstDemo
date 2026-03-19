using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackHoleSkillController : MonoBehaviour
{
    [Header("黑洞信息")]
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private bool isOpen;
    [SerializeField] private float freezrContinue;

    [Space]
    [SerializeField] private GameObject hotKeyPreFab;
    [SerializeField] private List<KeyCode> keyCodes;

    private List<Transform> enemyList= new List<Transform>();
    private void Update()
    {
        if (isOpen)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize,maxSize), growSpeed*Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>()!=null)
        {
            collision.GetComponent<Enemy>().StartCoroutine("FreezeTimeFor", freezrContinue);

            HotKeyCreat(collision);
        }

    }

    private void HotKeyCreat(Collider2D collision)
    {
        if (keyCodes.Count <= 0)
        {
            Debug.Log("热键不够用！！！");
            return;
        }

        GameObject newButton = Instantiate(hotKeyPreFab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);

        KeyCode keyCode = keyCodes[Random.Range(0, keyCodes.Count)];
        keyCodes.Remove(keyCode);

        BlackHoleHotKeyController blackHoleHotKeyController = newButton.GetComponent<BlackHoleHotKeyController>();

        blackHoleHotKeyController.SetUpHotKey(keyCode, collision.transform, this);
    }

    public void AddEnemy(Transform transform)
    {
        enemyList.Add(transform);
    }
}
