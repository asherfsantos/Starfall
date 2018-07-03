using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour 
{
	public GameObject player;
	public PlayerMovement playerScript;
	private float landingTime;
	public float timeLimit = 3.0f;
	public bool onMeteor = false;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerScript = player.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(onMeteor)
			MeteorTimer();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			onMeteor = true;
			StartTimer();
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		onMeteor = false;
	}

	void StartTimer()
	{
		landingTime = Time.time;
	}

	void MeteorTimer()
	{
		if(Time.time > landingTime + timeLimit)
		{
			ReduceFuel();
		}
	}

	void ReduceFuel()
	{
		if(playerScript.jumpsRemaining == 0)
			playerScript.PlayerDies();
		else
		{
			landingTime = Time.time;
			playerScript.jumpsRemaining--;
		}
	}
}
