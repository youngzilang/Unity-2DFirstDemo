using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    [Header("受击材质")]
    [SerializeField]private Material hitMaterial;
    private Material originMaterial;

    [Header("受击时长")]
    [SerializeField] private float hitTime;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originMaterial = spriteRenderer.material;
    }

    private  IEnumerator Fx()
    {
        spriteRenderer.material = hitMaterial;

        yield return new WaitForSeconds(hitTime);

        spriteRenderer.material = originMaterial;
    }

    private void ColorFlash()
    {
        if (spriteRenderer.color != Color.white) spriteRenderer.color = Color.white;
        else spriteRenderer.color = Color.red;
    }

    private void CancleFlash()
    {
        CancelInvoke();
        spriteRenderer.color = Color.white;
    }


}
