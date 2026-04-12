using Cinemachine;
using UnityEngine;

public class PlayerFX : FX
{
    [Header("角色残影视觉")]
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private float colorLoseRate;
    [SerializeField] private float afterImageCd;
    private float afterImageTimer;

    [Space]
    [Header("屏幕震动效果")]
    [SerializeField] private float shakeMultiplier;
    public Vector3 swordShake;
    public Vector3 criticalShake;
    private CinemachineImpulseSource impulseSource;

    [Space]
    [Header("剑的灰尘效果")]
    [SerializeField] private ParticleSystem dustFX;

    protected override void Start()
    {
        base.Start();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        afterImageTimer -= Time.deltaTime;
    }
    public void CreatAfterImage()
    {
        if (afterImageTimer < 0)
        {
            afterImageTimer = afterImageCd;
            GameObject afterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
            afterImage.GetComponent<AfterImageFX>().SetUpAfterImage(spriteRenderer.sprite, colorLoseRate);
        }
    }

    public void ScreenShake(Vector3 _shakePower)
    {
        impulseSource.m_DefaultVelocity = new Vector3(_shakePower.x * PlayerManager.instance.player.faceDir, _shakePower.y) * shakeMultiplier;
        impulseSource.GenerateImpulse();
    }


    public void PlayDustFX()
    {
        if (dustFX != null) dustFX.Play();
    }

}
