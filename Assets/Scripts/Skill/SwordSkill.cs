using System;
using UnityEngine;

public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}

public class SwordSkill : Skill
{
    [Header("飞剑类型")]
    public SwordType swordType=SwordType.Regular;

    [Header("反弹飞剑")]
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceG;

    [Header("贯穿飞剑")]
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceG;

    [Header("飞刀数据")]
    [SerializeField] private GameObject swordPreFab;
    [SerializeField] private Vector2 launchDirection;
    [SerializeField] private float swordGravity;


    private Vector2 swordFinalDirection;

    [Header("抛物线数据")]
    [SerializeField] private int dotNum;
    [SerializeField] private float dotBetween;
    [SerializeField] private GameObject dotPreFab;
    [SerializeField] private Transform dotParent;

    private GameObject[] dots;
    public GameObject swordOnly;

    protected override void Start()
    {
        base.Start();

        DotsCreat();

        SetSwordG();
    }

    private void SetSwordG()
    {
        switch (swordType)
        {
            case SwordType.Bounce:swordGravity = bounceG;break;
            case SwordType.Pierce:swordGravity = pierceG;break;
        }
    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Vector2 aim = AimDirection().normalized;
            swordFinalDirection = new Vector2(aim.x * launchDirection.x, aim.y * launchDirection.y);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * dotBetween);
            }
        }
    }

    public void OnlySword(GameObject onlySword)
    {
        swordOnly = onlySword;
    }

    public void DestroyMoreSword()
    {
        player.stateMachine.ChangeState(player.holdState);
        Destroy(swordOnly);
    }

    public void CreatSword()
    {
        GameObject newSword = Instantiate(swordPreFab, player.transform.position, player.transform.rotation);
        SwordSkillController swordSkillController = newSword.GetComponent<SwordSkillController>();


        switch (swordType)
        {
            case SwordType.Bounce:swordSkillController.SetUpBounce(true, bounceAmount);break;
            case SwordType.Pierce:swordSkillController.SetUpPierce(pierceAmount); break;
        }
            
        

        OnlySword(newSword);
        swordSkillController.SetUpSword(swordFinalDirection, swordGravity, player);
        DotsActive(false);
    }

    #region Aim
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void DotsCreat()
    {
        dots = new GameObject[dotNum];
        for (int i = 0; i < dotNum; i++)
        {
            dots[i] = Instantiate(dotPreFab, player.transform.position, Quaternion.identity, dotParent);
            dots[i].SetActive(false);
        }



    }

    public void DotsActive(bool isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(isActive);
        }
    }

    public Vector2 DotsPosition(float t)
    {
        Vector2 aim = AimDirection().normalized;

        Vector2 initVelocity = new Vector2(aim.x * launchDirection.x, aim.y * launchDirection.y);
        float gY = Physics2D.gravity.y * swordGravity;
        float deltaX = initVelocity.x * t;
        float deltaY = initVelocity.y * t + .5f * gY * (t * t);

        Vector2 finalPosition = (Vector2)player.transform.position + new Vector2(deltaX, deltaY);
        return finalPosition;
    }
    #endregion
}
