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
        _MemoryManagment.DataSave_Int("LastLevel", level);
        int currentLevel = _MemoryManagment.DataLoad_Int("LastLevel") - 4;
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (i+1<=currentLevel)
            {
                Buttons[i].GetComponentInChildren<Text>().text = (i + 1).ToString();

            }
            else
            {
                Buttons[i].GetComponent<Image>().sprite = sprite;
                //Buttons[i].interactable = false; //bu şekilde bir yönetem kullaabilriz.ancak bu yapı silik görünüm verir.
                Buttons[i].enabled = false;
            }
        }
    }

    
    public void TurnBack()
    {
        SceneManager.LoadScene(0);//ana menu ındex=0
    }
}
