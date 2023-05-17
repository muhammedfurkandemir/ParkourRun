using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    private static GameObject instance;
    public AudioSource Sound;
    void Start()
    {
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
        //Sound.volume = PlayerPrefs.GetFloat("SoundLevel");
        DontDestroyOnLoad(instance);//başka bir sahne açıldığında bu objenin kalmasını sağlar.GameManagerda sildim oyunda çalmaması için.
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
