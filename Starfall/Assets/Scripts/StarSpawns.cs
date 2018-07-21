using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawns : MonoBehaviour 
{
	public GameObject star;
	public float nextSpawn = 0.0f;
	public float spawnRate = 1.0f;
	public float spawnLocationX = 81.0f;
	public float upperY = -4.0f;
	public float lowerY = -8.0f;
	public float randomY;
	public Vector2 spawnLocation;
	public GameObject starsParent;
	public GameObject newStar;

	// Use this for initialization
	void Start () 
	{
		starsParent = GameObject.FindGameObjectWithTag("Stars Parent");
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
			spawnLocation = new Vector2(spawnLocationX, randomY);
			newStar = Instantiate(star, spawnLocation, Quaternion.identity);
			newStar.transform.parent = starsParent.transform;
		}
	}

}
