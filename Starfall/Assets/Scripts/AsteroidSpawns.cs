using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawns : MonoBehaviour 
{
	public GameObject asteroid;
	public GameObject asteroid1;
	public GameObject asteroid2;
	public GameObject asteroid3;
	public GameObject asteroid4;
	public GameObject asteroid5;
	public float nextSpawn = 0.0f;
	public float spawnRate = 5.0f;
	public float spawnLocationX = 20.0f;
	public float lowerY = -5.0f;
	public float upperY = 5.0f;
	public float randomY;
	public Vector2 spawnLocation;
	public GameObject asteroidsParent;
	public GameObject newAsteroid;

	// Use this for initialization
	void Start () 
	{
		asteroidsParent = GameObject.FindGameObjectWithTag("Asteroids Parent");
	}
	
	// Update is called once per frame
	void Update () 
	{
		SpawnAsteroid();
	}

	void SpawnAsteroid()
	{
		if(Time.time > nextSpawn)
		{
			nextSpawn = Time.time + spawnRate;
			randomY = Random.Range(lowerY, upperY);
			spawnLocation = new Vector2(spawnLocationX, randomY);
			//Instantiate(asteroid, spawnLocation, Quaternion.identity);
			newAsteroid = Instantiate(asteroid, spawnLocation, Quaternion.identity);
			newAsteroid.transform.parent = asteroidsParent.transform;
		}
	}
}
