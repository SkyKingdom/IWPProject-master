using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ClickOnRay : MonoBehaviour
{
	public Transform hittedObject = null;
	private XRDirectInteractor interactor = null;
	public XRController controller = null;

	public bool shouldIPlaySound = false;

	public static ClickOnRay clickRay = null;

	bool gripped = false;
	bool pointed = false;

	private void Awake()
	{
		interactor = GetComponent<XRDirectInteractor>();
	}

	private void OnEnable()
	{
		Debug.Log("enabled");
		clickRay = this;
		interactor.onHoverEntered.AddListener(TriggerObjectSelect);
	}

	private void OnDisable()
	{
		interactor.onHoverEntered.RemoveListener(TriggerObjectSelect);
	}

	void FixedUpdate()
	{
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		Debug.DrawRay(transform.position, fwd * 50, Color.green);

		int layerMask = 1 << 7;

		//hittableObjects;
		RaycastHit objectHit;
		if (Physics.Raycast(transform.position, fwd, out objectHit, 50, layerMask))
		{
			if (hittedObject != objectHit.collider.transform)
			{
				Debug.Log(objectHit.collider.name);
				hittedObject = objectHit.collider.transform;
				if(shouldIPlaySound)
					PlaySound(objectHit.collider.transform.GetComponent<AudioClipHolder>().audioClip);
			}
		}

		if (controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerPressed))
		{
			if(triggerPressed > 0.4 && !pointed)
			{
				if(hittedObject != null)
				{
					Debug.Log(hittedObject.name + " : " + hittedObject.tag);
					TriggerObject(hittedObject.tag);
				}
				pointed = true;
			}
			else if(triggerPressed < 0.4)
			{
				pointed = false;
			}
		}

		if (controller.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripPressed))
		{
			if (gripPressed > 0.4 && !gripped)
			{
				if (GameController.gameCont.holdingObject != null)
				{
					Debug.Log("Gripping " + GameController.gameCont.holdingObject.name + " : " + GameController.gameCont.holdingObject.tag);
					if(GameController.gameCont.holdingObject.name == "ThinVial")
					{
						TriggerObject(GameController.gameCont.holdingObject.GetChild(0).tag);
					}
				}
				gripped = true;
			}
			else if (gripPressed < 0.4)
			{
				gripped = false;
			}
		}
	}

	public AudioSource audioSource = null;

	public void TriggerObject(string functionToTrigger)
	{
		if (functionToTrigger == "TTSYes")
		{
			GameController.gameCont.ToggleTTS(true);
		}
		else if (functionToTrigger == "TTSNo")
		{
			GameController.gameCont.ToggleTTS(false);
		}
		else if (functionToTrigger == "ToggleCoat")
		{
			if (GameController.gameCont.gameStage == 1)
				GameController.gameCont.ToggleCoat(true);
			else
				GameController.gameCont.PlaySound(GameController.gameCont.errorSound);
		}
		//Step 2
		else if (functionToTrigger == "2_fetchBeaker")
		{
			if (GameController.gameCont.gameStage == 2)
			{
				GameController.gameCont.ToggleBeakerFetched(true);
				GameController.gameCont.PlayProgressSound();
			}
			else
				GameController.gameCont.PlaySound(GameController.gameCont.errorSound);
		}
		else if (functionToTrigger == "2_fillBeaker")
		{
			if (GameController.gameCont.gameStage == 2 && GameController.gameCont.grabbedBeaker)
			{
				GameController.gameCont.ToggleBeakerFilled(true);
				GameController.gameCont.PlayProgressSound();
			}
			else
				GameController.gameCont.PlaySound(GameController.gameCont.errorSound);
		}
		else if (functionToTrigger == "2_placeBeaker")
		{
			if (GameController.gameCont.gameStage == 2 && GameController.gameCont.grabbedBeaker && GameController.gameCont.filledBeaker)
			{
				GameController.gameCont.ToggleBeakerPlaced(true);
			}
			else
				GameController.gameCont.PlaySound(GameController.gameCont.errorSound);
		}
		//Step 3
		else if (functionToTrigger == "3_checkedRings")
		{
			if (GameController.gameCont.gameStage == 3)
			{
				GameController.gameCont.ToggleCheckedRings(true);
				GameController.gameCont.PlayProgressSound();
			}
			else
				GameController.gameCont.PlaySound(GameController.gameCont.errorSound);
		}
		else if (functionToTrigger == "3_connectedTube")
		{
			if (GameController.gameCont.gameStage == 3 && GameController.gameCont.checkedRings)
			{
				GameController.gameCont.ToggleConnectedTube(true);
				GameController.gameCont.PlayProgressSound();
			}
			else
				GameController.gameCont.PlaySound(GameController.gameCont.errorSound);
		}
		else if (functionToTrigger == "3_openedTube")
		{
			if (GameController.gameCont.gameStage == 3 && GameController.gameCont.checkedRings && GameController.gameCont.connectedTube)
			{
				GameController.gameCont.ToggleOpenedTube(true);
				GameController.gameCont.PlayProgressSound();
			}
			else
				GameController.gameCont.PlaySound(GameController.gameCont.errorSound);
		}
		else if (functionToTrigger == "3_litFlame")
		{
			if (GameController.gameCont.gameStage == 3 && GameController.gameCont.checkedRings && GameController.gameCont.connectedTube && GameController.gameCont.openedTube)
			{
				GameController.gameCont.ToggleLitFlame(true);
			}
			else
				GameController.gameCont.PlaySound(GameController.gameCont.errorSound);
		}
		else if (functionToTrigger == "4_standOverFlame")
		{

			if (GameController.gameCont.gameStage == 4)
			{
				GameController.gameCont.ToggleStandOverFlame(true);
			}
			else
				GameController.gameCont.PlaySound(GameController.gameCont.errorSound);
		}
		else if (functionToTrigger == "5_bunsenLess")
		{

			if (GameController.gameCont.gameStage == 4)
			{
				GameController.gameCont.ToggleStandOverFlame(true);
			}
			else
				GameController.gameCont.PlaySound(GameController.gameCont.errorSound);
		}
		else if (functionToTrigger == "5_bunsenMore")
		{

			if (GameController.gameCont.gameStage == 4)
			{
				GameController.gameCont.ToggleStandOverFlame(true);
			}
			else
				GameController.gameCont.PlaySound(GameController.gameCont.errorSound);
		}
	}

	public void TriggerObjectSelect(XRBaseInteractable interactable)
	{
		Debug.Log("selecting; " + hittedObject.tag);
		if(hittedObject != null)
			TriggerObject(hittedObject.tag);
	}

	public void PlaySound(AudioClip soundToPlay)
	{
		if (audioSource == null)
			audioSource = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();

		if (audioSource.isPlaying)
			audioSource.Stop();

		audioSource.clip = soundToPlay;
		audioSource.Play();
	}
}
