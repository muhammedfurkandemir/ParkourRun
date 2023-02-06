using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sub_Character : MonoBehaviour
{
    GameObject Target;
    NavMeshAgent Agent;
    
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Target = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().DestinationPoint;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Agent.SetDestination(Target.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PinBox"))
        {
            Vector3 PositionChange = new Vector3(transform.position.x, .23f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CreateDeadEfect(PositionChange);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Obstacle_Saw"))
        {
            Vector3 PositionChange = new Vector3(transform.position.x, .23f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CreateDeadEfect(PositionChange);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("PropellerNeedle"))
        {
            Vector3 PositionChange = new Vector3(transform.position.x, .23f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CreateDeadEfect(PositionChange);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("SledgeHammer"))
        {
            Vector3 PositionChange = new Vector3(transform.position.x, .23f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CreateDeadEfect(PositionChange,true);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Enemy"))
        {
            Vector3 PositionChange = new Vector3(transform.position.x, .23f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CreateDeadEfect(PositionChange,false,false);
            gameObject.SetActive(false);
        }
    }
}
