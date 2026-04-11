using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpTextFX : MonoBehaviour
{
    //定义出文字预制体，并且设置一个文本组件来显示伤害数值，文字向上飘动的速度，和文字消失的时间
     private TextMeshPro myText;
    [SerializeField]private float moveUpSpeed;
    [SerializeField]private float disappearSpeed;
    [SerializeField]private float lifeTime;

    private float timer;

    private void Start()
    {
        myText=GetComponent<TextMeshPro>();
        timer = lifeTime;
    }

    private void Update()
    {
        //让文字向上飘动
        transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
           float alpha = myText.color.a - disappearSpeed * Time.deltaTime;
            myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, alpha);
            if (myText.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
