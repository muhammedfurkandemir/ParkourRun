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
    bool Contact;
    void Start()
    {
        
    }
    private void LateUpdate()
    {
        if(Contact)
        _NavMesh.SetDestination(Target.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SubCharacter")||other.CompareTag("Player"))
        {
            ChangeMaterialAndAnimationTrigger();
            Contact = true;
        }
    }
    void ChangeMaterialAndAnimationTrigger()
    {
        Material[] mats = _Renderer.materials;
        mats[0] = AsignedMaterial;
        _Renderer.materials = mats;
        _Animator.SetBool("Fight", true);
    }


}
