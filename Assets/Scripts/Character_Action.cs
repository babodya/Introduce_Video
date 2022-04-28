using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character_Action : MonoBehaviour
{

    void Start()
    {
        //Vector3 lookPosition = (Character_Comtroller.instance.tvOBJ.position - transform.position);
        //Quaternion quaternion = Quaternion.LookRotation(lookPosition);
        ////transform.rotation = Quaternion.Euler(transform.rotation.x, lookPosition.y, transform.rotation.z);
        //transform.rotation = Quaternion.Euler(lookPosition);


        //transform.DORotateQuaternion(quaternion, 0.1f)
        //    .SetEase(Ease.Linear);


        //transform.DORotate(lookPosition, 0.1f)
        //    .SetEase(Ease.Linear);
    }

    void Update()
    {
        
    }
}
