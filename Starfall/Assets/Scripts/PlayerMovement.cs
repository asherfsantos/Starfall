using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour 
{

	public GameObject player;
	public GameObject currentStar;
	public Rigidbody2D playerBody;
	public SpriteRenderer playerRenderer;
	public Sprite deathSprite;
	public Sprite frozenSprite;
	public Sprite idleSprite;
	public bool onStar = false;
	public bool canLand = true;
	public float speed = 10.0f;
	public float jumpForce;
	private float moveInput;
	private bool facingRight = false;
	public int maxJumps;
	public int jumpsRemaining;
	public int playerProgress;
	public float levelStartPos = -35.0f;
	public float levelEndPos = 45.0f;
	public Animator myAnim;
	public bool falling;
	public bool movingToCenter;
	private Vector3 velocity = Vector3.zero;
	public bool canMove = true;


	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerBody = player.GetComponent<Rigidbody2D>();
		playerRenderer = player.GetComponent<SpriteRenderer>();
		myAnim = player.GetComponent<Animator>();
		levelStartPos = GameObject.FindWithTag("Startpoint").transform.position.x;
		levelEndPos = GameObject.FindWithTag("Endpoint").transform.position.x;
		jumpsRemaining = maxJumps;
		canMove = true;

	}
	
	void FixedUpdate()
	{
		moveInput = Input.GetAxis("Horizontal");
		if(canMove)
		{
			playerBody.velocity = new Vector2(moveInput * speed, playerBody.velocity.y);
			if(!facingRight && moveInput > 0)
				Flip();
			else if(facingRight && moveInput < 0)
				Flip();
		}
		//print("Progress: " + CalculateProgress().ToString("F2"));

		myAnim.SetBool ("falling", falling);
		myAnim.SetBool ("onStar", onStar);
		HandleMovements();
		HandleInput();
	}
	// Update is called once per frame
	void Update () 
	{
		
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
		if(other.gameObject.CompareTag("Meteor"))
			other.gameObject.GetComponent<MeteorScript>().onMeteor = true;
		if(other.gameObject.CompareTag("Black Hole"))
			if(canLand)
				BlackHoleDeath(other);
		if(other.gameObject.CompareTag("Asteroid"))
			AsteroidDeath(other);
			
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
		//MoveTowardCenter();

		//if(movingToCenter == false)
		playerBody.gravityScale = 0;
		transform.position = currentStar.transform.position;
	}

	private void MoveTowardCenter()
	{
		/*if (transform.position == Vector3.MoveTowards(transform.position, currentStar.transform.position, (4.0f * Time.deltaTime)))
		{
			MoveWithStar();
			//transform.position = currentStar.transform.position;
		}
		else
		{	
			transform.position = Vector3.MoveTowards(transform.position, currentStar.transform.position, (4.0f * Time.deltaTime));
		}
		//MoveWithStar();*/
		// Define a target position above and behind the target transform

        // Smoothly move the camera towards that target position
		playerBody.gravityScale = 0;
        transform.position = Vector3.SmoothDamp(transform.position, currentStar.transform.position, ref velocity, 0.03f);
	}

	// manipulate player position
	private void HandleMovements()
	{
		// if riding a star, move player position with it
		//if(onStar)
		//	MoveTowardCenter();
		if(onStar)
		{
			//MoveWithStar();
			playerBody.gravityScale = 0;
			MoveTowardCenter();
		}
	}

	private void HandleInput()
	{
		if(canMove)
		{
			if((Input.GetKeyDown(KeyCode.Space)) && (jumpsRemaining > 0))
			{
				playerBody.gravityScale = 0.5f;
				canLand = false;
				onStar = false;
				playerBody.velocity = Vector2.up * jumpForce;
				jumpsRemaining--;
				falling = false;
			}
			else if(Input.GetKeyUp(KeyCode.Space))
			{
				canLand = true;
				falling = true;
			}
		}
		if(!canMove)
			canLand = false;
	}

	private void RefillFuel()
	{
		jumpsRemaining = maxJumps;
	}

	public void PlayerDies()
	{
		playerRenderer.sprite = deathSprite;
	}

	public void FreezePlayer()
	{
		playerRenderer.sprite = frozenSprite;
	}

	public void UnfreezePlayer()
	{
		playerRenderer.sprite = idleSprite;
	}

	public void BlackHoleDeath(Collider2D blackHole)
	{
		transform.position = blackHole.transform.position;
		playerRenderer.sprite = deathSprite;
	}

	public void AsteroidDeath(Collider2D asteroid)
	{
		transform.position = asteroid.transform.position;
		playerRenderer.sprite = deathSprite;
	}

	public float CalculateProgress()
	{
		return transform.position.x / (levelEndPos - levelStartPos);
	}
}
