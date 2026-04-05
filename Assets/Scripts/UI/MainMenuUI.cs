using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainScene";
    [SerializeField] private GameObject continueButton;
    [SerializeField] private FadeScreenUI fadeScreen;

    private void Start()
    {
        if (SaveManager.instance.HaveDataOrNot() == false) continueButton.SetActive(false);
    }

    public void ContinueGame()
    {
        StartCoroutine(LoadScreenWithFade(1.5f));
    }

    public void NewGame()
    {
        SaveManager.instance.DeleteGameData();
        StartCoroutine(LoadScreenWithFade(1.5f));
    }

    public void ExitGame()
    {

    }

    IEnumerator LoadScreenWithFade(float _delay)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(sceneName);
    }
}
