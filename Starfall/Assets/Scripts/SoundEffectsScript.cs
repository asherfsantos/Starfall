using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsScript : MonoBehaviour 
{
	public AudioSource sfxPlayer;
	public AudioClip explosion;
	public AudioClip jetpack;
	// Use this for initialization
	void Start () 
	{
		sfxPlayer = GameObject.FindWithTag("SFX Player").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void PlayExplosion()
	{
		sfxPlayer.PlayOneShot(explosion, 0.5f);
	}

	public void PlayJetpack()
	{
		sfxPlayer.PlayOneShot(jetpack, 1f);
	}
}
