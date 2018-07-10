﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMovements : MonoBehaviour 
{
	public float movementSpeed;
	GameObject player;
	PlayerMovement playerData;
	public Vector3 topLeft = new Vector3(-1.0f, 7.0f);
	public Vector3 bottomRight = new Vector3(81.0f, -8.0f);
	public Sprite purpleStar, pinkStar, whiteStar;
	public SpriteRenderer starRenderer;
	private bool colorChosen = false;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerData = player.GetComponent<PlayerMovement>();
		movementSpeed = Random.Range(0.5f, 3.0f);
		starRenderer = GetComponent<SpriteRenderer>();
		StarColor();
	}
	
	// Update is called once per frame
	void Update () 
	{
		MoveStar();
	}

	// move star toward top left of screen
	void MoveStar()
	{
        float step = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, topLeft, step);
		if(transform.position == topLeft)
			Destroy(gameObject);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Star"))	
		{
			DestroySlowerStar(other.gameObject);
		}
	}

	void DestroySlowerStar(GameObject other)
	{
		if(other.gameObject.CompareTag("Star"))
		{
			if(movementSpeed < other.GetComponent<StarMovements>().movementSpeed)
			{
				Destroy(gameObject);
				if(playerData.currentStar == gameObject)
					SetPlayerOffStar();
			}
			else
			{
				Destroy(other);
				if(playerData.currentStar == other)
					SetPlayerOffStar();
			}
		}
	}

	void SetPlayerOffStar()
	{
		playerData.onStar = false;
		playerData.canLand = true;
	}

	void DestroyOffScreen()
	{
		if(transform.position == Camera.main.ViewportToWorldPoint(new Vector3(0,1,0)))
			Destroy(gameObject);
	}

	void StarColor()
	{
		int randomNumber = Random.Range(0, 3);
		switch (randomNumber)
		{
			case 0:
				starRenderer.sprite = purpleStar;
				break;
			case 1:
				starRenderer.sprite = whiteStar;
				break;
			case 2:
				starRenderer.sprite = pinkStar;
				break;
		}
		colorChosen = true;
		
	}
}
