using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackHoleHotKeyController : MonoBehaviour
{
    private KeyCode hotKey;
    private TextMeshProUGUI textMesh;

    private Transform enemy;
    private BlackHoleSkillController skillController;
    private SpriteRenderer spriteRenderer;

    public void SetUpHotKey(KeyCode _hotKey,Transform _enemy,BlackHoleSkillController _skillController)
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        enemy = _enemy;
        skillController = _skillController;
        hotKey = _hotKey;
        textMesh.text = _hotKey.ToString();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(hotKey))
        {
            skillController.AddEnemy(enemy);

            textMesh.color = Color.clear;
            spriteRenderer.color = Color.clear;
        }
    }
}
