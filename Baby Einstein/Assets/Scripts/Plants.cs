using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    public GameObject allCharacters;
    // Start is called before the first frame update
    void Start()
    {

        GameManager.current.onPlantsInteract += OnPlantsInteract;
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    foreach(var character in allCharacters.GetComponentsInChildren<BoxCollider2D>())
                    {
                        character.enabled = true;
                    }
                    //gameObject.GetComponent<Character>().
                }
                //if(hit.collider.gameObject.tag == "Plants")
                //{
                //    this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                //}
            }

        }


    }
}
