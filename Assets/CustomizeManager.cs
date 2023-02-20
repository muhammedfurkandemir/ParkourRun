using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Furkan;

public class CustomizeManager : MonoBehaviour
{
    [Header("TextField")]
    public TMP_Text puanText;
    public TMP_Text capText;
    [Header("Caps")]
    public GameObject[] Caps;
    public Button[] CapButtons;
    [Header("Sticks")]
    public GameObject[] Sticks;
    [Header("Materials")]
    public Material[] Costumes;

    int capIndex = -1;//sapkanın olmaması durumu için default değer olarak -1 verdik.
    Mmemory_Managment _MemoryManagment = new Mmemory_Managment();
    Data_Managment _DataManagment = new Data_Managment();

    public List<ItemInformation> _ItemInformation = new List<ItemInformation>();
    void Start()
    {
        _MemoryManagment.DataSave_Int("activeCap", -1);
        if (_MemoryManagment.DataLoad_Int("activeCap")==-1)
        {
            foreach (var item in Caps)
            {
                item.SetActive(false);
            }
            capIndex = -1;
            capText.text = "0";
        }
        else
        {
            capIndex = _MemoryManagment.DataLoad_Int("activeCap");
            Caps[capIndex].SetActive(true);
            capText.text = "200";
        }
        
    }
    
    
    public void CapAction_Button(string action)
    {
        if (action=="forward")
        {
            if (capIndex==-1)
            {
                capIndex = 0;
                Caps[capIndex].SetActive(true);
            }
            else
            {
                Caps[capIndex].SetActive(false);
                capIndex++;
                Caps[capIndex].SetActive(true);
            }
            if (capIndex==Caps.Length-1)
            {
                CapButtons[1].interactable = false;
            }
            else
            {
                CapButtons[1].interactable = true;
            }
            if (capIndex!=-1)
            {
                CapButtons[0].interactable = true;
            }
        }
        else 
        {
            if (capIndex!=-1)
            {
                Caps[capIndex].SetActive(false);
                capIndex--;
                if (capIndex!=-1)
                {
                    Caps[capIndex].SetActive(true);
                    CapButtons[0].interactable = true;
                }
                else
                {
                    CapButtons[0].interactable = false;
                }
            }
            else
            {
                CapButtons[0].interactable = false;
            }
            if (capIndex != Caps.Length - 1)//en önemli mantığı burda.burada bu if i koymazsak geri tuşunab astığımızda ileri tuşu geri aktif olmayacaktı.
            {
                CapButtons[1].interactable = true;
            }
        }
    }

    
}
