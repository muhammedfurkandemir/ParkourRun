using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Furkan;

public class CustomizeManager : MonoBehaviour
{
    [Header("      TextField")]
    public TMP_Text coinText;
    public TMP_Text[] itemCoinsText;
    [Header("      ChooseButtons")]
    public GameObject[] ItemPanels;
    public GameObject[] ItemButtons;
    public Button[] ItemUnlockButtons;
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
    bool isClick = false;
    int oldIndex;
    Vector3 oldPos;

    int activeButtonIndex=-1;

    Mmemory_Managment _MemoryManagment = new Mmemory_Managment();
    Data_Managment _DataManagment = new Data_Managment();
    [Header("      Item Information")]
    public List<ItemInformation> _ItemInformation = new List<ItemInformation>();
    void Start()
    {
        _MemoryManagment.DataSave_Int("activeCap", -1);
        _MemoryManagment.DataSave_Int("activeStick", -1);
        _MemoryManagment.DataSave_Int("activeCostume", -1);
        _MemoryManagment.DataSave_Int("coin", 1500);
        coinText.text = _MemoryManagment.DataLoad_Int("coin").ToString();
        

        _DataManagment.Save(_ItemInformation);
        _DataManagment.Load();
        _ItemInformation = _DataManagment.TransferData();
        
    }

    public void Select()
    {
        Debug.Log(activeButtonIndex);
    }
    public void Buy()
    {
        if (activeButtonIndex !=-1)
        {
            switch (activeButtonIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }
    }
    void ItemActiveCheckStatus(int index,bool isActive=false)
    {
        if (index==0)
        {
            if (_MemoryManagment.DataLoad_Int("activeCap") == -1)
            {
                foreach (var item in Caps)
                {
                    item.SetActive(false);
                }
                ItemUnlockButtons[0].interactable = false;
                ItemUnlockButtons[1].interactable = false;
                if (!isActive)
                {
                    capIndex = -1;
                    capText.text = "Varsayılan";
                }
               
            }
            else
            {
                capIndex = _MemoryManagment.DataLoad_Int("activeCap");
                Caps[capIndex].SetActive(true);
            }
        }
        if (index == 1)
        {
            if (_MemoryManagment.DataLoad_Int("activeStick") == -1)
            {
                foreach (var item in Sticks)
                {
                    item.SetActive(false);
                }
                ItemUnlockButtons[0].interactable = false;
                ItemUnlockButtons[1].interactable = false;
                if (!isActive)
                {
                    stickIndex = -1;
                    stickText.text = "Varsayılan";
                }
               
            }
            else
            {
                stickIndex = _MemoryManagment.DataLoad_Int("activeStick");
                Sticks[stickIndex].SetActive(true);
            }
        }
        if (index == 2)
        {
            if (_MemoryManagment.DataLoad_Int("activeCostume") == -1)
            {                
                if (!isActive)
                {
                    costumeIndex = -1;
                    costumeText.text = "Varsayılan";
                }
                else
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = DefaultCostume;
                    _Renderer.materials = mats;
                }
                ItemUnlockButtons[0].interactable = false;
                ItemUnlockButtons[1].interactable = false;
            }
            else
            {
                costumeIndex = _MemoryManagment.DataLoad_Int("activeCostume");
                Material[] mats = _Renderer.materials;
                mats[0] = Costumes[costumeIndex];
                _Renderer.materials = mats;
            }
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
                capText.text = _ItemInformation[capIndex].ItemName;
                if (!_ItemInformation[capIndex].PurchaseStatus)
                {
                     itemCoinsText[0].text= _ItemInformation[capIndex].ItemCoin.ToString();
                     ItemUnlockButtons[0].interactable = false;
                     ItemUnlockButtons[1].interactable = true;
                }
                else
                {
                    itemCoinsText[0].text = "Satın Alındı";
                    ItemUnlockButtons[0].interactable = true;
                    ItemUnlockButtons[1].interactable = false;
                }
            }
            else
            {
                Caps[capIndex].SetActive(false);
                capIndex++;
                Caps[capIndex].SetActive(true);
                capText.text = _ItemInformation[capIndex].ItemName;
                if (!_ItemInformation[capIndex].PurchaseStatus)
                {
                    itemCoinsText[0].text = _ItemInformation[capIndex].ItemCoin.ToString();
                    ItemUnlockButtons[0].interactable = false;
                    ItemUnlockButtons[1].interactable = true;
                }
                else
                {
                    itemCoinsText[0].text = "Satın Alındı";
                    ItemUnlockButtons[0].interactable = true;
                    ItemUnlockButtons[1].interactable = false;
                }
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
                    if (!_ItemInformation[capIndex].PurchaseStatus)
                    {
                        itemCoinsText[0].text = _ItemInformation[capIndex].ItemCoin.ToString();
                        ItemUnlockButtons[0].interactable = false;
                        ItemUnlockButtons[1].interactable = true;
                    }
                    else
                    {
                        itemCoinsText[0].text = "Satın Alındı";
                        ItemUnlockButtons[0].interactable = true;
                        ItemUnlockButtons[1].interactable = false;
                    }
                }
                else
                {
                    CapButtons[0].interactable = false;
                    capText.text = "Varsayılan";
                    itemCoinsText[0].text = "0";
                    ItemUnlockButtons[0].interactable = true;
                    ItemUnlockButtons[1].interactable = false;
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
                if (!_ItemInformation[stickIndex + 9].PurchaseStatus)
                {
                    itemCoinsText[1].text = _ItemInformation[stickIndex + 9].ItemCoin.ToString();
                    ItemUnlockButtons[0].interactable = false;
                    ItemUnlockButtons[1].interactable = true;
                }
                else
                {
                    itemCoinsText[1].text = "Satın Alındı";
                    ItemUnlockButtons[0].interactable = true;
                    ItemUnlockButtons[1].interactable = false;
                }
            }
            else
            {
               
                Sticks[stickIndex].SetActive(false);
                stickIndex++;               
                Sticks[stickIndex].SetActive(true);
                stickText.text = _ItemInformation[stickIndex + 9].ItemName;
                if (!_ItemInformation[stickIndex + 9].PurchaseStatus)
                {
                    itemCoinsText[1].text = _ItemInformation[stickIndex + 9].ItemCoin.ToString();
                    ItemUnlockButtons[0].interactable = false;
                    ItemUnlockButtons[1].interactable = true;
                }
                else
                {
                    itemCoinsText[1].text = "Satın Alındı";
                    ItemUnlockButtons[0].interactable = true;
                    ItemUnlockButtons[1].interactable = false;
                }
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
                    if (!_ItemInformation[stickIndex + 9].PurchaseStatus)
                    {
                        itemCoinsText[1].text = _ItemInformation[stickIndex + 9].ItemCoin.ToString();
                        ItemUnlockButtons[0].interactable = false;
                        ItemUnlockButtons[1].interactable = true;
                    }
                    else
                    {
                        itemCoinsText[1].text = "Satın Alındı";
                        ItemUnlockButtons[0].interactable = true;
                        ItemUnlockButtons[1].interactable = false;
                    }
                }
                else
                {
                    StickButtons[0].interactable = false;
                    stickText.text = "Varsayılan";
                    itemCoinsText[1].text = "0";
                    ItemUnlockButtons[0].interactable = true;
                    ItemUnlockButtons[1].interactable = false;
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
                if (!_ItemInformation[costumeIndex + 17].PurchaseStatus)
                {
                    itemCoinsText[2].text = _ItemInformation[costumeIndex + 17].ItemCoin.ToString();
                    ItemUnlockButtons[0].interactable = false;
                    ItemUnlockButtons[1].interactable = true;
                }
                else
                {
                    itemCoinsText[2].text = "Satın Alındı";
                    ItemUnlockButtons[0].interactable = true;
                    ItemUnlockButtons[1].interactable = false;
                }
            }
            else
            {
                costumeIndex++;
                Material[] mats = _Renderer.materials;
                mats[0] = Costumes[costumeIndex];
                _Renderer.materials = mats;
                costumeText.text = _ItemInformation[costumeIndex + 17].ItemName;
                if (!_ItemInformation[costumeIndex + 17].PurchaseStatus)
                {
                    itemCoinsText[2].text = _ItemInformation[costumeIndex + 17].ItemCoin.ToString();
                    ItemUnlockButtons[0].interactable = false;
                    ItemUnlockButtons[1].interactable = true;
                }
                else
                {
                    itemCoinsText[2].text = "Satın Alındı";
                    ItemUnlockButtons[0].interactable = true;
                    ItemUnlockButtons[1].interactable = false;
                }
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
                    if (!_ItemInformation[costumeIndex + 17].PurchaseStatus)
                    {
                        itemCoinsText[2].text = _ItemInformation[costumeIndex + 17].ItemCoin.ToString();
                        ItemUnlockButtons[0].interactable = false;
                        ItemUnlockButtons[1].interactable = true;
                    }
                    else
                    {
                        itemCoinsText[2].text = "Satın Alındı";
                        ItemUnlockButtons[0].interactable = true;
                        ItemUnlockButtons[1].interactable = false;

                    }
                }
                else
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = DefaultCostume;
                    _Renderer.materials = mats;
                    CostumeButtons[0].interactable = false;
                    costumeText.text = "Varsayılan";
                    itemCoinsText[2].text = "0";
                    ItemUnlockButtons[0].interactable = true;
                    ItemUnlockButtons[1].interactable = false;
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
        ItemActiveCheckStatus(activeButtonIndex);//buraya tekrardan bak olmadı startta iflerden kurtarıp koyacaksın yani buton
                                       //indexlerini başlangıçta direk alması lazım
        if (isClick)
        {
            ItemButtons[oldIndex].transform.localPosition = oldPos;
            ItemButtons[oldIndex].transform.localScale = Vector3.one;
            ItemPanels[oldIndex].SetActive(false);
            ItemActiveCheckStatus(oldIndex,true);
            isClick = false;
        }
        oldPos = ItemButtons[index].transform.localPosition;        
        oldIndex = index;
        ItemPanels[index].SetActive(true);
        ItemButtons[index].transform.localPosition = PosCreate(index);
        ItemButtons[index].transform.localScale = ItemButtons[index].transform.localScale + new Vector3(.200f, .200f, .200f);
        isClick = true;
        activeButtonIndex = index;
    }
    private Vector3 PosCreate(int index)
    {
        return new Vector3(0.927f, ItemButtons[index].transform.localPosition.y, ItemButtons[index].transform.localPosition.z);        
    }
    
   



}

