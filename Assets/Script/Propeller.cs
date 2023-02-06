using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    public float WorkingIntervalTime;
    private Animator animator;
    public BoxCollider Wild;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void CreateAnimation(string state)
    {
        if (state == "true")
        {
            animator.SetBool("Directive", true);
            Wild.enabled = true; 
        }            
        else
        {
            animator.SetBool("Directive", false);
            StartCoroutine(WaitTimer());
            Wild.enabled = false;
        }
    }
    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(WorkingIntervalTime);
        CreateAnimation("true");
    }
}
