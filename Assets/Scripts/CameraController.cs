using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    Camera mainCamera;

    public Ease ease;
    public Transform [] cameraTarget;
    
    void Start()
    {
        mainCamera = Camera.main;

        CameraMoving();
    }

    void Update()
    {
        
    }

    public void CameraMoving()
    {
        mainCamera.transform.DOMove(cameraTarget[3].position, 0.1f)
            .SetEase(ease);
        mainCamera.transform.DORotateQuaternion(cameraTarget[3].rotation, 0.1f)
            .SetEase(ease);

        mainCamera.transform.DOMove(cameraTarget[0].position, 2.0f)
        .SetEase(ease)
        .OnComplete(() => {
            mainCamera.transform.DOMove(cameraTarget[1].position, 1.0f)
            .SetEase(ease)
            .OnComplete(() => {
                mainCamera.transform.DOMove(cameraTarget[2].position, 2.0f)
                    .SetEase(ease);
                mainCamera.transform.DORotateQuaternion(cameraTarget[2].rotation, 2.0f)
                    .SetEase(ease)
                    .OnComplete(() => {
                        //CameraMoving();
                        Character_Comtroller.instance.PlayingIntroduce();
                    });
            });
            mainCamera.transform.DORotateQuaternion(cameraTarget[1].rotation, 1.0f)
                .SetEase(ease);
        });
        mainCamera.transform.DORotateQuaternion(cameraTarget[0].rotation, 2.0f)
            .SetEase(ease);
    }
}
