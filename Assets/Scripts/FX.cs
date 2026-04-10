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

    [Header("打击效果")]
    [SerializeField] private GameObject hitFX;
    [SerializeField] private GameObject criticalHitFX;
    private GameObject currentHitFX;

    [Space]
    [Header("剑的灰尘效果")]
    [SerializeField] private ParticleSystem dustFX;

    [Space]
    [Header("角色残影视觉")]
    [SerializeField]private GameObject afterImagePrefab;
    [SerializeField]private float colorLoseRate;
    [SerializeField]private float afterImageCd;
    private float afterImageTimer;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originMaterial = spriteRenderer.material;
    }

    private void Update()
    {
        afterImageTimer-= Time.deltaTime;
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

    public void CreatHitFX(Transform _target,bool _critical)
    {
        float zRotation = Random.Range(-90, 90);
        float yRotation = 0;
        float xPosition = Random.Range(-.5f, .5f);
        float yPosition = Random.Range(-.5f, .5f);

        Vector3 hitRotation = new Vector3(0, 0, zRotation);

        currentHitFX = hitFX;

        if (_critical)
        {
            zRotation = Random.Range(-45, 45);

            if (GetComponent<Entity>().faceDir == 1) yRotation = 180;

            hitRotation = new Vector3(0, yRotation, zRotation);

            currentHitFX = criticalHitFX;
        }

        GameObject newHit = Instantiate(currentHitFX, _target.position+new Vector3(xPosition,yPosition,0), Quaternion.identity);

         newHit.transform.Rotate(hitRotation);
        

        Destroy(newHit, .5f);
    }

    public void CreatAfterImage()
    {
        if (afterImageTimer < 0)
        {
            afterImageTimer = afterImageCd;
            GameObject afterImage = Instantiate(afterImagePrefab, transform.position,transform.rotation);
            afterImage.GetComponent<AfterImageFX>().SetUpAfterImage(spriteRenderer.sprite, colorLoseRate);
        }

       
    }

    public void PlayDustFX()
    {
        if(dustFX!= null) dustFX.Play();
    }
}
