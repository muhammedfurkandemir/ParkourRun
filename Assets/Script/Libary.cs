using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Furkan
{
    public class Mathmatical_Funcition
    {
        public void Multiple(int inComeNumber, List<GameObject> SubCharacters, Transform spawn, List<GameObject> SpawnEfects)
        {
            int LoopNumber = (GameManager.InstantCharacterCount * inComeNumber) - GameManager.InstantCharacterCount;
            int countNumber = 0;
            foreach (var item in SubCharacters)
            {
                if (countNumber < LoopNumber)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in SpawnEfects)
                        {
                            item2.SetActive(true);
                            item2.transform.position = spawn.position;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                        item.transform.position = spawn.position + new Vector3(0, 0, -1f);
                        item.SetActive(true);
                        countNumber++;
                    }
                }
                else
                {
                    countNumber = 0;
                    break;
                }
            }
            GameManager.InstantCharacterCount *= inComeNumber;
        }

        public void Sum(int inComeNumber, List<GameObject> SubCharacters, Transform spawn, List<GameObject> SpawnEfects)
        {
            int countNumber1 = 0;
            foreach (var item in SubCharacters)
            {
                if (countNumber1 < inComeNumber)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in SpawnEfects)
                        {
                            item2.SetActive(true);
                            item2.transform.position = spawn.position;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                        item.transform.position = spawn.position + new Vector3(0, 0, -1f);
                        item.SetActive(true);
                        countNumber1++;
                    }
                }
                else
                {
                    countNumber1 = 0;
                    break;
                }

            }
            GameManager.InstantCharacterCount += inComeNumber;
        }

        public void Sub(int inComeNumber, List<GameObject> SubCharacters, List<GameObject> DeadEfects)
        {
            if (GameManager.InstantCharacterCount < inComeNumber)
            {
                foreach (var item in SubCharacters)
                {
                    if (item.activeInHierarchy)
                    {
                        foreach (var item2 in DeadEfects)
                        {
                            Vector3 newPoz = new Vector3(item.transform.position.x, .22f, item.transform.position.z);
                            item2.SetActive(true);
                            item2.transform.position = newPoz;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                        item.transform.position = Vector3.zero;
                        item.SetActive(false);
                    }
                }
                GameManager.InstantCharacterCount = 1;
            }
            else
            {
                int countNumber2 = 0;
                foreach (var item in SubCharacters)
                {
                    if (countNumber2 != inComeNumber)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in DeadEfects)
                            {
                                Vector3 newPoz = new Vector3(item.transform.position.x, .22f, item.transform.position.z);
                                item2.SetActive(true);
                                item2.transform.position = newPoz;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            countNumber2++;
                        }
                    }
                    else
                    {
                        countNumber2 = 0;
                        break;
                    }

                }
                GameManager.InstantCharacterCount -= inComeNumber;
            }
        }

        public void Divide(int inComeNumber, List<GameObject> SubCharacters,List<GameObject> DeadEfects)
        {
            if (GameManager.InstantCharacterCount <= inComeNumber)
            {
                foreach (var item in SubCharacters)
                {
                    if (item.activeInHierarchy)
                    {
                        foreach (var item2 in DeadEfects)
                        {
                            Vector3 newPoz = new Vector3(item.transform.position.x, .23f, item.transform.position.z);
                            item2.SetActive(true);
                            item2.transform.position = newPoz;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                        item.transform.position = Vector3.zero;
                        item.SetActive(false);
                    }
                }
                GameManager.InstantCharacterCount = 1;
            }
            else
            {
                int divisor = GameManager.InstantCharacterCount / inComeNumber;
                int countNumber3 = 0;
                foreach (var item in SubCharacters)
                {
                    if (countNumber3 != divisor)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in DeadEfects)
                            {
                                Vector3 newPoz = new Vector3(item.transform.position.x, .23f, item.transform.position.z);
                                item2.SetActive(true);
                                item2.transform.position = newPoz;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            countNumber3++;
                        }
                    }
                    else
                    {
                        countNumber3 = 0;
                        break;
                    }

                } // divide for 2 and 3
                if (GameManager.InstantCharacterCount % inComeNumber == 0)
                {
                    GameManager.InstantCharacterCount /= inComeNumber;
                }
                else if (GameManager.InstantCharacterCount % inComeNumber == 1)
                {
                    GameManager.InstantCharacterCount /= inComeNumber;
                    GameManager.InstantCharacterCount++;
                }
                else if (GameManager.InstantCharacterCount % inComeNumber == 2)
                {
                    GameManager.InstantCharacterCount /= inComeNumber;
                    GameManager.InstantCharacterCount += 2;
                }

            }
        }

    }

    public class Mmemory_Managment
    {
        public void DataSave_String(string Key,string Value)
        {
            PlayerPrefs.SetString(Key, Value);
            PlayerPrefs.Save();
        }
        public void DataSave_Int(string Key,int Value)
        {
            PlayerPrefs.SetInt(Key, Value);
            PlayerPrefs.Save();
        }
        public void DataSave_Float(string Key,float Value)
        {
            PlayerPrefs.SetFloat(Key, Value);
            PlayerPrefs.Save();
        }

        public string DataLoad_String(string Key)
        {
            return PlayerPrefs.GetString(Key);
        }
        public int DataLoad_Int(string Key)
        {
            return PlayerPrefs.GetInt(Key);
        }
        public float DataLoad_Float(string Key)
        {
            return PlayerPrefs.GetFloat(Key);
        }
        public void ControlAndDefine()//oyun ilk açıldığında son kalınan leveli çağırmak içindir.
        {
            if (!PlayerPrefs.HasKey("LastLevel"))//haskey ile bu isimde level olup olmadığını kontrol ederiz.
            {
                PlayerPrefs.SetInt("LastLevel", 5);
                PlayerPrefs.SetInt("Coin", 100);
            }
        }
    }
    public class Data
    {
        public static int puan;
    }
}

