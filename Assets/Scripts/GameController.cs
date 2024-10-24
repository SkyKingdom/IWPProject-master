using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	#region Start Scene
	[Header ("Start Scene")]
	public bool enableTextToSpeech = false;
	public AudioClip audioExplanationStart = null;

	public void ToggleTTS(bool enable)
	{
		enableTextToSpeech = enable;
		SwapScenes();
		ChangeGameStage(1);
	}
	#endregion

	#region Step 1 - Put on Lab Coat and Goggles
	[Header("Step 1 - Put on Lab Coat and Goggles")]
	public bool coatOn = false;
	public AudioClip audioExplanationStep1 = null;

	public void ToggleCoat(bool enable)
	{
		coatOn = enable;
		ChangeGameStage(2);
	}
	#endregion

	#region Step 2 - Fetch materials
	[Header("Step 2 - Fetch materials")]
	public bool grabbedBeaker = false;
	public bool filledBeaker = false;
	public bool placedOnStand = false;
	public AudioClip audioExplanationStep2 = null;

	public void ToggleBeakerFetched(bool enable)
	{
		grabbedBeaker = enable;
	}

	public void ToggleBeakerFilled (bool enable)
	{
		filledBeaker = enable;
	}

	public void ToggleBeakerPlaced(bool enable)
	{
		placedOnStand = enable;
		ChangeGameStage(3);
	}
	#endregion

	#region Step 3 - Set up Bunsen Burner
	[Header("Step 3 - Set up Bunsen Burner")]
	public bool checkedRings = false;
	public bool connectedTube = false;
	public bool openedTube = false;
	public bool litFlame = false;
	public AudioClip audioExplanationStep3 = null;
	public AudioClip audioGasValveOpen = null;
	public AudioClip audioGasValveClosed = null;

	public void ToggleCheckedRings(bool enable)
	{
		checkedRings = enable;
	}

	public void ToggleConnectedTube(bool enable)
	{
		connectedTube = enable;
	}

	public void ToggleOpenedTube(bool enable)
	{
		openedTube = enable;
		if (enable)
			PlaySound(audioGasValveOpen);
		else
			PlaySound(audioGasValveClosed);
	}

	public void ToggleLitFlame(bool enable)
	{
		litFlame = enable;
		ChangeGameStage(4);
	}
	#endregion

	#region Step 4 - Stand over Flame
	[Header("Step 4 - Stand over Flame")]
	public bool standOverFlame = false;
	public AudioClip audioExplanationStep4 = null;

	public void ToggleStandOverFlame(bool enable)
	{
		standOverFlame = enable;
		ChangeGameStage(5);
	}
	#endregion

	#region Step 5 - Set Oxygen
	[Header("Step 5 - Set Oxygen")]
	public float oxygenValue = 0.0f;
	public bool oxygenSet = false;
	public AudioClip audioExplanationStep5 = null;


	#endregion

	#region Step 6 - Observe Experiment
	[Header("Step 6 - Observe Experiment")]
	public float correctInteractionMark = 0.0f;
	public AudioClip audioExplanationStep6 = null;
	#endregion

	#region Game Setup
	[Header("General Game Setup")]
	// Start is called before the first frame update
	public int gameStage = 0;

	public static GameController gameCont = null;
	public AudioSource audioSource = null;
	public AudioSource globalSource = null;
	public AudioClip progressSound = null;
	public AudioClip errorSound = null;

	public Transform holdingObject = null;

	void Start()
	{
		DontDestroyOnLoad(this);
		if(gameCont == null)
			gameCont = this;

		if (audioSource == null)
			audioSource = Camera.main.GetComponent<AudioSource>();

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public void PlaySound(AudioClip soundToPlay)
	{
		if (audioSource.isPlaying)
			audioSource.Stop();

		audioSource.clip = soundToPlay;
		audioSource.Play();
	}

	public void PlayProgressSound()
	{
		if (globalSource == null)
			globalSource = GameObject.FindWithTag("Player").GetComponent<AudioSource>();

		if (globalSource != null)
		{
			globalSource.clip = progressSound;
			globalSource.Play();
		}
	}

	//Reset all necessary parts and swap over to the Experiment scene.
	public void SwapScenes()
	{
		audioSource = null;
		SceneManager.LoadScene(1);
	}

	public void ChangeGameStage(int stage)
	{
		gameStage = stage;
		PlayProgressSound();
		//Game-Stage dependant coding.
		switch (gameStage)
		{
			case 0:
				PlaySound(audioExplanationStart);
				break;
			case 1:
				PlaySound(audioExplanationStep1);
				break;
			case 2:
				PlaySound(audioExplanationStep2);
				break;
			case 3:
				PlaySound(audioExplanationStep3);
				break;
			case 4:
				PlaySound(audioExplanationStep4);
				break;
			case 5:
				PlaySound(audioExplanationStep5);
				break;
			case 6:
				PlaySound(audioExplanationStep6);
				break;
		}
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		audioSource = Camera.main.GetComponent<AudioSource>();
		globalSource = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
	#endregion
}
