using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressScript : MonoBehaviour 
{

	public float progress;
	public Text progressText; 
	public GameObject player;
	public PlayerMovement playerData;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerData = player.GetComponent<PlayerMovement>();
	}
	

	void FixedUpdate () 
	{
		progress = playerData.CalculateProgress();
		SetProgressText();
	}

	void SetProgressText()
	{
		progressText.text = "Progress: " + progress.ToString("F2");
	}
}
