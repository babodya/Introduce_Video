using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class Introduce_Content
{
    public string HumanName;

    [TextArea(3, 10)]
    public string nameInputFieldText;

    [TextArea(3, 10)]
    public string contentInputFieldText;

    public bool isLeader;
}

public class Character_Comtroller : MonoBehaviour
{
    public static Character_Comtroller instance;
    public Transform tvOBJ;
    public List<Motion> animations;
    public AnimatorController animator;
    public Animator danceAnimator;
    private BlendTree blendTree;
    public Ease ease;

    [SerializeField]
    GameObject CharacterPrefab;
    public Transform CharacterTranform;
    public Material CharacterMaterial;
    public GameObject sunglassOBJ;
    public Transform[] CharacterPaths;

    [SerializeField]
    float blend_Number = 0;

    [SerializeField]
    Text nameText;

    [SerializeField]
    Text contenetText;

    public List<Introduce_Content> introduce_Contents;
    public int textNumber = 0;

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
        CharacterPrefab.transform.position = CharacterPaths[0].position;
        CharacterPrefab.transform.rotation = CharacterPaths[0].rotation;

        //Create_Blend_AddMotions();
        danceAnimator = CharacterPrefab.GetComponent<Animator>();
        CharacterMaterial = CharacterPrefab.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().materials[0];
    }

    public void Create_Blend_AddMotions()
    {
        animator.CreateBlendTreeInController("Active", out blendTree);

        for (int i = 0; i < animations.Count; i++)
        {
            blendTree.AddChild(animations[i]);
        }
    }

    public void DanceChange()
    {
        if (blend_Number < 15) 
        {
            blend_Number++;
            
            danceAnimator.SetFloat("Blend", blend_Number);
        }
        else if(blend_Number == 15)
        {
            danceAnimator.SetFloat("Blend", blend_Number);

            blend_Number = 0;
        }
    }

    public void TextFadeOut()
    {
        nameText.DOFade(0, 1)
            .SetEase(ease);
        contenetText.DOFade(0, 1)
            .SetEase(ease);
        //CharacterMaterial.DOFade(0, 1)
        //    .SetEase(ease);
    }

    public void TextFadeIn()
    {
        nameText.DOFade(1, 1)
            .SetEase(ease);
        contenetText.DOFade(1, 1)
            .SetEase(ease);
        //CharacterMaterial.DOFade(1, 1)
        //    .SetEase(ease);
    }

    public void TextChange()
    {
        nameText.text = introduce_Contents[textNumber].nameInputFieldText;
        contenetText.text = introduce_Contents[textNumber].contentInputFieldText;
        sunglassOBJ.SetActive(introduce_Contents[textNumber].isLeader);
    }

    public void PlayingIntroduce()
    {
        StartCoroutine(PlayIntroduce());
    }

    IEnumerator PlayIntroduce()
    {
        while (true)
        {
            if(textNumber < introduce_Contents.Count)
            {
                DanceChange();
                TextChange();

                textNumber++;
            }
            else
            {
                textNumber = 0;

                TextChange();
                DanceChange();
            }

            CharacterPrefab.transform.DOMove(CharacterPaths[1].position, 1.0f)
                .SetEase(ease);
            CharacterPrefab.transform.DORotateQuaternion(CharacterPaths[1].rotation, 1.0f)
                .SetEase(ease)
                .OnComplete(() =>
                {
                    TextFadeIn();

                    CharacterPrefab.transform.DOMove(CharacterPaths[2].position, 1.0f)
                        .SetEase(ease);
                    CharacterPrefab.transform.DORotateQuaternion(CharacterPaths[2].rotation, 1.0f)
                        .SetEase(ease);
                        //.OnComplete(() =>
                        //{
                            
                        //});
                });
            yield return new WaitForSeconds(7.0f);

            danceAnimator.SetFloat("Blend", 0);

            CharacterPrefab.transform.DOMove(CharacterPaths[3].position, 1.0f)
                .SetEase(ease);
            CharacterPrefab.transform.DORotateQuaternion(CharacterPaths[3].rotation, 0.2f)
                .SetEase(ease);

            TextFadeOut();

            if(textNumber == introduce_Contents.Count)
            {
                CameraController.instance.fadeImage.DOFade(1, 0.5f)
                    .SetEase(ease);
            }

            yield return new WaitForSeconds(1.0f);

            //sunglassOBJ.SetActive(false);

            CharacterPrefab.transform.position = CharacterPaths[0].position;
            CharacterPrefab.transform.rotation = CharacterPaths[0].rotation;

            //CharacterPrefab.transform.DOMove(CharacterTranform.position, 0.1f)
            //    .SetEase(ease);
            //CharacterPrefab.transform.DORotateQuaternion(CharacterTranform.rotation, 0.1f)
            //    .SetEase(ease);

            yield return new WaitForSeconds(0.5f);
        }
    }

}
