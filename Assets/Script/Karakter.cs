using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karakter : MonoBehaviour
{
    public GameManager _GameManager;
    public GameObject Camera;
    public GameObject CharacterFinalPosition;
    bool isCharacterFinalPointOn;
    private void FixedUpdate()
    {
        if (!isCharacterFinalPointOn)
           transform.Translate(Vector3.forward * 1f * Time.deltaTime);  
        
    }


    void Update()
    {
        if (isCharacterFinalPointOn)
        {
            transform.position = Vector3.Lerp(transform.position, CharacterFinalPosition.transform.position, .01f);
        }
        else
        {
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
            Camera.GetComponent<Kamera>().İsFinalPointOn = true;
            _GameManager.TriggerEnemy();
            isCharacterFinalPointOn = true;

        }
    }
}
