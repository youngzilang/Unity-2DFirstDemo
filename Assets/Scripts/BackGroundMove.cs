using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    private GameObject camera;

    [Header("背景跟随速度系数")]
    [SerializeField] private float backGroundMove;

    private float xPosition;
    private float length;

    private void Start()
    {
        camera = GameObject.Find("Main Camera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;


        xPosition = transform.position.x;
    }

    private void Update()
    {
        float distanceMove = transform.position.x * (1 - backGroundMove);
        float distanceMoveFllow = camera.transform.position.x * backGroundMove;

        transform.position = new Vector3(xPosition + distanceMoveFllow, transform.position.y);

        if (distanceMove > xPosition + length) xPosition = length + xPosition;
        else if (distanceMove < xPosition - length) xPosition = xPosition - length;

    }
}
