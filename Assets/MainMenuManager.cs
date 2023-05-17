using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Furkan;

public class MainMenuManager : MonoBehaviour
{
    Mmemory_Managment _Memory_Managment = new Mmemory_Managment();
    Data_Managment _DataManagment = new Data_Managment();

    public List<ItemInformation> _ItemInformation = new List<ItemInformation>();
    public GameObject ExitPanel;
    public AudioSource ButtonSound;
    private void Start()
    {
        _Memory_Managment.ControlAndDefine();
        _DataManagment.InıtialSetupFileCreation(_ItemInformation); // ilk oyun açıldığında veri dosyamızı oluşturacağız.
    }
    public void LoadScene(int index)
    {
        ButtonSound.Play();
        SceneManager.LoadScene(index);
    }
    public void PlayButton()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel"));
    }
    
    public void ExitButtonPopup(string state)
    {
        ButtonSound.Play();
        if (state=="exit")
            ExitPanel.SetActive(true);
        else if (state=="ok")
            Application.Quit();
        else
            ExitPanel.SetActive(false);
        
    }
}
