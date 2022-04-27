using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImageController : MonoBehaviour
{
    public static ImageController instance;

    public Texture[] textures;
    public RawImage rawImage;
    public Ease ease;
    int count = 0;

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
    }
    void Start()
    {
        StartCoroutine(ChangeImage());
    }

    IEnumerator ChangeImage()
    {
        rawImage.DOFade(0, 0.1f)
            .SetEase(ease);

        while (true)
        {
            if (count < 7) 
            {
                rawImage.texture = textures[count];

                count++;
            }
            else if(count  == 7)
            {
                count = 0;

                rawImage.texture = textures[count];
            }

            rawImage.DOFade(0.8f, 1.0f)
                .SetEase(ease);

            yield return new WaitForSeconds(3.0f);

            rawImage.DOFade(0, 1.0f)
                .SetEase(ease);

            yield return new WaitForSeconds(2.0f);
        }
    }

}
