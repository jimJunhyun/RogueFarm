using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrowStat
{
	Seed, //��
	Bush, //���� Ǯ
	Plant, //ū Ǯ
	Tree, //����
}

[Serializable]
public struct GrowSteps
{
	public GrowStat state;
	public float proceedTime;
}

public class GrowSeed : MonoBehaviour, IEatable
{
	public string entityName;

	[Tooltip("�ð������� �� => �Ʒ�")]
	public List<GrowSteps> growSteps;
	

	public Vector3 position => transform.position;

	public int type => ((int)FoodType.Herb);

	public Action<Animal> onBeingSetTarget { get; set; }

	public void OnEaten(Animal by)
	{
		Debug.Log(by.entityName + "���� ���� �Ĺ�");
	}

	void Awake()
    {
        
    }
}
