using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private PlayerStat myStat;
    [SerializeField] private Slider slider;

    [SerializeField] private Image dashImage;
    [SerializeField] private Image parryImage;
    [SerializeField] private Image crystalImage;
    [SerializeField] private Image swordImage;
    [SerializeField] private Image blackHoleImage;
    [SerializeField] private Image flaskImage;

    [Header("Áé»ê")]
    [SerializeField] private TextMeshProUGUI currentSoul;
    [SerializeField] private float soulAmount;
    [SerializeField] private float increaseRate;
    private void Start()
    {
        if (myStat) myStat.onHPChange += UpdateHp;
    }

    private void Update()
    {
        UpdateSoul();

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dashSkill.dashUnlock) SetCd(dashImage);
        if (Input.GetKeyDown(KeyCode.C) && SkillManager.instance.parrySkill.parryUnlock) SetCd(parryImage);
        if (Input.GetKeyDown(KeyCode.F) && SkillManager.instance.crystalSkill.crystalUnlock) SetCd(crystalImage);
        if (Input.GetKeyDown(KeyCode.Mouse1) && SkillManager.instance.swordSkill.swordUnlock) SetCd(swordImage);
        if (Input.GetKeyDown(KeyCode.R) && SkillManager.instance.blackHoleSkill.blackHoleUnlock) SetCd(blackHoleImage);
        if (Input.GetKeyDown(KeyCode.Alpha1) && Inventory.instance.GetEquipmentByType(EquipmentType.Flask)) SetCd(flaskImage);

        CheckCd(dashImage, SkillManager.instance.dashSkill.cd);
        CheckCd(parryImage, SkillManager.instance.parrySkill.cd);
        CheckCd(crystalImage, SkillManager.instance.crystalSkill.cd);
        CheckCd(swordImage, SkillManager.instance.swordSkill.cd);
        CheckCd(blackHoleImage, SkillManager.instance.blackHoleSkill.cd);
        CheckCd(flaskImage, Inventory.instance.flaskTime);
    }

    private void UpdateSoul()
    {
        if (PlayerManager.instance.GetCurrency() > soulAmount) soulAmount += Time.deltaTime * increaseRate;
        else soulAmount = PlayerManager.instance.GetCurrency();

        currentSoul.text = ((int)soulAmount).ToString();
    }

    private void UpdateHp()
    {
        slider.maxValue = myStat.GetMaxHp();
        slider.value = myStat.currentHP;
    }

    private void SetCd(Image _image)
    {
        if (_image.fillAmount <= 0) _image.fillAmount = 1;
    }

    private void CheckCd(Image _image,float _cd)
    {
        if (_image.fillAmount > 0)
        {
            _image.fillAmount -= 1 / _cd * Time.deltaTime;
        }
    }

}
