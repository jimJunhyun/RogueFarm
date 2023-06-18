using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemAdder : MonoBehaviour
{
    public List<Products> allSeeds;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
            AddSeed(allSeeds[Random.Range(0, allSeeds.Count)], 1);
    }

    void AddSeed(Products s, int cnt)
	{
        if(s.myType == FoodType.Herb)
		{
            Inventory.instance.AddItem(s, cnt);
		}
        if(s.myType == FoodType.Game)
		{
            NavMeshHit hit;
            NavMesh.SamplePosition(Random.insideUnitSphere * 100, out hit, 100f, NavMesh.AllAreas);
            Instantiate(s.prefab, hit.position, Quaternion.identity);
		}
	}
}
