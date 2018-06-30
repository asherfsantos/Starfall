using System.Collections;
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
		HandleMovements();
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		HandleInput();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// set current star player is riding on
		if(other.gameObject.CompareTag("Star"))
			currentStar = other.gameObject;
			if(canLand)
			{
				onStar = true;
				MoveWithStar();
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
}
