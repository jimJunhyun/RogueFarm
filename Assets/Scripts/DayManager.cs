using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager instance;

	public List<Animal> allAnimals;

	public int dayCount = 1;

	public float secondPerDay;

	float passedT = 0;
	System.Action onUpdatedDay;
	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		onUpdatedDay?.Invoke();
	}

	private void Update()
	{
		passedT += Time.deltaTime;
		if(passedT >= secondPerDay)
		{
			dayCount += 1;
			passedT = 0;
			CheckAnimals();
			onUpdatedDay?.Invoke();
		}
	}

	public void AddAnimal(Animal a)
	{
		allAnimals.Add(a);
	}

	public void RemoveAnimal(Animal a)
	{
		allAnimals.Remove(a);
	}

	public void CheckAnimals()
	{
		for (int i = 0; i < allAnimals.Count; i++)
		{
			if (allAnimals[i].CheckHunger())
			{
				
				allAnimals[i].OnDead();
				--i;
			}
		}
		for (int i = 0; i < allAnimals.Count; i++)
		{
			allAnimals[i].OnDayChanged.Invoke();
		}
	}

	public void AddDayUpdBhv(System.Action act)
	{
		onUpdatedDay += act;
	}
}
