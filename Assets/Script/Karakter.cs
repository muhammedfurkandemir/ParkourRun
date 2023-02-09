using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Karakter : MonoBehaviour
{
    public GameManager _GameManager;
    public Kamera _Camera;
    public GameObject CharacterFinalPosition;
    bool isCharacterFinalPointOn;
    public Slider _Slider;
    public GameObject FinishPoint;
    
    
    private void FixedUpdate()
    {
        if (!isCharacterFinalPointOn)
           transform.Translate(Vector3.forward * 1f * Time.deltaTime);  
        
    }
    private void Start()
    {
        //slider starting max value 
        float diff = Vector3.Distance(transform.position, FinishPoint.transform.position);//karakterin başlangıcıdan bitiş triggerına kadar olan mesafeyi alır.
        _Slider.maxValue = diff;
    }
    void Update()
    {
        if (isCharacterFinalPointOn)
        {
            transform.position = Vector3.Lerp(transform.position, CharacterFinalPosition.transform.position, .01f);
            if (_Slider.value != 0)
                _Slider.value -= 0.005f;
        }
        else
        {
            //slider diff current value 
            float diff = Vector3.Distance(transform.position, FinishPoint.transform.position);
            _Slider.value = diff;//sliderin değerini burada arttırır.
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Input.GetAxis("Mouse X") < 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y
                        , transform.position.z), .3f);
                }
                if (Input.GetAxis("Mouse X") > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .3f, transform.position.y
                        , transform.position.z), .3f);
                }
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Multiple")||other.CompareTag("Sum") || other.CompareTag("Sub") || other.CompareTag("Divide"))
        {
            int number = int.Parse(other.name);
            _GameManager.CharacterManager(other.tag, number, other.transform);
            
        }
        else if (other.CompareTag("FinalTrigger"))
        {
            _Camera.İsFinalPointOn = true;
            _GameManager.TriggerEnemy();
            isCharacterFinalPointOn = true;

        }
        else if (other.CompareTag("EmptyCharacter"))
        {
            _GameManager.SubCharacters.Add(other.gameObject);           
        }
    }
    private void OnCollisionEnter(Collision collision)//takılma buglarını yakalar ve belli birim öteleyerek sorunu çözer.
    {
        if (collision.gameObject.CompareTag("Column")||collision.gameObject.CompareTag("PinBox"))
        {
            if (transform.position.x>0)
            {
                transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
            }
        }
        else if (collision.gameObject.CompareTag("PropellerNeedle"))
        {
            if (transform.position.x > 0)
            {
                transform.position = new Vector3(transform.position.x - .75f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + .75f, transform.position.y, transform.position.z);
            }
        }
    }
}
