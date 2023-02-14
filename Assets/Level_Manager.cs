using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Furkan;

public class Level_Manager : MonoBehaviour
{
    public Button[] Buttons;
    Mmemory_Managment _MemoryManagment = new Mmemory_Managment();
    public int level;
    public Sprite sprite;
    void Start()
    {
       
        int currentLevel = _MemoryManagment.DataLoad_Int("LastLevel") - 4;
        int index = 1;
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (i+1<=currentLevel)
            {
                Buttons[i].GetComponentInChildren<Text>().text = index.ToString();
                int sceneIndex = index + 4;
                Buttons[i].onClick.AddListener(delegate { SceneLoad(sceneIndex); });               
                //delegate ile sceneLoad fonksiyonuna parametre göndeririz.
                //onClick script tarafında button tıklanmasının alınmasıdır.
            }
            else
            {
                Buttons[i].GetComponent<Image>().sprite = sprite;
                //Buttons[i].interactable = false; //bu şekilde bir yönetem kullaabilriz.ancak bu yapı silik görünüm verir.
                Buttons[i].enabled = false;
            }
            index++;
        }
    }
    public void SceneLoad(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }


    public void TurnBack()
    {
        SceneManager.LoadScene(0);//ana menu ındex=0
    }
}
