using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundRay : MonoBehaviour
{

	public Transform hittedObject = null;
	public Transform hitZone = null;

    void FixedUpdate()
	{
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		Debug.DrawRay(transform.position, fwd * 50, Color.green);

		int layerMask = 1 << 8;
		int layerMask1 = 1 << 9;

		//hittableObjects;
		RaycastHit objectHit;
		if (Physics.Raycast(transform.position, fwd, out objectHit, 50, layerMask)){
			if (hittedObject != objectHit.collider.transform)
			{
				Debug.Log(objectHit.collider.name);
				hittedObject = objectHit.collider.transform;
				//play sound of this object;
				if (objectHit.collider.transform.GetComponent<AudioClipHolder>() != null)
				{
					PlaySound(objectHit.collider.transform.GetComponent<AudioClipHolder>().audioClip);
				}
			}
			else
			{
				/*if (objectHit.collider.transform.GetComponent<AudioClipHolder>() != null)
					PlaySoundAgain(objectHit.collider.transform.GetComponent<AudioClipHolder>().audioClip);*/
			}
		}

		//Zones
		RaycastHit objectHitZones;
		if (Physics.Raycast(transform.position, fwd, out objectHitZones, 50, layerMask1))
		{
			if (hitZone != objectHitZones.collider.transform)
			{
				Debug.Log(objectHitZones.collider.name);
				hitZone = objectHitZones.collider.transform;
				//play sound of this object;
				PlaySound(objectHitZones.collider.transform.GetComponent<AudioClipHolder>().audioClip);
			}
		}
	}

	public AudioSource audioSource = null;

	public void PlaySound(AudioClip soundToPlay)
	{
		if (audioSource == null)
			audioSource = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
  
		if (audioSource.isPlaying)
			audioSource.Stop();

		audioSource.clip = soundToPlay;
		audioSource.Play();
	}

	//To make sure sounds can be played after hitting the same object again.
	public void PlaySoundAgain(AudioClip soundToPlay)
	{
		if (audioSource == null)
			audioSource = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();

		if (audioSource.isPlaying)
			return;

		audioSource.clip = soundToPlay;
		audioSource.Play();
	}
}
