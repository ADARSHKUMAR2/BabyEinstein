using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Image img;
    public Image img2;

    [SerializeField]
    private Text first_text;
    public Text second_Text;
    public Text third_Text;

    private float start_pos;
    private float end_pos;
    private float dis = 400f;

    private float delay = 0.4f;
    private string fullText = "Walk through the \n morning dewdrops with me ";
    private string currentText="";

    private CharacterController characterController;

    void Start()
    {
        StartCoroutine(TypeWriter());
        characterController = GameManager.current.GetComponent<CharacterController>();
        start_pos = second_Text.transform.position.y;
        end_pos = second_Text.transform.position.y - dis;
        GameManager.current.onPanel1_Active += OnPanel1_Active;
    }

    private IEnumerator TypeWriter()
    {
        string[] array = fullText.Split(' ');
        first_text.GetComponent<Text>().text = array[0];
        for (int i = 1; i < fullText.Length; ++i)
        {
            yield return new WaitForSeconds(delay);
            first_text.GetComponent<Text>().text += " "+array[i];
        }
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
        characterController.StartHighlighting();
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
        yield return new WaitForSeconds(4f);
        Destroy(img2.gameObject);
        Destroy(img.gameObject);
        Destroy(first_text.gameObject);
        Destroy(second_Text.gameObject);
        Destroy(third_Text.gameObject);
    }

    private void OnDestroy()
    {
        GameManager.current.onPanel1_Active -= OnPanel1_Active;
    }
}

