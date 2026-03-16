using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    [SerializeField] private float cloneTransparentSpeed;
    private SpriteRenderer sr;
    private float cloneTimer;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (cloneTransparentSpeed * Time.deltaTime));
        }
    }

    public void SetUpClone(Transform clonePosition,float _cloneTimer)
    {
        transform.position = clonePosition.position;
        cloneTimer = _cloneTimer;
    }
}
