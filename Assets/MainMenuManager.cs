using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Furkan;

public class MainMenuManager : MonoBehaviour
{
    Mmemory_Managment _Memory_Managment = new Mmemory_Managment();
    public GameObject ExitPanel;
    private void Start()
    {
        _Memory_Managment.ControlAndDefine();
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel"));
    }
    
    public void ExitButtonPopup(string state)
    {
        if(state=="exit")
            ExitPanel.SetActive(true);
        else if (state=="ok")
            Application.Quit();
        else
            ExitPanel.SetActive(false);
        
    }
}
