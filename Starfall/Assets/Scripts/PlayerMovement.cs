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
	public float speed = 10.0f;
	public float jumpForce;
	private float moveInput;
	private bool facingRight = false;
	public int maxJumps;
	public int jumpsRemaining;


	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerBody = player.GetComponent<Rigidbody2D>();;
	}
	
	void FixedUpdate()
	{
		moveInput = Input.GetAxis("Horizontal");
		playerBody.velocity = new Vector2(moveInput * speed, playerBody.velocity.y);

		if(!facingRight && moveInput > 0)
			Flip();
		else if(facingRight && moveInput < 0)
			Flip();
	}
	// Update is called once per frame
	void Update () 
	{
		HandleMovements();
		HandleInput();
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
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
			RefillFuel();
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
		if((Input.GetKeyDown(KeyCode.Space)) && (jumpsRemaining > 0))
		{
			canLand = false;
			onStar = false;
			playerBody.velocity = Vector2.up * jumpForce;
			jumpsRemaining--;
			print("Jumps left: " + jumpsRemaining);
		}
		else if(Input.GetKeyUp(KeyCode.Space))
			canLand = true;
	}

	private void RefillFuel()
	{
		jumpsRemaining = maxJumps;
	}
}
