using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

	public GameObject player;
	public GameObject currentStar;
	public Rigidbody2D playerBody;
	public bool onStar = false;
	public bool canLand = true;
	public float speed = 100.0f;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerBody = player.GetComponent<Rigidbody2D>();;
	}
	
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		transform.Translate(new Vector3(moveHorizontal, moveVertical) * Time.deltaTime * speed);
		//playerBody.AddForce (movement * speed);
	}
	// Update is called once per frame
	void Update () 
	{
		HandleMovements();
		HandleInput();
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		// set current star player is riding on
		if(other.gameObject.CompareTag("Star"))
			LandOnStar(other.gameObject);
			
	}

	private void LandOnStar(GameObject star)
	{
		if(canLand && !onStar)
			{
				currentStar = star;
				onStar = true;
				HandleMovements();
			}
	}
	// move player position with star position
	private void MoveWithStar()
	{
		transform.position = currentStar.transform.position;
	}

	// manipulate player position
	private void HandleMovements()
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
