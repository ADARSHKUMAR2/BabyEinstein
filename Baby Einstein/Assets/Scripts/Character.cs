using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField]
    private AnimationReferenceAsset[] anims;

    [SerializeField]
    private SkeletonAnimation skeletonAnimation;

    [SerializeField]
    private string currentState;

    [SerializeField]
    private string currentAnimation;


    private void Start()
    {
        currentState = anims[0].name;
        SetCharacterState(currentState);
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
                    SetCharacterState(anims[1].name);
                }
                //if(hit.collider.gameObject.tag == "Plants")
                //{
                //    this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                //}
            }
            
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

}
