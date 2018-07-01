using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawns : MonoBehaviour 
{
	public GameObject player;
	public GameObject star;

	public float nextSpawn = 0.0f;
	public float spawnRate = 1.0f;
	public float spawnLocationX = 20f;
	public float lowerY = -5.0f;
	public float upperY = -3.0f;
	public float randomY;
	public Vector2 spawnLocation;


	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		SpawnStar();
	}

	void SpawnStar()
	{
		if(Time.time > nextSpawn)
			{
				nextSpawn = Time.time + spawnRate;
				randomY = Random.Range(lowerY, upperY);
				/*while ( (Mathf.Abs (prevSpawnLocation.x - randomX) < minSpaceBetween) || (Mathf.Abs (prevSpawnLocation.y - randomY)) < minSpaceBetween)
				{
					randomX = Random.Range(realLeft, realRight);
					randomY = Random.Range(realLower, realUpper);
				}*/
				spawnLocation = new Vector2(spawnLocationX, randomY);
				//spawnedPortal = Instantiate(portal, spawnLocation, Quaternion.identity);
				Instantiate(star, spawnLocation, Quaternion.identity);
				//prevSpawnLocation = spawnLocation;
				//Destroy(spawnedPortal, 1.0f);
			}
	}
}
