using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController current;


    [SerializeField]
    private List<Character> characters;

    private void Awake()
    {
        current = this;
    }
    public void CancelTweenOnAnimals()
    {
        foreach (var character in characters)
        {
            LeanTween.cancel(character.gameObject);

        }

    }

    public void StartHighlighting()
    {
        Debug.Log("Start Highlighting");
        foreach (var character in characters)
        {
            character.HIghlightObject();
        }
    }

    public void ResetScale()
    {
        foreach(var character in characters)
        {
            character.Rescale();
        }
    }
}
