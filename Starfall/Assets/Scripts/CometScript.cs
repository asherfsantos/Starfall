using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometScript : MonoBehaviour 
{
	public GameObject player;
	public PlayerMovement playerScript;
	public bool onComet = false;
	public float landingTime;
	private float exitTime;
	public float timeLimit = 1.5f;
	public float freezeDuration = 3.0f;
	public float frozenStartTime;
	public float frozenEndTime;
	public bool frozen = false;
	public bool timerOn = false;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerScript = player.GetComponent<PlayerMovement>();
		frozen = false;
		timeLimit = 1.5f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckFrozen();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			onComet = true;
			playerScript.canLand = false;
			StartCometTimer();
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		onComet = false;
		//EndTimer();
	}

	void StartCometTimer()
	{
		if(onComet)
		{
			landingTime = Time.time;
			frozenStartTime = landingTime + timeLimit;
			frozenEndTime = frozenStartTime + freezeDuration;
		}
	}

	/*void EndTimer()
	{
		exitTime = Time.time;
	}*/

	/*void startFrozenTimer()
	{
		if(Time.time < frozenEndTime)
			frozen = true;
		if(Time.time > frozenEndTime)
		{
			print("Player Unfrozen");
			frozen = false;
		}
	}*/

	/*void ResetTimers()
	{
		StartCometTimer();
	}*/

	void CheckFrozen()
	{
		if(onComet)
		{
			if(Time.time > frozenStartTime && Time.time < frozenEndTime )
			{
				//print(Time.time);
				frozen = true;
			}
			else			
			{
				frozen = false;
				//StartCometTimer();
				//frozenStartTime = Time.time + 1.0f;
			}
		}
		if(frozen)
		{
			frozen = true;
			//print("Frozen");
			playerScript.FreezePlayer();
			//playerScript.PlayerDies();
		}
		if(!frozen && onComet)
		{
			frozen = false;
			//print("Unfrozen");
			playerScript.UnfreezePlayer();
			//StartCometTimer();
		}
	}
}
