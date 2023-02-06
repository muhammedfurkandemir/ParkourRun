using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [Header("Left for '-' values  Right for '+' values")]
    public float Force;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SubCharacter"))
        {
            //alt karakterlerin rigidbody' sinde onlara güç uygulayarak rüzgar etkisin oluşturur.
            other.GetComponent<Rigidbody>().AddForce(new Vector3(Force, 0, 0), ForceMode.Impulse);
        }
    }
}
