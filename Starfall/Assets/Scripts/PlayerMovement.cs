using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody2D player;
	public GameObject currentStar;
	private bool onStar = true;
	// Use this for initialization
	void Start () 
	{
		//player = FindObjec
	}
	
	// Update is called once per frame
	void Update () 
	{
		MoveWithStar();
	}

	void OnTriggerEnter(Collider other)
	{

		if(other.tag == "Star")
			currentStar = other.gameObject;
			transform.position = other.transform.position;
			onStar = true;
	}

	void MoveWithStar()
	{
		if(onStar)
		{
			transform.position = currentStar.transform.position;
		}
	}
}
