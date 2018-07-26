using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour 
{
	public List<CanvasGroup> canvasList;
	public CanvasGroup canvas1;
	public CanvasGroup canvas2;
	public CanvasGroup canvas3;
	public CanvasGroup canvas4;
	public CanvasGroup canvas5;
	public CanvasGroup canvas6;
	public int counter;
	public float canvasTime;
	public CanvasGroup currentCanvas;
	// Use this for initialization
	void Start () 
	{
		canvasList = new List<CanvasGroup>(new CanvasGroup[]{canvas1, canvas2, canvas3, canvas4, canvas5, canvas6});
		counter = 0;
		canvasTime = 15.36f;
		foreach(CanvasGroup canvas in canvasList)
		{
			canvas.gameObject.SetActive(false);
		}
		currentCanvas = canvasList[counter];
		canvasList[counter].gameObject.SetActive(true);
		Invoke("NextCanvas",canvasTime);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		
	}

	void NextCanvas()
	{
		if(counter < canvasList.Count-1)
		{
			print(canvasList[counter]);
			canvasList[counter].gameObject.SetActive(false);
			canvasList[++counter].gameObject.SetActive(true);
			Invoke("NextCanvas", canvasTime);
		}
	}
}
