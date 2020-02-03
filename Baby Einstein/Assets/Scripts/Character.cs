using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Character : MonoBehaviour
{
    private UIScript uiScript;
    [SerializeField]
    private AnimationReferenceAsset[] anims;

    [SerializeField]
    private SkeletonAnimation skeletonAnimation;

    [SerializeField]
    private string currentState;

    [SerializeField]
    private string currentAnimation;
    private bool isAnimalClicked = false;

    private Vector3 scaleChange;
    private Vector3 currentScale;
    private float duration = 2f;
    private float incrementScale = 0.4f;

    public GameObject otherAnimals;
    
    private CharacterController characterController;
    private void Start()
    {
        characterController = GameManager.current.GetComponent<CharacterController>();
        uiScript = GetComponent<UIScript>();
        currentState = anims[0].name;
        SetCharacterState(currentState);
        scaleChange = new Vector3(incrementScale, incrementScale, 1f);
        currentScale = transform.localScale;
        
    }

    

    private void FixedUpdate()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction,Mathf.Infinity);
            if (hit.collider != null)
            {
                Debug.Log("I'm hitting " + hit.collider.tag);
                if (hit.collider.gameObject.tag == gameObject.tag)
                {
                    isAnimalClicked = true;
                    characterController.CancelTweenOnAnimals();
                    SetCharacterState(anims[1].name);
                    uiScript.EndAnimationText2();
                    StartCoroutine(ActivateOtherAnimals());
                }
            }
            
        }

        
    }

    public IEnumerator ActivateOtherAnimals()
    {
        foreach (var character in otherAnimals.GetComponentsInChildren<BoxCollider2D>())
        {
            yield return new WaitForSeconds(2f);
            character.enabled = true;
        }


    }

    public void SetAnimation(AnimationReferenceAsset animation,bool loop,float timeScale)
    {
        if(animation.name.Equals(currentAnimation))
        {
            return;
        }

        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    public void SetCharacterState(string state)
    {
        if(state.Equals(anims[0].name))
        {
            SetAnimation(anims[0], true, 1f);
        }

        else if(state.Equals(anims[1].name))
        {
            SetAnimation(anims[1], false, 1f);
            StartCoroutine(IDleAnim());
        }
    }

    private IEnumerator IDleAnim()
    {
        yield return new WaitForSeconds(1f);
        SetAnimation(anims[0], true, 1f);
        
    }

    public void HIghlightObject()
    {
        StartCoroutine(WaitForScale());
    }

    public IEnumerator WaitForScale()
    {
        yield return new WaitForSeconds(5f);
        ScaleUp();
    }

    public void ScaleUp()
    {
        //if (!isAnimalClicked)
        //{
        LeanTween.scale(this.gameObject, currentScale + scaleChange, duration).setOnComplete(ScaleDown);
        //}
        
    }

    public void ScaleDown()
    {
        //if (!isAnimalClicked)
        //{
            LeanTween.scale(this.gameObject, currentScale, duration).setOnComplete(ScaleUp);
        //}

    }
}
