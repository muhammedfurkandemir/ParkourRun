using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Furkan;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("GameObject")]   
    public GameObject MainCharacter;

    public static int InstantCharacterCount = 1;
    [Header("ObjectPooling")]
    public List<GameObject> SubCharacters;
    public List<GameObject> SpawnEfects;
    public List<GameObject> DeadEfects;
    public List<GameObject> SledgeHammerEfects;
    [Header("LevelData")]
    public List<GameObject> Enemies;
    [Header("CharacterCustomize")]
    public GameObject[] Caps;
    public GameObject[] Sticks;
    public Material[] Costumes;
    public Material DefaultCostume;
    public SkinnedMeshRenderer _Renderer;

    public int EnemyCount;
    bool isGameOver;
    bool isCometoEnd;
    

    Scene _Scene;

    Mathmatical_Funcition _Mathmatical_Funcition = new Mathmatical_Funcition();
    Mmemory_Managment _Memory_Managment = new Mmemory_Managment();
    private void Awake()
    {
        Destroy(GameObject.FindWithTag("MenuSound"));
        CharacterCustomizeControl();
    }
    private void Start()
    {
        CreateEnemy();
        _Scene = SceneManager.GetActiveScene();
    }
    public void CreateEnemy()
    {
        for (int i = 0; i < EnemyCount; i++)
        {
            Enemies[i].SetActive(true);
        }
    }
    public void TriggerEnemy()
    {
        foreach (var item in Enemies)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<Enemy>().AnimationTrigger();
            }
        }
        isCometoEnd = true;
        BattleState();
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //    foreach (var item in SubCharacters)
        //    {
        //        if (!item.activeInHierarchy)
        //        {
        //            item.transform.position = SpawnPoint.transform.position;
        //            item.SetActive(true);
        //            InstantCharacterCount++;
        //            break;
        //        }
        //    }
        Debug.Log(_Memory_Managment.DataLoad_Int("activeCap"));
        
    }

    void BattleState()
    {
        if (isCometoEnd)
        {
            if (InstantCharacterCount == 1 || EnemyCount == 0)
            {
                isGameOver = true;
                foreach (var item in Enemies)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Fight", false);
                        item.GetComponent<Animator>().SetBool("Final", true);
                    }
                }
                foreach (var item in SubCharacters)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Fight", false);
                    }
                }
                if (InstantCharacterCount <= EnemyCount)
                {
                    print("kaybettin");
                    if (_Scene.buildIndex==_Memory_Managment.DataLoad_Int("LastLevel"))
                    {
                        _Memory_Managment.DataSave_Int("coin", _Memory_Managment.DataLoad_Int("coin") + 500);
                        _Memory_Managment.DataSave_Int("LastLvel", _Memory_Managment.DataLoad_Int("LastLevel") + 1);
                    }
                    MainCharacter.GetComponent<Animator>().SetBool("lose", true);
                    MainCharacter.GetComponent<Animator>().SetBool("win", false);
                    MainCharacter.transform.Rotate(0, -180, 0);
                }
                else
                {
                    print("kazandın");
                    if (_Scene.buildIndex == _Memory_Managment.DataLoad_Int("LastLevel"))
                    {
                        _Memory_Managment.DataSave_Int("coin", _Memory_Managment.DataLoad_Int("coin") + 500);
                        _Memory_Managment.DataSave_Int("LastLvel", _Memory_Managment.DataLoad_Int("LastLevel") + 1);
                    }
                    MainCharacter.GetComponent<Animator>().SetBool("win", true);
                    MainCharacter.GetComponent<Animator>().SetBool("lose", false);
                    MainCharacter.transform.Rotate(0, -180, 0);
                    _Memory_Managment.DataSave_Int("LateLevel", _Memory_Managment.DataLoad_Int("LateLevel") + 1);
                }
            }
        }
       
        
    }
    public void CharacterManager(string OperationType, int inComeNumber, Transform spawn)
    {
        switch (OperationType)
        {
            case "Multiple":
                _Mathmatical_Funcition.Multiple(inComeNumber, SubCharacters, spawn,SpawnEfects);
                break;
            case "Sum":
                _Mathmatical_Funcition.Sum(inComeNumber, SubCharacters, spawn,SpawnEfects);
                break;
            case "Sub":
                _Mathmatical_Funcition.Sub(inComeNumber, SubCharacters,DeadEfects);
                break;
            case "Divide":
                _Mathmatical_Funcition.Divide(inComeNumber, SubCharacters, DeadEfects);
                break;
        }
    }

    public void CreateDeadEfect(Vector3 Position,bool SledgeHammer=false,bool state=false)
    {
        foreach (var item in DeadEfects)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = Position;
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();
                if (!state)
                    InstantCharacterCount--;
                else
                    EnemyCount--;
                break;
            }
        }
        if (SledgeHammer)
        {

            Vector3 PositionChange = new Vector3(Position.x, .005f,Position.z );
            foreach (var item in SledgeHammerEfects)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.transform.position = PositionChange; 
                    break;
                }
            }
        }
        if(!isGameOver)
           BattleState(); 
    }

    void CharacterCustomizeControl()
    {
        
       
        if (CustomizeManager.customizeLoaded)
        {
            Debug.Log("kostüm değiştirildi");
            if (_Memory_Managment.DataLoad_Int("activeCap") != -1)
                Caps[_Memory_Managment.DataLoad_Int("activeCap")].SetActive(true);
            if (_Memory_Managment.DataLoad_Int("activeStick") != -1)
                Sticks[_Memory_Managment.DataLoad_Int("activeStick")].SetActive(true);
            if (_Memory_Managment.DataLoad_Int("activeCostume") != -1)
            {
                Material[] mats = _Renderer.materials;
                mats[0] = Costumes[_Memory_Managment.DataLoad_Int("activeCostume")];
                _Renderer.materials = mats;
            }
            else
            {
                Material[] mats = _Renderer.materials;
                mats[0] = DefaultCostume;
                _Renderer.materials = mats;
            }
        }
        
    }
    
}
