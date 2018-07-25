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
	public float gameDuration = 120.0f;
	public float timeLeft;
	public GameObject pausePanel;
	public GameObject diedMenu;
	public AudioClip deathAudio;
	public AudioSource gameplaySound;
	public bool playerLiving = true;


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
		timeLeft = gameDuration;
		pausePanel = GameObject.FindWithTag("Pause Panel");
		pausePanel.SetActive(false);
		diedMenu = GameObject.FindWithTag("Died Menu");
		diedMenu.SetActive(false);
	}
	
	void FixedUpdate()
	{
		UpdateTime();
		moveInput = Input.GetAxis("Horizontal");
		if(canMove)
		{
			playerBody.velocity = new Vector2(moveInput * speed, playerBody.velocity.y);
			if(!facingRight && moveInput > 0)
				Flip();
			else if(facingRight && moveInput < 0)
				Flip();
		}

		myAnim.SetBool ("falling", falling);
		myAnim.SetBool ("onStar", onStar);
		HandleMovements();
		//HandleInput();
	}
	// Update is called once per frame
	void Update () 
	{
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
		if(other.gameObject.CompareTag("Wall"))
		{
			canMove = false;
			Invoke("PlayerDies", 2.0f);
			Invoke("EnablePauseMenu", 2.0f);
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Wall")
			PlayerDies();
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
		print("Player Died");
		playerLiving = false;
		gameplaySound.PlayOneShot(deathAudio, 0.08f);
		playerRenderer.sprite = deathSprite;
		playerBody.constraints = RigidbodyConstraints2D.FreezeAll;
		canMove = false;
		canLand = false;
		diedMenu.SetActive(true);
	}

	public void FreezePlayer()
	{
		print("Player Froze");
		playerRenderer.sprite = frozenSprite;
		playerBody.constraints = RigidbodyConstraints2D.FreezeAll;
		canMove = false;
	}

	public void UnfreezePlayer()
	{
		print("Player Unfroze");
		if(playerLiving)
		{
			playerRenderer.sprite = idleSprite;
			playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
			canMove = true;
		}
	}

	/*public void BlackHoleDeath(Collider2D blackHole)
	{
		transform.position = blackHole.transform.position;
		playerRenderer.sprite = deathSprite;
	}*/

	/*public void AsteroidDeath(Collider2D asteroid)
	{
		transform.position = asteroid.transform.position;
		playerRenderer.sprite = deathSprite;
	}*/

	public float CalculateProgress()
	{
		return transform.position.x / (levelEndPos - levelStartPos);
	}

	public void UpdateTime()
	{
		if(!pausePanel.activeSelf)
		{
			timeLeft -= Time.deltaTime;
			CheckTime();
		}
	}

	public void CheckTime()
	{
		if(timeLeft <= 0.0f)
		{
			PlayerDies();
		}
	}

	public void EnablePauseMenu()
	{
		diedMenu.SetActive(true);
	}
}
