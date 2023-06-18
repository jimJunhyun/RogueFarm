using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Seed Data", order = 1)]
public class Seed : ScriptableObject
{
    public string seedName;
    public Sprite icon;
    public GameObject prefab;
}
