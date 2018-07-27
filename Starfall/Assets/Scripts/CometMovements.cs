using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometMovements : MonoBehaviour 
{
	public float movementSpeed;
	GameObject player;
	PlayerMovement playerData;
	public Vector3 topLeft = new Vector3(-1.5f, 7.0f);
	public Vector3 bottomRight = new Vector3(81.0f, -8.0f);
	public Rigidbody2D playerBody;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerData = player.GetComponent<PlayerMovement>();
		playerBody = player.GetComponent<Rigidbody2D>();
		movementSpeed = Random.Range(0.5f, 3.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		MoveComet();
	}

	void MoveComet()
	{
        float step = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, topLeft, step);
		if(transform.position == topLeft)
		{	
			//SetPlayerOffStar();
			Destroy(gameObject);
		}
	}

	void DestroySlowerComet(GameObject other)
	{
		if(other.gameObject.CompareTag("Comet"))
		{
			if(movementSpeed < other.GetComponent<StarMovements>().movementSpeed)
			{
				//if(playerData.currentStar == gameObject)
					//SetPlayerOffStar();
				Destroy(gameObject);
			}
			else
			{
				//if(playerData.currentStar == other)
					//SetPlayerOffStar();
				Destroy(other);
			}
		}
	}

	void SetPlayerOffComet()
	{
		playerData.onStar = false;
		playerData.canLand = true;
		playerBody.gravityScale = 0.5f;
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
			LandOnMeteor(other.gameObject);
		if(other.gameObject.CompareTag("Wall"))
		{
			Destroy(gameObject);
		}
	}

	private void LandOnMeteor(GameObject player)
	{
		/*if(canLand && !onStar)
		{
			currentStar = star;
			onStar = true;
			RefillFuel();
			HandleMovements();
		}*/
		HandleMovements();
	}

	private void MoveWithStar()
	{
		//MoveTowardCenter();

		//if(movingToCenter == false)
		playerBody.gravityScale = 0;
		playerBody.transform.position = transform.position;
	}

	private void HandleMovements()
	{
		// if riding a star, move player position with it
		//if(onStar)
		//	MoveTowardCenter();
		/*if(onStar)
		{
			//MoveWithStar();
			playerBody.gravityScale = 0;
			MoveTowardCenter();
		}*/
		MoveTowardCenter();
	}

	private void MoveTowardCenter()
	{
		Vector3 colliderPosition;
		CircleCollider2D currentCollider;
		currentCollider = gameObject.GetComponent<CircleCollider2D>();
		colliderPosition = new Vector3(currentCollider.offset.x, currentCollider.offset.y, 0f);
		playerBody.gravityScale = 0;
        //transform.position = Vector3.SmoothDamp(transform.position, currentStar.transform.position, ref velocity, 0.03f);
		transform.position = Vector3.SmoothDamp(transform.position, currentStar.transform.position + colliderPosition, ref velocity, 0.03f);
	}
}
