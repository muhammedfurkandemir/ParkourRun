using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Furkan;

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
    public int EnemyCount;
    bool isGameOver;
    bool isCometoEnd;

    private void Start()
    {
        CreateEnemy();
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

                    MainCharacter.GetComponent<Animator>().SetBool("lose", true);
                    MainCharacter.GetComponent<Animator>().SetBool("win", false);
                }
                else
                {
                    print("kazandın");

                    MainCharacter.GetComponent<Animator>().SetBool("win", true);
                    MainCharacter.GetComponent<Animator>().SetBool("lose", false);
                    MainCharacter.transform.Rotate(0, -180, 0);
                }
            }
        }
       
        
    }
    public void CharacterManager(string OperationType, int inComeNumber, Transform spawn)
    {
        switch (OperationType)
        {
            case "Multiple":
                Mathmatical_Funcition.Multiple(inComeNumber, SubCharacters, spawn,SpawnEfects);
                break;
            case "Sum":
                Mathmatical_Funcition.Sum(inComeNumber, SubCharacters, spawn,SpawnEfects);
                break;
            case "Sub":
                Mathmatical_Funcition.Sub(inComeNumber, SubCharacters,DeadEfects);
                break;
            case "Divide":
                Mathmatical_Funcition.Divide(inComeNumber, SubCharacters, DeadEfects);
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
    
}
