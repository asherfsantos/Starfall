﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawns : MonoBehaviour 
{
	public GameObject star;
	public float nextSpawn = 0.0f;
	public float spawnRate = 1.0f;
	public float spawnLocationX = 20.0f;
	public float lowerY = -5.0f;
	public float upperY = -3.0f;
	public float randomY;
	public Vector2 spawnLocation;


	// Use this for initialization
	void Start () 
	{
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
			Instantiate(star, spawnLocation, Quaternion.identity);
		}
	}
}
