using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPointScript : MonoBehaviour 
{
	public SceneLoader loader;
	public GameObject player;
	public Rigidbody2D playerBody;
	public bool sceneLoading = false;
	public PlayerMovement playerData;
	public BGMusicScript bgMusic;
	public AudioSource mainCamera;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		playerBody = player.GetComponent<Rigidbody2D>();
		playerData = player.GetComponent<PlayerMovement>();
		mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
		//bgMusic = GameObject.FindWithTag("Main Camera").GetComponent<BGMusicScript>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			StartCoroutine (BGMusicScript.FadeOut(mainCamera, 0.1f));
			playerData.canLand = false;
			playerData.canMove = false;
			playerBody.position = transform.position;
			playerData.StopMovements();
			/*if(!sceneLoading)
			{
				sceneLoading = true;
				loader.LoadScreen(SceneManager.GetActiveScene().buildIndex +1);
			}*/
			if(!sceneLoading)
			{
				Invoke("LoadScene", 0.1f);
			}
		}
	}

	private void LoadScene()
	{
		if(!sceneLoading)
			{
				sceneLoading = true;
				loader.LoadScreen(SceneManager.GetActiveScene().buildIndex +1);
			}
	}
			
}
