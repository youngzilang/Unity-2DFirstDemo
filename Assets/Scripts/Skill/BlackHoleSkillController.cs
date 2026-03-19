using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackHoleSkillController : MonoBehaviour
{
    [Header("黑洞信息")]
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float smallerSpeed;
    [SerializeField] private bool isOpen;
    [SerializeField] private float freezrContinue;
    [SerializeField] private float cloneAttackCd;
    [SerializeField] private float cloneAttackAmount;
    private float cloneAttackTimer;
    private bool isClone;
    private bool isSmaller;
    private bool canCreatHotKey=true;

    [Space]
    [SerializeField] private GameObject hotKeyPreFab;
    [SerializeField] private List<KeyCode> keyCodes;

    private List<Transform> enemyList= new List<Transform>();
    private List<GameObject> hotKeyToDestroy=new List<GameObject>();
    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
        {
            isClone = true;
            HotKeyDestroy();
            canCreatHotKey = false;
        }

        if (isOpen&&!isSmaller)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize,maxSize), growSpeed*Time.deltaTime);
        }

        if (cloneAttackTimer < 0 && isClone)
        {
            cloneAttackTimer = cloneAttackCd;

            int index = Random.Range(0, enemyList.Count);


            SkillManager.instance.cloneSkill.ClonePrefab(enemyList[index], 0);

            cloneAttackAmount--;
            if (cloneAttackAmount <= 0)
            {
                isClone = false;
                isSmaller = true;
            }
        }

        if (isSmaller)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), smallerSpeed * Time.deltaTime);
            if (transform.localScale.x < 0) Destroy(gameObject);
        }

    }

    private void HotKeyDestroy()
    {
        if (hotKeyToDestroy.Count <= 0) return;
        for(int i = 0; i < hotKeyToDestroy.Count; i++)
        {
            Destroy(hotKeyToDestroy[i]);
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

        if (!canCreatHotKey) return;

        GameObject newButton = Instantiate(hotKeyPreFab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        hotKeyToDestroy.Add(newButton);

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
