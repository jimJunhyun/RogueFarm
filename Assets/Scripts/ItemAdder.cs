using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdder : MonoBehaviour
{
    public List<Seed> allSeeds;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
            AddSeed(allSeeds[Random.Range(0, allSeeds.Count)], 1);
    }

    void AddSeed(Seed s, int cnt)
	{
        Inventory.instance.AddItem(s, cnt);
	}
}
