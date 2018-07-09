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
	public int count;
	public Text countText;


	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerBody = player.GetComponent<Rigidbody2D>();
		playerRenderer = player.GetComponent<SpriteRenderer>();
	}
	
	void FixedUpdate()
	{
		moveInput = Input.GetAxis("Horizontal");
		playerBody.velocity = new Vector2(moveInput * speed, playerBody.velocity.y);

		if(!facingRight && moveInput > 0)
			Flip();
		else if(facingRight && moveInput < 0)
			Flip();
		count = jumpsRemaining;
		SetCountText();
	}
	// Update is called once per frame
	void Update () 
	{
		HandleMovements();
		HandleInput();
		//SetCountText();
		
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
		}
		else if(Input.GetKeyUp(KeyCode.Space))
			canLand = true;
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
		//playerBody.gravityScale = 0;
		playerRenderer.sprite = deathSprite;
	}

	public void AsteroidDeath(Collider2D asteroid)
	{
		transform.position = asteroid.transform.position;
		//playerBody.gravityScale = 1;
		playerRenderer.sprite = deathSprite;
	}

	void SetCountText()
	{
		countText.text ="Jumps Remaining: " + count.ToString();
	}
}
