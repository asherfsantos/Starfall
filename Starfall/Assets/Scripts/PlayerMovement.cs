﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

	public GameObject player;
	public GameObject currentStar;
	public bool onStar = false;
	public bool canLand = true;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		HandleMovements();
		HandleInput();
	}

	void OnTriggerStay2D(Collider2D other)
	{
		// set current star player is riding on
		if(other.gameObject.CompareTag("Star"))
			LandOnStar(other.gameObject);
			
	}

	void LandOnStar(GameObject star)
	{
		if(canLand && !onStar)
			{
				currentStar = star;
				onStar = true;
				MoveWithStar();
				//MoveToStarCenter();
			}
	}
	// move player position with star position
	void MoveWithStar()
	{
		transform.position = currentStar.transform.position;
	}

	// manipulate player position
	void HandleMovements()
	{
		// if riding a star, move player position with it
		if(onStar)
			MoveWithStar();
	}

	private void HandleInput()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			canLand = false;
			onStar = false;
		}
		else
			canLand = true;
	}


	void MoveToStarCenter()
	{
		float movementSpeed = 2.0f;
		float step = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentStar.transform.position, step);
	}
}
