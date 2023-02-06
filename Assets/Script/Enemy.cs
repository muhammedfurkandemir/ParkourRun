using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject FightPoint;
    NavMeshAgent _NavMesh;
    bool isFightStart;
    void Start()
    {
        _NavMesh = GetComponent<NavMeshAgent>();
    }

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
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CreateDeadEfect(PositionChange,false,true);
            gameObject.SetActive(false);
        }
    }
}
