using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Furkan;

public class CustomizeManager : MonoBehaviour
{
    [Header("      TextField")]
    public TMP_Text puanText;
    [Header("      ChooseButtons")]
    public GameObject[] ItemPanels;
    public GameObject[] ItemButtons;
    [Header("      Caps")]
    public GameObject[] Caps;
    public Button[] CapButtons;
    public TMP_Text capText;
    [Header("      Sticks")]
    public GameObject[] Sticks;
    public Button[] StickButtons;
    public TMP_Text stickText;
    [Header("      Materials")]
    public Material[] Costumes;
    public Button[] CostumeButtons;
    public Material DefaultCostume;
    public TMP_Text costumeText;
    public SkinnedMeshRenderer _Renderer;

    int capIndex = -1;
    int stickIndex = -1;
    int costumeIndex = -1;//sapkanın olmaması durumu için default değer olarak -1 verdik.
    //item button action field
    bool girildimi = false;
    int oldIndex;
    Vector3 oldPos;

    Mmemory_Managment _MemoryManagment = new Mmemory_Managment();
    Data_Managment _DataManagment = new Data_Managment();
    [Header("      Item Information")]
    public List<ItemInformation> _ItemInformation = new List<ItemInformation>();
    void Start()
    {
        _MemoryManagment.DataSave_Int("activeCap", -1);
        _MemoryManagment.DataSave_Int("activeStick", -1);
        _MemoryManagment.DataSave_Int("activeCostume", -1);
        #region cap
        if (_MemoryManagment.DataLoad_Int("activeCap")==-1)
        {
            foreach (var item in Caps)
            {
                item.SetActive(false);
            }
            capIndex = -1;
            capText.text = "Varsayılan";
        }
        else
        {
            capIndex = _MemoryManagment.DataLoad_Int("activeCap");
            Caps[capIndex].SetActive(true);
        }
        #endregion
        #region stick
        if (_MemoryManagment.DataLoad_Int("activeStick") == -1)
        {
            foreach (var item in Sticks)
            {
                item.SetActive(false);
            }
            stickIndex = -1;
            stickText.text = "Varsayılan";
        }
        else
        {
            stickIndex = _MemoryManagment.DataLoad_Int("activeStick");
            Sticks[stickIndex].SetActive(true);
        }
        #endregion
        #region costume
        if (_MemoryManagment.DataLoad_Int("activeCostume") == -1)
        {            
            costumeIndex = -1;
            costumeText.text = "Varsayılan";
        }
        else
        {
            costumeIndex = _MemoryManagment.DataLoad_Int("activeCostume");
            Material[] mats = _Renderer.materials;
            mats[0] = Costumes[costumeIndex];
            _Renderer.materials = mats;
        }
        #endregion
        _DataManagment.Save(_ItemInformation);
        _DataManagment.Load();
        _ItemInformation = _DataManagment.TransferData();
        
    }
    
    
    public void CapAction_Button(string action)
    {
        if (action=="forward")
        {
            if (capIndex==-1)
            {
                capIndex = 0;
                Caps[capIndex].SetActive(true);
                capText.text = _ItemInformation[capIndex].ItemName;
            }
            else
            {
                Caps[capIndex].SetActive(false);
                capIndex++;
                Caps[capIndex].SetActive(true);
                capText.text = _ItemInformation[capIndex].ItemName;
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
                    capText.text = _ItemInformation[capIndex].ItemName;
                }
                else
                {
                    CapButtons[0].interactable = false;
                    capText.text = "Varsayılan";
                }
                
            }
            else
            {
                CapButtons[0].interactable = false;
                capText.text = "Varsayılan";
            }
            if (capIndex != Caps.Length - 1)//en önemli mantığı burda.burada bu if i koymazsak geri tuşunab astığımızda ileri tuşu geri aktif olmayacaktı.
            {
                CapButtons[1].interactable = true;
            }
        }
    }
    public void WeaponAction_Button(string action)
    {
        if (action == "forward")
        {
            if (stickIndex == -1)
            {
                stickIndex = 0;
                Sticks[stickIndex].SetActive(true);
                stickText.text = _ItemInformation[stickIndex + 9].ItemName;
            }
            else
            {
               
                Sticks[stickIndex].SetActive(false);
                stickIndex++;               
                Sticks[stickIndex].SetActive(true);
                stickText.text = _ItemInformation[stickIndex + 9].ItemName;
            }
            if (stickIndex == Sticks.Length - 1)
            {
                StickButtons[1].interactable = false;
            }
            else
            {
                StickButtons[1].interactable = true;
            }
            if (stickIndex != -1)
            {
                StickButtons[0].interactable = true;
            }
        }
        else
        {
            if (stickIndex != -1)
            {               
                Sticks[stickIndex].SetActive(false);
                stickIndex--;
                if (stickIndex != -1)
                {                   
                    Sticks[stickIndex].SetActive(true);
                    StickButtons[0].interactable = true;
                    stickText.text = _ItemInformation[stickIndex + 9].ItemName;
                }
                else
                {
                    StickButtons[0].interactable = false;
                    stickText.text = "Varsayılan";
                }

            }
            else
            {
                StickButtons[0].interactable = false;
                stickText.text = "Varsayılan";
            }
            if (stickIndex != Sticks.Length - 1)//en önemli mantığı burda.burada bu if i koymazsak geri tuşunab astığımızda ileri tuşu geri aktif olmayacaktı.
            {
                StickButtons[1].interactable = true;
            }
        }
    }

    public void CostumeAction_Button(string action)
    {
        if (action == "forward")
        {
            if (costumeIndex == -1)
            {
                costumeIndex = 0;
                Material[] mats = _Renderer.materials;
                mats[0] = Costumes[costumeIndex];
                _Renderer.materials = mats;
                costumeText.text = _ItemInformation[costumeIndex + 17].ItemName;
            }
            else
            {
                costumeIndex++;
                Material[] mats = _Renderer.materials;
                mats[0] = Costumes[costumeIndex];
                _Renderer.materials = mats;
                costumeText.text = _ItemInformation[costumeIndex + 17].ItemName;
            }
            if (costumeIndex == Costumes.Length - 1)
            {
                CostumeButtons[1].interactable = false;
            }
            else
            {
                CostumeButtons[1].interactable = true;
            }
            if (costumeIndex != -1)
            {
                CostumeButtons[0].interactable = true;
            }
        }
        else
        {
            if (costumeIndex != -1)
            {
                costumeIndex--;
                if (costumeIndex != -1)
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = Costumes[costumeIndex];
                    _Renderer.materials = mats;
                    CostumeButtons[0].interactable = true;
                    costumeText.text = _ItemInformation[costumeIndex + 17].ItemName;
                }
                else
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = DefaultCostume;
                    _Renderer.materials = mats;
                    CostumeButtons[0].interactable = false;
                    costumeText.text = "Varsayılan";
                }

            }
            else
            {
                Material[] mats = _Renderer.materials;
                mats[0] = DefaultCostume;
                _Renderer.materials = mats;
                CostumeButtons[0].interactable = false;
                costumeText.text = "Varsayılan";
            }
            if (costumeIndex != Costumes.Length - 1)//en önemli mantığı burda.burada bu if i koymazsak geri tuşunab astığımızda ileri tuşu geri aktif olmayacaktı.
            {
                CostumeButtons[1].interactable = true;
            }
        }
    }
    public void ItemButtonAction(int index)
    {
        if (girildimi)
        {
            ItemButtons[oldIndex].transform.localPosition = oldPos;
            ItemButtons[oldIndex].transform.localScale = Vector3.one;
            ItemPanels[oldIndex].SetActive(false);
            girildimi = false;
        }
        oldPos = ItemButtons[index].transform.localPosition;        
        oldIndex = index;
        ItemPanels[index].SetActive(true);
        ItemButtons[index].transform.localPosition = PosVer(index);
        ItemButtons[index].transform.localScale = ItemButtons[index].transform.localScale + new Vector3(.200f, .200f, .200f);
        girildimi = true;
    }
    private Vector3 PosVer(int index)
    {
        return new Vector3(0.927f, ItemButtons[index].transform.localPosition.y, ItemButtons[index].transform.localPosition.z);
        //transform.localPosition = new Vector3(0.85799998f, -0.233400002f, -0.493999988f);
    }
    
   



}

