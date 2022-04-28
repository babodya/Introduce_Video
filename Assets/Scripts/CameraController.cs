using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    Camera mainCamera;

    public Ease ease;
    public Transform [] cameraTarget;

    public Image fadeImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        //fadeImage.DOFade(1, 0.1f)
        //    .SetEase(ease);

        fadeImage.color = new Color(0, 0, 0, 1);
    }

    void Start()
    {
        mainCamera = Camera.main;

        fadeImage.DOFade(0, 1.0f)
            .SetEase(ease)
            .OnComplete(() =>
            {
                CameraMoving();
            });
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
