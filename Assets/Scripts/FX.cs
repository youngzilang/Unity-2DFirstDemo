using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class FX : MonoBehaviour
{
    [Header("受击材质")]
    [SerializeField]private Material hitMaterial;
    private Material originMaterial;

    [Header("受击时长")]
    [SerializeField] private float hitTime;

    [Header("负面状态颜色")]
    [SerializeField] private Color[] fireColor;
    [SerializeField] private Color[] iceColor;
    [SerializeField] private Color[] lightColor;

    private SpriteRenderer spriteRenderer;

    [Header("元素反应")]
    [SerializeField] private ParticleSystem fireFX;
    [SerializeField] private ParticleSystem iceFX;
    [SerializeField] private ParticleSystem lightFX;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originMaterial = spriteRenderer.material;
    }

    public void Transprent(bool _transprent)
    {
        if (_transprent) spriteRenderer.color = Color.clear;
        else spriteRenderer.color = Color.white;
    }

    private  IEnumerator Fx()
    {
        spriteRenderer.material = hitMaterial;
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(hitTime);

        spriteRenderer.color = originalColor;
        spriteRenderer.material = originMaterial;
    }

    public void FireFor(float _seconds)
    {
        fireFX.Play();

        InvokeRepeating("FireFlash", 0, .3f);
        Invoke("CancleFlash", _seconds);
    }

    public void IceFor(float _seconds)
    {
        iceFX.Play();

        InvokeRepeating("IceFlash", 0, .3f);
        Invoke("CancleFlash", _seconds);
    }

    public void LightFor(float _seconds)
    {
        lightFX.Play();

        InvokeRepeating("LightFlash", 0, .3f);
        Invoke("CancleFlash", _seconds);
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

        fireFX.Stop();
        iceFX.Stop();
        lightFX.Stop();
    }

    private void FireFlash()
    {
        if (spriteRenderer.color != fireColor[0]) spriteRenderer.color = fireColor[0];
        else spriteRenderer.color = fireColor[1];
    }

    private void IceFlash()
    {
        if (spriteRenderer.color != iceColor[0]) spriteRenderer.color = iceColor[0];
        else spriteRenderer.color = iceColor[1];
    }

    private void LightFlash()
    {
        if (spriteRenderer.color != lightColor[0]) spriteRenderer.color = lightColor[0];
        else spriteRenderer.color = lightColor[1];
    }
}
