using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackHoleSkillController : MonoBehaviour
{
    
     private float maxSize;
     private float growSpeed;
     private float smallerSpeed;
    
    private float cloneAttackCd;
    private float cloneAttackAmount;
    private float cloneAttackTimer;
    private bool isClone;
    private bool isSmaller;
    private bool canCreatHotKey=true;
    private bool isOpen = true;
    
    [SerializeField]private GameObject hotKeyPreFab;
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
            PlayerManager.instance.player.Transprent(true);
        }

        if (isOpen && !isSmaller)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }

        CloneAttack();

        if (isSmaller)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), smallerSpeed * Time.deltaTime);
            if (transform.localScale.x < 0) Destroy(gameObject);
        }

    }

    public void SetUpBlackHole(float _maxSize,float _growSpeed,float _smallerSpeed,float _cloneAttackCd,float _cloneAttackAmount)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        smallerSpeed = _smallerSpeed;
        cloneAttackCd = _cloneAttackCd;
        cloneAttackAmount = _cloneAttackAmount;
    }


    private void CloneAttack()
    {
        if (cloneAttackTimer < 0 && isClone)
        {
            cloneAttackTimer = cloneAttackCd;

            int index = Random.Range(0, enemyList.Count);

            if (cloneAttackAmount > 0)
                SkillManager.instance.cloneSkill.ClonePrefab(enemyList[index], 0);

            cloneAttackAmount--;
            if (cloneAttackAmount <= 0)
            {
                Invoke("BlackHoleFinish", 0.5f);
            }
        }
    }

    private void BlackHoleFinish()
    {
        PlayerManager.instance.player.stateMachine.ChangeState(PlayerManager.instance.player.fallState);
        isClone = false;
        isSmaller = true;
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
            collision.GetComponent<Enemy>().FreezeTime(true);

            HotKeyCreat(collision);
        }

    }

    private void OnTriggerExit2D(Collider2D collision) => collision.GetComponent<Enemy>()?.FreezeTime(false);
    

    private void HotKeyCreat(Collider2D collision)
    {
        if (keyCodes.Count <= 0)
        {
            Debug.Log("ÈÈ¼ü²»¹»ÓÃ£¡£¡£¡");
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
