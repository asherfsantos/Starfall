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
	private bool onComet = false;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerScript = player.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		CometTimer();
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			if(!timerStarted)
				StartTimer();
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		timerStarted = false;
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
			print("time limit reached");
			print("with fuel: " + playerScript.jumpsRemaining);
			ReduceFuel();
			print("reduced to: " + playerScript.jumpsRemaining);
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
