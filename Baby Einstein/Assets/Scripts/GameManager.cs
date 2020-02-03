using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager current;


    [SerializeField]
    private Text first_Text;

    private void Awake()
    {
        current = this;
    }

    public event Action onPanel1_Active;
    public event Action onPlayAudio;
    public event Action onPlantsInteract;
    public event Action onCharactersInteract;

    void Start()
    {
        StartCoroutine(PlayTheAudio());
        StartCoroutine(Panel1_Active());
        StartCoroutine(PlantsInteract());
        StartCoroutine(FirstTextOnScreen());
    }

    public IEnumerator FirstTextOnScreen()
    {
        yield return new WaitForSeconds(2f);
        first_Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        first_Text.gameObject.SetActive(false);
    }

    public IEnumerator Panel1_Active()
    {
        yield return new WaitForSeconds(7f);
        if(onPanel1_Active!=null)
        {
            onPanel1_Active();
        }
    }

    public IEnumerator PlayTheAudio()
    {
        yield return new WaitForSeconds(5f);
        if(onPlayAudio!=null)
        {
            onPlayAudio();
        }
    }

    public IEnumerator PlantsInteract()
    {
        yield return new WaitForSeconds(10f);
        if(onPlantsInteract!=null)
        {
            onPlantsInteract();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
