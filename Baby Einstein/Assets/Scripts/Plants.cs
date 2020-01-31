using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Plants : MonoBehaviour
{
    public GameObject allCharacters;

    [SerializeField]
    private AnimationReferenceAsset[] anims;

    [SerializeField]
    private SkeletonAnimation skeletonAnimation;

    [SerializeField]
    private string currentState;

    [SerializeField]
    private string currentAnimation;

    private UIScript uiScript;

    void Start()
    {
        uiScript = GetComponent<UIScript>();
        GameManager.current.onPlantsInteract += OnPlantsInteract;
        currentState = anims[0].name;
        SetCharacterState(currentState);
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

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider != null)
            {
                Debug.Log("I'm hitting " + hit.collider.tag);
                if (hit.collider.gameObject.tag == "Plants")
                {
                      SetCharacterState(anims[1].name);
                      StartCoroutine(EnableTheCharacters());
                }
            }

        }


    }
    
    public IEnumerator EnableTheCharacters()
    {
        uiScript.GetComponent<UIScript>().EndAnimationText();
        yield return new WaitForSeconds(3f);

        foreach (var character in allCharacters.GetComponentsInChildren<BoxCollider2D>())
        {
            yield return new WaitForSeconds(3f);
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
