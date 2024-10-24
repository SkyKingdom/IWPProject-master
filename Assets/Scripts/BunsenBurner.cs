using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class BunsenBurner : XRBaseInteractable
{
	enum FlameStates { Off, SafeFlame, BlueFlame, RoaringFlame};
	FlameStates flameState;
	int flameStateN;
	bool bunsenPressed = false;

	public AudioSource audioSource = null;

	public AudioClip[] burnerSounds = null;

	/*public UnityEvent OnPress = null;

	private float yMin = 0.0f;
	private float yMax = 0.0f;
	private bool previousPress = false;

	private XRBaseInteractor hoverInteractor = null;
	private float previousHandHeight = 0.0f;

	private void Awake()
	{
		base.Awake();
		onHoverEntered.AddListener(StartPress);
		onHoverExited.AddListener(EndPress);
	}

	private void StartPress(XRBaseInteractor interactor)
	{
		hoverInteractor = interactor;
		previousHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
	}

	private void EndPress(XRBaseInteractor interactor)
	{
		hoverInteractor = null;
		previousHandHeight = 0.0f;

		previousPress = false;
		SetYPosition(yMax);
	}

	private void OnDestroy()
	{
		onHoverEntered.RemoveListener(StartPress);
		onHoverExited.RemoveListener(EndPress);
	}

	private void Start()
	{
		SetMinMax();	
	}

	private void SetMinMax()
	{
		Collider collider = GetComponent<Collider>();
		yMin = transform.localPosition.y - (collider.bounds.size.y * 0.5f);
		yMax = transform.localPosition.y;
	}*/

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Hands")
			ChangeFlame();
	}

	public void ChangeFlame()
	{
		
		Debug.Log("Flame Changed!");

		if (flameStateN + 1 > 3)
		{
			flameStateN = 0;
		}
		else
		{
			flameStateN++;
		}

		flameState = (FlameStates)flameStateN;
		if (audioSource.isPlaying)
			audioSource.Stop();

		audioSource.clip = burnerSounds[flameStateN];
		audioSource.Play();

		Debug.Log("Current Flame:" + flameState);
		
	}

	/*public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
	{
		if (hoverInteractor)
		{
			float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
			float handDifference = previousHandHeight - newHandHeight;
			previousHandHeight = newHandHeight;

			float newPosition = transform.localPosition.y - handDifference;
			SetYPosition(newPosition);

			CheckPress();
		}

	}

	private float GetLocalYPosition(Vector3 position)
	{
		Vector3 localPosition = transform.root.InverseTransformPoint(position);
		return localPosition.y;
	}

	private void SetYPosition(float position)
	{
		Vector3 newPosition = transform.localPosition;
		newPosition.y = Mathf.Clamp(position, yMin, yMax);
		transform.localPosition = newPosition;
	}

	private void CheckPress()
	{
		bool inPosition = InPosition();

		if (inPosition && inPosition != previousPress)
			OnPress.Invoke();

		previousPress = inPosition;
	}

	private bool InPosition()
	{
		float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMax + 0.01f);

		return transform.localPosition.y == inRange;
	}*/
}
