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


    private void FixedUpdate()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction,Mathf.Infinity);
            if (hit.collider != null)
            {
                //cube.SetActive(true);
                Debug.Log("I'm hitting " + hit.collider.tag);
                if (hit.collider.gameObject.tag == gameObject.tag)
                    RunCoreAnim();
            }
            
        }

        
    }

    public void RunIdleAnim()
    {

        skeletonAnimation.AnimationState.SetAnimation(1, anims[0], true);
    }

    public void RunCoreAnim()
    {
        skeletonAnimation.AnimationState.SetAnimation(2, anims[1], false);
    }
}
