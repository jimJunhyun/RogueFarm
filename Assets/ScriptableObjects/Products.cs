using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Create Product Data", order = 1)]
public class Products : ScriptableObject
{
    public string myName;
    public int myPrice;
    public Sprite icon;
    public GameObject prefab;

    public FoodType myType;

    public string InfoText;
}
