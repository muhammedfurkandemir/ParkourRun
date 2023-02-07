using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmptyCharacter : MonoBehaviour
{
    public SkinnedMeshRenderer _Renderer;
    public Material AsignedMaterial;
    public NavMeshAgent _NavMesh;
    public Animator _Animator;
    public GameObject Target;
    public GameManager _GameManager;
    bool Contact;
    void Start()
    {

    }
    private void LateUpdate()
    {
        if (Contact)
            _NavMesh.SetDestination(Target.transform.position);
    }
    Vector3 GetPosition()
    {
        return new Vector3(transform.position.x, .23f, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SubCharacter") || other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("EmptyCharacter"))
            {
                ChangeMaterialAndAnimationTrigger();
                Contact = true;
                GetComponent<AudioSource>().Play();
            }            
        }
        else if (other.CompareTag("PinBox"))
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
        {
            ;
            _GameManager.CreateDeadEfect(GetPosition(), true);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Enemy"))
        {
            _GameManager.CreateDeadEfect(GetPosition(), false, false);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Enemy"))
        {
            _GameManager.CreateDeadEfect(GetPosition(), false, false);
            gameObject.SetActive(false);
        }
    }
    void ChangeMaterialAndAnimationTrigger()
    {
        Material[] mats = _Renderer.materials;
        mats[0] = AsignedMaterial;
        _Renderer.materials = mats;
        _Animator.SetBool("Fight", true);
        GameManager.InstantCharacterCount++;
        gameObject.tag = "SubCharacter";        
    }


}
