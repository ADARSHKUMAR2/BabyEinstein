using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsController : MonoBehaviour
{
    public static PlantsController current;

    [SerializeField]
    private List<Plants> plants;

    private void Awake()
    {
        current= this;
    }

    public void CancelTweenOnPlants()
    {
        foreach(var plant in plants)
        {
            LeanTween.cancel(plant.gameObject);
        }

    }


}
