using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField]private GameObject characterUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject skillUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject inGameUI;

    public ItemToolTipUI tipUI;
    public StatToolTipUI statTipUI;
    public CraftWindowUI craftWindow;
    public SkillToolTipUI skillTipUI;

    private void Awake()
    {
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
            transform.GetChild(i).gameObject.SetActive(false);
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
        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf) return;
        }
        SwitchTo(inGameUI);
    }
}
