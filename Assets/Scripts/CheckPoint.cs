using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator animator;
    public string id;
    public bool active;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    [ContextMenu("获取物品id")]
    private void GenerateId() => id=System.Guid.NewGuid().ToString();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            ActiveCheckPoint();
        }
    }

    public void ActiveCheckPoint()
    {
        active = true;
        animator.SetBool("Active", true);
    }
}
