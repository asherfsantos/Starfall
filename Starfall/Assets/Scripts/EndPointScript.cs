using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointScript : MonoBehaviour 
{
	public SceneLoader loader;
	public GameObject player;
	public Rigidbody2D playerBody;
	public bool sceneLoading = false;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerBody = player.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		// set current star player is riding on
		print("player here");
		if(other.gameObject.CompareTag("Player"))
		{
			playerBody.position = transform.position;
			if(!sceneLoading)
			{
				sceneLoading = true;
				loader.LoadScreen(2);
			}
		}
	}
			
}
