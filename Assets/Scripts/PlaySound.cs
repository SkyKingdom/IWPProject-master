using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlaySound : MonoBehaviour
{
	public AudioClip soundToPlay = null;
	public AudioSource audioSource = null;

    public bool hapticFeedback = false;
    public float hapticFrequency = 0f;
    public float hapticAmplitude = 0f;

	public XRController xr;

    //TODO: add conditions to when sounds should play?

    private void OnTriggerEnter(Collider other)
	{
		if (audioSource.isPlaying)
			audioSource.Stop();

		audioSource.clip = soundToPlay;
		audioSource.Play();

        if (hapticFeedback)
        {
			Debug.Log("colliding with; " + other.name);
			xr.SendHapticImpulse(hapticAmplitude, hapticFrequency);
        }
	}
}
