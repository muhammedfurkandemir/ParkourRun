using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Furkan;

public class CustomizeManager : MonoBehaviour
{
    public TMP_Text puanText;
    public TMP_Text capText;
    public GameObject[] Caps;
    public GameObject[] Sticks;
    public Material[] Costumes;

    int capIndex = -1;//sapkanın olmaması durumu için default değer olarak -1 verdik.
    Mmemory_Managment _MemoryManagment = new Mmemory_Managment();
    void Start()
    {
        _MemoryManagment.DataSave_Int("activeCap", 1);
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

    
}
