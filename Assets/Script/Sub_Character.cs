using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sub_Character : MonoBehaviour
{
    public GameObject Target;
    NavMeshAgent Agent;
    public GameManager _GameManager;
    
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();       
        
    }
    
    private void LateUpdate()
    {
        Agent.SetDestination(Target.transform.position);
    }
    Vector3 GetPosition()
    {
        return new Vector3(transform.position.x, .23f, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PinBox"))
        {            
            _GameManager.CreateDeadEfect(GetPosition());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Obstacle_Saw"))
        {            
            _GameManager.CreateDeadEfect(GetPosition());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("PropellerNeedle"))
        {
            _GameManager.CreateDeadEfect(GetPosition());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("SledgeHammer"))
        {;
            _GameManager.CreateDeadEfect(GetPosition(), true);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Enemy"))
        {
            _GameManager.CreateDeadEfect(GetPosition(), false,false);
            gameObject.SetActive(false);
            transform.position = Vector3.back;
        }
        else if (other.CompareTag("EmptyCharacter"))
        {
            _GameManager.SubCharacters.Add(other.gameObject);            
        }
    }
}
