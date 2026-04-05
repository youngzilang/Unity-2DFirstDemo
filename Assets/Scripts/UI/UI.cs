using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]private GameObject characterUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject skillUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject inGameUI;

    [SerializeField] public FadeScreenUI screenUI;
    [SerializeField] private GameObject dieText;
    [SerializeField] private GameObject restartButton;

    public ItemToolTipUI tipUI;
    public StatToolTipUI statTipUI;
    public CraftWindowUI craftWindow;
    public SkillToolTipUI skillTipUI;

    private List<GameObject> businessUIs;

    private void Awake()
    {
        businessUIs = new List<GameObject>()
        {
            characterUI,
            craftUI,
            skillUI,
            optionsUI,
            inGameUI
        };

        SwitchTo(skillUI);
    }

    private void Start()
    {
        SwitchTo(inGameUI);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) SwitchToWithKey(characterUI);
        if (Input.GetKeyDown(KeyCode.V)) SwitchToWithKey(skillUI);
        if (Input.GetKeyDown(KeyCode.B)) SwitchToWithKey(craftUI);
        if (Input.GetKeyDown(KeyCode.N)) SwitchToWithKey(optionsUI);
    }

    public void SwitchTo(GameObject menu)
    {

        for(int i = 0; i < transform.childCount; i++)
        {

            bool fade = transform.GetChild(i).GetComponent<FadeScreenUI>() != null;

            if(fade==false) transform.GetChild(i).gameObject.SetActive(false);

        }

        if (menu) menu.SetActive(true);
    }

    public void SwitchToWithKey(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckInGameUI();
            return;
        }

        SwitchTo(_menu);
    }

    public void CheckInGameUI()
    {
        bool hasActiveBusinessUI = false;
        // 遍历业务UI列表，判断是否有激活的
        foreach (var ui in businessUIs)
        {
            if (ui != null && ui.activeSelf)
            {
                hasActiveBusinessUI = true;
                break;
            }
        }

        // 如果没有任何业务UI激活，显示InGameUI
        if (!hasActiveBusinessUI)
        {
            SwitchTo(inGameUI);
        }
    }

    public void SwitchOnEnd()
    {
        screenUI.FadeOut();
        StartCoroutine(EndScreen());
    }

    IEnumerator EndScreen()
    {
        yield return new WaitForSeconds(1.5f);
        dieText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        restartButton.SetActive(true);
    }

    public void RestartButton() => GameManager.instance.RestartScene();
}
