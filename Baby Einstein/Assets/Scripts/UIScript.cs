using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Image img;
    public Image img2;

    public Text second_Text;
    public Text third_Text;

    private float start_pos;
    private float end_pos;
    private float dis = 400f;
    //private float lerp_time = 10;
    //private float currentLerpTime;

    void Start()
    {
        start_pos = second_Text.transform.position.y;
        end_pos = second_Text.transform.position.y -     dis;
        GameManager.current.onPanel1_Active += OnPanel1_Active;
    }

    private void OnPanel1_Active()
    {
        second_Text.gameObject.SetActive(true);
        second_Text.text = "TAP ON THE FLOWERS AND THE LEAVES \n TO MAKE THEM RUSTLE";
        AnimationText();

    }

    public void AnimationText()
    {
        LeanTween.moveY(img.gameObject, end_pos, 3f).setEase(LeanTweenType.easeInOutCubic);
    }

    public void EndAnimationText()
    {
        LeanTween.moveY(img.gameObject, start_pos, 3f).setEase(LeanTweenType.easeInOutCubic);
        
        AnimationText2();
    }
    
    public void AnimationText2()
    {
        //second_Text.gameObject.SetActive(false);
        LeanTween.moveY(img2.gameObject, end_pos, 3f).setEase(LeanTweenType.easeInOutCubic).setDelay(3f);
        third_Text.text = "MAKE YOUR FRIENDS, BABY TIGER AND BABY ZEBRA \n WAVE AT YOU BY TAPPING ON THEM";
    }

    public void EndAnimationText2()
    {
        LeanTween.moveY(img2.gameObject, start_pos, 3f).setEase(LeanTweenType.easeInOutCubic);
        StartCoroutine(DestroyTheTextObject());
    }

    private IEnumerator DestroyTheTextObject()
    {
        yield return new WaitForSeconds(2f);
        Destroy(img2.gameObject);
    }

    private void OnDestroy()
    {
        GameManager.current.onPanel1_Active -= OnPanel1_Active;
    }
}
