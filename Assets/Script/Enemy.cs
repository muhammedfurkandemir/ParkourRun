using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameManager _GameManager;
    public GameObject FightPoint;
    public  NavMeshAgent _NavMesh;
    bool isFightStart;  

    public void AnimationTrigger()
    {
        GetComponent<Animator>().SetBool("Fight", true);
        isFightStart = true;
    }
    void Update()
    {
        if (isFightStart)
        {
            _NavMesh.SetDestination(FightPoint.transform.position);
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SubCharacter"))
        {
            Vector3 PositionChange = new Vector3(transform.position.x, .23f, transform.position.z);
            _GameManager.CreateDeadEfect(PositionChange,false,true);
            gameObject.SetActive(false);
        }
    }
}
