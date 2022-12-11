using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _menuPanel;

    [SerializeField]
    private GameObject _creditsPanel;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        _menuPanel.SetActive(!_menuPanel.activeInHierarchy);
        _creditsPanel.SetActive(!_creditsPanel.activeInHierarchy);
    }


    public void Exit()
    {
        Application.Quit();
    }
}
