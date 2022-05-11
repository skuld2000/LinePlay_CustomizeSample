using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
	[SerializeField] Camera frontCamera;
	[SerializeField] Camera sideCamera;
	[SerializeField] Text cameraButtonText;
	[SerializeField] Text modeButtonText;
	[SerializeField] GameObject bodyPanel;
	[SerializeField] GameObject EquipPanel;
	[SerializeField] GameObject FacePanel;

	[SerializeField] GameObject fullbody;
	[SerializeField] GameObject portrait;

	bool isBodyMode = true;
	bool isFrontCameraMode = true;

	private void Start()
	{
		isBodyMode = false;

		ChangeMode();
	}

	public void ChangeMode()
	{
		isFrontCameraMode = false;
		ChangeCameraMode();

		if (isBodyMode == true)
		{
			isBodyMode = false;

			modeButtonText.text = "Body Customization";
			fullbody.GetComponent<BoneScaleController>().ResetSlieders();

			bodyPanel.SetActive(false);
			EquipPanel.SetActive(false);
			FacePanel.SetActive(true);

			fullbody.SetActive(false);
			portrait.SetActive(true);
		}
		else
		{
			isBodyMode = true;

			modeButtonText.text = "Face Customization";
			portrait.GetComponent<FaceController>().ResetSliders();

			bodyPanel.SetActive(true);
			EquipPanel.SetActive(true);
			FacePanel.SetActive(false);

			fullbody.SetActive(true);
			portrait.SetActive(false);
		}
	}
	
	public void ChangeCameraMode()
	{
		if (isFrontCameraMode == true)
		{
			isFrontCameraMode = false;
			frontCamera.depth = -1f;
			sideCamera.depth = 0f;

			cameraButtonText.text = "FRONT VIEW";
		}
		else
		{
			isFrontCameraMode = true;
			frontCamera.depth = 0f;
			sideCamera.depth = -1f;

			cameraButtonText.text = "SIDE VIEW";
		}
	}
}
