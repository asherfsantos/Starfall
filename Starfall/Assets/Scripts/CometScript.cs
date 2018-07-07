using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometScript : MonoBehaviour 
{
	public GameObject player;
	public PlayerMovement playerScript;
	private float landingTime;
	public float timeLimit = 3.0f;
	public bool onComet = false;
	public float freezeDuration = 3.0f;
	public float frozenStartTime;
	public float frozenEndTime;
	public bool frozen = false;
	public Vector3 frozenPosition;

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
		{
			onComet = true;
			StartTimer();
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		onComet = false;
	}

	void StartTimer()
	{
		landingTime = Time.time;
	}

	void CometTimer()
	{
		if(Time.time > landingTime + timeLimit)
		{
			frozenStartTime = Time.time;
			frozenEndTime = frozenStartTime + freezeDuration;
			frozenPosition = player.transform.position;
			FreezePlayer();
		}
		else
			frozen = false;

	}

	void FreezePlayer()
	{
		if(Time.time < frozenEndTime)
		{
			frozen = true;
			print("Player Frozen");
			playerScript.FreezePlayer();
		}	
		if(Time.time > frozenEndTime)
		{
			frozen = false;
			print("Player unfrozen");
			playerScript.UnfreezePlayer();
		}
		if(frozen)
		{
			player.transform.position = frozenPosition;
			//or disable input
		}	
	}
}
