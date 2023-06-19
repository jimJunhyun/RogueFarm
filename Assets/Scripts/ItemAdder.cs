using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemAdder : MonoBehaviour
{
    //public List<Products> allSeeds;
    //// Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetMouseButtonDown(1))
    //        AddProduct(allSeeds[Random.Range(0, allSeeds.Count)], 1);
    //}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadShopData.instance.OnShop();
        }
    }

    public static void AddProduct(Products s, int cnt)
	{
        if(s.myType == FoodType.Herb)
		{
            Inventory.instance.AddItem(s, cnt);
		}
        else if(s.myType == FoodType.Game)
		{
            
			for (int i = 0; i < cnt; i++)
            {
                if (DayManager.instance.allAnimals.Count <= 30)
                {
                    NavMeshHit hit;
                    NavMesh.SamplePosition(Random.insideUnitSphere * 50, out hit, 50, NavMesh.AllAreas);
                    GameObject ins = Instantiate(s.prefab, hit.position, Quaternion.identity);
                    DayManager.instance.AddAnimal(ins.GetComponent<Animal>());
                }
		    } 
        }
	}
}
