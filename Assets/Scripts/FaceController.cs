using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceController : MonoBehaviour
{
    [SerializeField] Transform leftEye;
    [SerializeField] Transform rightEye;
    [SerializeField] Transform nose;

    Slider rotateEyeSlider;
    Slider eyeSizeSlider;
    Slider noseSizeSlider;
    Slider noseWidthSlider;
    Slider nosePositionSlider;

    float eyeRot = 0f;
    Vector3 nosePos = Vector3.one;

    private void Start()
    {
        nosePos = nose.localPosition;

        rotateEyeSlider = GameObject.Find("RotateEyeSlider").GetComponent<Slider>();
        rotateEyeSlider.onValueChanged.AddListener(delegate { RotateEye(); });
        eyeSizeSlider = GameObject.Find("EyeSizeSlider").GetComponent<Slider>();
        eyeSizeSlider.onValueChanged.AddListener(delegate { EyeSize(); });
        noseSizeSlider = GameObject.Find("NoseSizeSlider").GetComponent<Slider>();
        noseSizeSlider.onValueChanged.AddListener(delegate { NoseSize(); });
        noseWidthSlider = GameObject.Find("NoseWidthSlider").GetComponent<Slider>();
        noseWidthSlider.onValueChanged.AddListener(delegate { NoseWidth(); });
        nosePositionSlider = GameObject.Find("NosePositionSlider").GetComponent<Slider>();
        nosePositionSlider.onValueChanged.AddListener(delegate { NosePosition(); });
    }

    public void RotateEye()
    {
        eyeRot = (rotateEyeSlider.value * 30) - 15f;
	}

    public void EyeSize()
    {
        leftEye.localScale = new Vector3(1f + (eyeSizeSlider.value * 0.3f) - 0.15f, 1f + (eyeSizeSlider.value * 0.4f) - 0.2f, leftEye.localScale.z);
        rightEye.localScale = new Vector3(1f + (eyeSizeSlider.value * 0.3f) - 0.15f, 1f + (eyeSizeSlider.value * 0.4f) - 0.2f, rightEye.localScale.z);
    }

    public void NoseSize()
    {
        nose.localScale = new Vector3(nose.localScale.x, nose.localScale.y, 1f + (noseSizeSlider.value * 1f) - 0.5f);
	}

    public void NoseWidth()
    {
        nose.localScale = new Vector3(1f + (noseWidthSlider.value * 0.8f) - 0.4f, nose.localScale.y, nose.localScale.z);
	}

    public void NosePosition()
    {
        nosePos = new Vector3(nosePos.x, nosePositionSlider.value * 0.01f - 0.005f, nosePos.z);
	}

	private void LateUpdate()
	{
        leftEye.localRotation = Quaternion.Euler(leftEye.localRotation.x, leftEye.localRotation.y, eyeRot);
        rightEye.localRotation = Quaternion.Euler(rightEye.localRotation.x, rightEye.localRotation.y, eyeRot);

        nose.localPosition = new Vector3(nosePos.x, nosePos.y, nosePos.z);
    }

    public void ResetSliders()
    {
        rotateEyeSlider.value = 0.5f;
        eyeSizeSlider.value = 0.5f;
        noseSizeSlider.value = 0.5f;
        noseWidthSlider.value = 0.5f;
        nosePositionSlider.value = 0.5f;
    }
}
