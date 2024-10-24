using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleStepSub : MonoBehaviour
{
	public ClickOnRay clickRay = null;

    // Start is called before the first frame update
    void Start()
    {
		if (clickRay == null)
			clickRay = GameObject.FindGameObjectsWithTag("Hands")[1].GetComponent<ClickOnRay>();
    }

	private void OnTriggerEnter(Collider other)
	{
		clickRay.TriggerObject(tag);
	}
}
