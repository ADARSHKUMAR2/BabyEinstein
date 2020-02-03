using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;

public class Plants : MonoBehaviour
{
    public GameObject allCharacters;
    private bool isRaycastEnabled = false;

    [SerializeField]
    private AnimationReferenceAsset[] anims;

    [SerializeField]
    private SkeletonAnimation skeletonAnimation;

    [SerializeField]
    private string currentState;

    [SerializeField]
    private string currentAnimation;

    private UIScript uiScript;
    private Vector3 scaleChange;
    private Vector3 currentScale;
    private float duration = 2f;
    private float incrementScale = 0.2f;

    public bool isPlantsClicked = false;
    private PlantsController plantsController;

    void Start()
    {
        plantsController =GameManager.current.GetComponent<PlantsController>();
        scaleChange = new Vector3(incrementScale,incrementScale,1f);
        currentScale = transform.localScale;
        uiScript = GetComponent<UIScript>();
        GameManager.current.onPlantsInteract += OnPlantsInteract;
        currentState = anims[0].name;
        SetCharacterState(currentState);
        StartCoroutine(DetectRaycast());
     
    }

    public IEnumerator DetectRaycast()
    {
        yield return new WaitForSeconds(9f);
        isRaycastEnabled = true;
        HighlightObject();
    }

    private void OnPlantsInteract()
    {
        if (gameObject.tag == "Plants")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if(isRaycastEnabled)
        {
            //for(float i=0;i<scaleChange;i+=0.02f)
            //{
            //    transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, 0, currentScale + i), transform.localScale.y);
            //}

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit.collider != null)
                {
                    Debug.Log("I'm hitting " + hit.collider.tag);
                    if (hit.collider.gameObject.tag == gameObject.tag)
                    {
                        SetCharacterState(anims[1].name);
                        isPlantsClicked = true;
                        //LeanTween.cancel();
                        plantsController.CancelTweenOnPlants();
                        StartCoroutine(EnableTheCharacters());
                        //foreach (var plant in plantsController.GetComponentsInChildren<Plants>())
                        //{
                        //   LeanTween.cancel(gameObject);
                        //}
                    }
                }

            }
        }

    }

    public void HighlightObject()
    {
        //float i = 0.0f;
        //float rate = (1.0f / duration) * speed;
        //while(i<1.0f)
        //{
        //    i += Time.deltaTime;
        //    transform.localScale = Vector3.Lerp(currentScale, currentScale + scaleChange, i);
        //    yield return null;
        //}
        ScaleUp();
    }

    public void ScaleUp()
    {
        if(!isPlantsClicked)
        {
             LeanTween.scale(this.gameObject, currentScale + scaleChange, duration).setOnComplete(ScaleDown);
        }
            
    }

    public void ScaleDown()
    {
        if(!isPlantsClicked)
        {
            LeanTween.scale(this.gameObject, currentScale, duration).setOnComplete(ScaleUp);
        }
            
    }

    public IEnumerator EnableTheCharacters()
    {
        uiScript.GetComponent<UIScript>().EndAnimationText();
        yield return new WaitForSeconds(3f);

        foreach (var character in allCharacters.GetComponentsInChildren<BoxCollider2D>())
        {
            yield return new WaitForSeconds(2f);
            character.enabled = true;
        }

        //gameObject.GetComponent<Character>()
    }
    
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }

        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    public void SetCharacterState(string state)
    {
        if (state.Equals(anims[0].name))
        {
            SetAnimation(anims[0], true, 1f);
        }

        else if (state.Equals(anims[1].name))
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
    