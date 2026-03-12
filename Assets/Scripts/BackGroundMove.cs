using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    private GameObject camera;

    [Header("±³¾°¸úËæËÙ¶È")]
    [SerializeField] private float backGroundMove;

    private float xPosition;

    private void Start()
    {
        camera = GameObject.Find("Main Camera");

        xPosition = transform.position.x;
    }

    private void Update()
    {
        float distanceMove = camera.transform.position.x * backGroundMove;

        transform.position = new Vector3(xPosition + distanceMove, transform.position.y);
    }
}
