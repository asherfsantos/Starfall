﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour 
{

	GameObject player;
	PlayerMovement playerData;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerData = player.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
