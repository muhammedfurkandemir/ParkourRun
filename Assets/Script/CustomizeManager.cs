using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Furkan;
using UnityEngine.SceneManagement;

public class CustomizeManager : MonoBehaviour
{
    [Header("      TextField")]
    public TMP_Text coinText;
    public TMP_Text[] itemCoinsText;
    public TMP_Text[] unlockButtonsText;
    [Header("      ChooseButtons")]
    public GameObject[] ItemPanels;
    public GameObject[] ItemButtons;
    public GameObject[] itemBuyAndSelectButtons;
    public Image selectButtonImage;
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

    public AudioSource[] Sounds;

    int capIndex = -1;
    int stickIndex = -1;
    int costumeIndex = -1; //sapkanın olmaması durumu için default değer olarak -1 verdik.
    //item button action field
    bool isClick = false;
    int oldIndex;
    Vector3 oldPos;

    int activeButtonIndex=-1;

    public static bool customizeLoaded = false;

    Mmemory_Managment _MemoryManagment = new Mmemory_Managment();
    Data_Managment _DataManagment = new Data_Managment();
    [Header("      Item Information")]
    public List<ItemInformation> _ItemInformation = new List<ItemInformation>();
    void Start()
    {
        if (SceneManager.GetActiveScene().isLoaded && !customizeLoaded)
        {
            _MemoryManagment.DataSave_Int("activeCap", capIndex);
            _MemoryManagment.DataSave_Int("activeStick", stickIndex);
            _MemoryManagment.DataSave_Int("activeCostume", costumeIndex);
            customizeLoaded=true;
        }
        

        _DataManagment.Save(_ItemInformation);
        _MemoryManagment.DataSave_Int("coin", 10000); 
        coinText.text = _MemoryManagment.DataLoad_Int("coin").ToString();

        _DataManagment.Load();
        _ItemInformation = _DataManagment.TransferData();

        ItemActiveCheckStatus(0, true);
        ItemActiveCheckStatus(1, true);
        ItemActiveCheckStatus(2, true);

        

    }
    

    public void Select()
    {
        Sounds[1].Play();
        if (activeButtonIndex != -1)
        {    
            switch (activeButtonIndex)
            {
             
                case 0:
                    _MemoryManagment.DataSave_Int("activeCap", capIndex);
                    itemBuyAndSelectButtons[0].GetComponentInChildren<Image>().sprite = selectButtonImage.sprite; 
                    print("bastıldı");
                    break;
                case 1:
                    _MemoryManagment.DataSave_Int("activeStick", stickIndex);
                    break;
                case 2:                    
                    _MemoryManagment.DataSave_Int("activeCostume", costumeIndex);
                    break;
            }
        }
    }

  
    public void Buy()
    {
        Sounds[2].Play();
        if (activeButtonIndex !=-1)
        {
            switch (activeButtonIndex)
            {
                case 0:
                    BuyItemResult(capIndex);                    
                    break;
                case 1:
                    int index = stickIndex + 9;
                    BuyItemResult(index);                  
                    break;
                case 2:
                    int index2 = costumeIndex + 17;
                    BuyItemResult(index2);
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
                //itemCoinsText[0].text =_ItemInformation[capIndex].ItemCoin.ToString();
                itemBuyAndSelectButtons[0].SetActive(false);
                itemBuyAndSelectButtons[1].SetActive(false);
                if (!isActive)
                {
                    capIndex = -1;
                    capText.text = "Varsayılan";                
                }
                
            }
            else
            {
                Debug.Log("Item Information Count: " + _ItemInformation.Count);
                Debug.Log("capIndex: " + capIndex);

                foreach (var item in Caps)
                {
                    item.SetActive(false);
                }
                capIndex = _MemoryManagment.DataLoad_Int("activeCap");
                Caps[capIndex].SetActive(true);                
                capText.text = _ItemInformation[capIndex].ItemName;
                itemCoinsText[0].text = _ItemInformation[capIndex].ItemCoin.ToString();              
                itemBuyAndSelectButtons[1].SetActive(true);
                itemBuyAndSelectButtons[0].SetActive(true);
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
                itemCoinsText[1].text = _ItemInformation[stickIndex + 9].ItemCoin.ToString();                
                itemBuyAndSelectButtons[0].SetActive(false);
                itemBuyAndSelectButtons[1].SetActive(false);
                if (!isActive)
                {
                    stickIndex = -1;
                    stickText.text = "Varsayılan";
                  
                }
                

            }
            else
            {
                foreach (var item in Sticks)
                {
                    item.SetActive(false);
                }
                stickIndex = _MemoryManagment.DataLoad_Int("activeStick");
                Sticks[stickIndex].SetActive(true);
                stickText.text = _ItemInformation[stickIndex + 9].ItemName;
                itemBuyAndSelectButtons[1].SetActive(false);
                itemBuyAndSelectButtons[0].SetActive(true);
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
                itemCoinsText[1].text = _ItemInformation[stickIndex + 17].ItemCoin.ToString();
                itemBuyAndSelectButtons[0].SetActive(false);
                itemBuyAndSelectButtons[1].SetActive(false);

            }
            else
            {
                costumeIndex = _MemoryManagment.DataLoad_Int("activeCostume");
                Material[] mats = _Renderer.materials;
                mats[0] = Costumes[costumeIndex];
                _Renderer.materials = mats;
                costumeText.text = _ItemInformation[costumeIndex + 17].ItemName;
                itemBuyAndSelectButtons[1].SetActive(false);
                itemBuyAndSelectButtons[0].SetActive(true);
            }
        }
    }    
    public void CapAction_Button(string action)
    {
        Sounds[0].Play();
        if (action == "forward")
        {
            if (capIndex == -1)
            {
                capIndex = 0;
                Caps[capIndex].SetActive(true);
                capText.text = _ItemInformation[capIndex ].ItemName;
                if (!_ItemInformation[capIndex ].PurchaseStatus)
                {
                    itemCoinsText[0].text = _ItemInformation[capIndex ].ItemCoin.ToString();
                    itemBuyAndSelectButtons[0].SetActive(false);
                    if (_MemoryManagment.DataLoad_Int("coin") < _ItemInformation[capIndex ].ItemCoin)
                        itemBuyAndSelectButtons[1].SetActive(false);
                    else
                        itemBuyAndSelectButtons[1].SetActive(true);
                }
                else
                {
                    itemCoinsText[0].text = "0";
                    itemBuyAndSelectButtons[0].SetActive(true);
                    itemBuyAndSelectButtons[1].SetActive(false);
                }
            }
            else
            {

                Caps[capIndex].SetActive(false);
                capIndex++;
                Caps[capIndex].SetActive(true);
                capText.text = _ItemInformation[capIndex ].ItemName;
                if (!_ItemInformation[capIndex ].PurchaseStatus)
                {
                    itemCoinsText[0].text = _ItemInformation[capIndex ].ItemCoin.ToString();
                    itemBuyAndSelectButtons[0].SetActive(false);
                    if (_MemoryManagment.DataLoad_Int("coin") < _ItemInformation[capIndex ].ItemCoin)
                        itemBuyAndSelectButtons[1].SetActive(false);
                    else
                        itemBuyAndSelectButtons[1].SetActive(true);
                }
                else
                {
                    itemCoinsText[0].text = "0";
                    itemBuyAndSelectButtons[0].SetActive(true);
                    itemBuyAndSelectButtons[1].SetActive(false);
                }
            }
            if (capIndex == Caps.Length - 1)
            {
                CapButtons[1].interactable = false;
            }
            else
            {
                CapButtons[1].interactable = true;
            }
            if (capIndex != -1)
            {
                CapButtons[0].interactable = true;
            }
        }
        else
        {
            if (capIndex != -1)
            {
                Caps[capIndex].SetActive(false);
                capIndex--;
                if (capIndex != -1)
                {
                    Caps[capIndex].SetActive(true);
                    CapButtons[0].interactable = true;
                    capText.text = _ItemInformation[capIndex].ItemName;
                    if (!_ItemInformation[capIndex ].PurchaseStatus)
                    {
                        itemCoinsText[0].text = _ItemInformation[capIndex].ItemCoin.ToString();
                        itemBuyAndSelectButtons[0].SetActive(false);
                        if (_MemoryManagment.DataLoad_Int("coin") < _ItemInformation[capIndex].ItemCoin)
                            itemBuyAndSelectButtons[1].SetActive(false);
                        else
                            itemBuyAndSelectButtons[1].SetActive(true);
                    }
                    else
                    {
                        itemCoinsText[0].text = "0";
                        itemBuyAndSelectButtons[0].SetActive(true);
                        itemBuyAndSelectButtons[1].SetActive(false);
                    }
                }
                else
                {
                    CapButtons[0].interactable = false;
                    capText.text = "Varsayılan";
                    itemCoinsText[1].text = "0";
                    itemBuyAndSelectButtons[0].SetActive(true);
                    itemBuyAndSelectButtons[1].SetActive(false);
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
        Sounds[0].Play();
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
                    itemBuyAndSelectButtons[0].SetActive(false);
                    if (_MemoryManagment.DataLoad_Int("coin") < _ItemInformation[stickIndex + 9].ItemCoin)
                        itemBuyAndSelectButtons[1].SetActive(false);
                    else
                        itemBuyAndSelectButtons[1].SetActive(true);
                }
                else
                {
                    itemCoinsText[1].text = "0";
                    itemBuyAndSelectButtons[0].SetActive(true);
                    itemBuyAndSelectButtons[1].SetActive(false);
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
                    itemBuyAndSelectButtons[0].SetActive(false);
                    if (_MemoryManagment.DataLoad_Int("coin") < _ItemInformation[stickIndex + 9].ItemCoin)
                        itemBuyAndSelectButtons[1].SetActive(false);
                    else
                        itemBuyAndSelectButtons[1].SetActive(true);
                }
                else
                {
                    itemCoinsText[1].text = "0";
                    itemBuyAndSelectButtons[0].SetActive(true);
                    itemBuyAndSelectButtons[1].SetActive(false);
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
                        itemBuyAndSelectButtons[0].SetActive(false);
                        if (_MemoryManagment.DataLoad_Int("coin") < _ItemInformation[stickIndex + 9].ItemCoin)
                            itemBuyAndSelectButtons[1].SetActive(false);
                        else
                            itemBuyAndSelectButtons[1].SetActive(true);
                    }
                    else
                    {
                        itemCoinsText[1].text = "0";
                        itemBuyAndSelectButtons[0].SetActive(true);
                        itemBuyAndSelectButtons[1].SetActive(false);
                    }
                }
                else
                {
                    StickButtons[0].interactable = false;
                    stickText.text = "Varsayılan";
                    itemCoinsText[1].text = "0";
                    itemBuyAndSelectButtons[0].SetActive(true);
                    itemBuyAndSelectButtons[1].SetActive(false);
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
        Sounds[0].Play();
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
                    itemBuyAndSelectButtons[0].SetActive(false);
                    if (_MemoryManagment.DataLoad_Int("coin") < _ItemInformation[costumeIndex + 17].ItemCoin)
                        itemBuyAndSelectButtons[1].SetActive(false);
                    else
                        itemBuyAndSelectButtons[1].SetActive(true);
                }
                else
                {
                    itemCoinsText[2].text = "0";
                    itemBuyAndSelectButtons[0].SetActive(true);
                    itemBuyAndSelectButtons[1].SetActive(false);
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
                    itemBuyAndSelectButtons[0].SetActive(false);
                    if (_MemoryManagment.DataLoad_Int("coin") < _ItemInformation[costumeIndex + 17].ItemCoin)
                        itemBuyAndSelectButtons[1].SetActive(false);
                    else
                        itemBuyAndSelectButtons[1].SetActive(true);
                }
                else
                {
                    itemCoinsText[2].text = "0";
                    itemBuyAndSelectButtons[0].SetActive(true);
                    itemBuyAndSelectButtons[1].SetActive(false);
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
                        itemBuyAndSelectButtons[0].SetActive(false);
                        if (_MemoryManagment.DataLoad_Int("coin") < _ItemInformation[costumeIndex + 17].ItemCoin)
                            itemBuyAndSelectButtons[1].SetActive(false);
                        else
                            itemBuyAndSelectButtons[1].SetActive(true);
                    }
                    else
                    {
                        itemCoinsText[2].text = "0";
                        itemBuyAndSelectButtons[0].SetActive(true);
                        itemBuyAndSelectButtons[1].SetActive(false);
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
                    itemBuyAndSelectButtons[0].SetActive(true);
                    itemBuyAndSelectButtons[1].SetActive(false);
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
        Sounds[0].Play();
        ItemActiveCheckStatus(index);
        if (isClick)
        {
            ItemButtons[oldIndex].transform.localPosition = oldPos;
            ItemButtons[oldIndex].transform.localScale = Vector3.one;
            ItemPanels[oldIndex].SetActive(false);
            ItemActiveCheckStatus(oldIndex, true);
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
    
    public void  MainMenuBackButton()
    {
        Sounds[0].Play();
        _DataManagment.Save(_ItemInformation);
        SceneManager.LoadScene(0);
    }

    //--------------------------------
    void BuyItemResult(int index)
    {
        _ItemInformation[index].PurchaseStatus = true;
        _MemoryManagment.DataSave_Int("coin", _MemoryManagment.DataLoad_Int("coin") - _ItemInformation[index].ItemCoin);
        unlockButtonsText[1].text = "Satın Al";
        itemBuyAndSelectButtons[1].SetActive(false);
        itemBuyAndSelectButtons[0].SetActive(true);
        coinText.text = _MemoryManagment.DataLoad_Int("coin").ToString();
    }

    private Vector3 PosCreate(int index)
    {
        return new Vector3(0.927f, ItemButtons[index].transform.localPosition.y, ItemButtons[index].transform.localPosition.z);
    }
}

