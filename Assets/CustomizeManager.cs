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
                CapButtons[1].interactable = true;
            }
        }
    }

    
}
