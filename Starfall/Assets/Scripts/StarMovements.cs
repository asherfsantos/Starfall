using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMovements : MonoBehaviour 
{

	public float movementSpeed;
	public float startPosition;
	public float endPosition;

	// Use this for initialization
	void Start () 
	{
		movementSpeed = Random.Range(0.5f, 3.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		moveStar();
		
	}

	void moveStar()
	{
        float step = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Camera.main.ViewportToWorldPoint(new Vector3(0,1,0)), step);
	}
}
