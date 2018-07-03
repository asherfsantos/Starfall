using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometScript : MonoBehaviour 
{
	public GameObject player;
	public PlayerMovement playerScript;
	private float landingTime;
	private bool timerStarted = false;
	public float timeLimit = 3.0f;
	public bool onComet = false;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerScript = player.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(onComet)
			CometTimer();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Player"))
			onComet = true;
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		onComet = false;
	}

	void StartTimer()
	{
		timerStarted = true;
		landingTime = Time.time;
	}

	void CometTimer()
	{
		if(Time.time > landingTime + timeLimit)
		{
			print("jumps remaining: " + playerScript.jumpsRemaining);
			ReduceFuel();
		}
	}

	void ReduceFuel()
	{
		if(playerScript.jumpsRemaining == 0)
			playerScript.PlayerDies();
		else
			playerScript.jumpsRemaining--;
			landingTime = Time.time;
	}
}
