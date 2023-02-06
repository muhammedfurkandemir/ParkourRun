using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    public Transform target;
    public Vector3 target_offset;
    public GameObject CameraFinalPoint;
    public bool İsFinalPointOn;
    void Start()
    {
        //Kamera ve karakterimiz arasında bi offset(aralık) değeri aldık.
        target_offset = transform.position - target.position;
    }


    private void LateUpdate()
    {
        //Kameranın açısal olarak karakteri takip etmesi için lerp ile takibi oluşturduk.Offset ile bu aralık değerini koruduk.
        //-Kamerada takip scriptinde vector.lerp komutunda zaman verirken .100 ile .150 arası ideal zaman aralığıdır.
        if (!İsFinalPointOn)
            transform.position = Vector3.Lerp(transform.position, target.position + target_offset, .125f);
        else
            transform.position = Vector3.Lerp(transform.position, CameraFinalPoint.transform.position, .015f);

    }
}
