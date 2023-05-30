using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomGrass : MonoBehaviour
{

    public GameObject grass;
    public float mapX;
    public float mapY;

	private void Awake()
	{
		GenGrass();
	}

	void GenGrass()
	{
		float rand = Random.Range(0f, 1000f);
		for (int y = 1; y <= mapY; y++)
		{
			for (int x = 1; x <= mapX; x++)
			{
				if(Mathf.PerlinNoise(x / mapX + rand, y / mapY + rand) > 0.5)
				{
					Vector3 pos = new Vector3(x - mapX / 2, 0, y - mapY / 2);
					Instantiate(grass, pos, Quaternion.identity);
				}
			}
		}
	}
}
