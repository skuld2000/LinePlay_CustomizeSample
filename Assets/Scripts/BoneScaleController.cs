using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoneScaleController : MonoBehaviour
{
	[Header("Root")]
	[SerializeField] Transform root;

	[Header("Head")]
	[SerializeField] Transform head;

	[Header("Left Arm")]
	[SerializeField] Transform leftShoulder;
	[SerializeField] Transform leftShoulderAdjust;
	[SerializeField] Transform leftArm;
	[SerializeField] Transform leftArmAdjust;
	[SerializeField] Transform leftForeArmAdjust;
	[SerializeField] Transform leftHand;

	[Header("Right Arm")]
	[SerializeField] Transform rightShoulder;
	[SerializeField] Transform rightShoulderAdjust;
	[SerializeField] Transform rightArm;
	[SerializeField] Transform rightArmAdjust;
	[SerializeField] Transform rightForeArmAdjust;
	[SerializeField] Transform rightHand;

	[Header("LeftFoot")]
	[SerializeField] Transform leftFoot;
	[SerializeField] Transform leftToe;

	[Header("RightFoot")]
	[SerializeField] Transform rightFoot;
	[SerializeField] Transform rightToe;
	float origPos = 0.0f;

	Slider heightSlider;
	Slider headSizeSlider;
	Slider armWidthSlider;
	Slider foreArmWidthSlider;
	Slider handSizeSlider;
	Slider armSizeSlider;
	Slider upperMuscleSlider;

	Animator animator;


	private void Start()
	{
		animator = GetComponent<Animator>();

		heightSlider = GameObject.Find("HeightSlider").GetComponent<Slider>();
		heightSlider.onValueChanged.AddListener(delegate { ChangeHeight(); });
		headSizeSlider = GameObject.Find("HeadSizeSlider").GetComponent<Slider>();
		headSizeSlider.onValueChanged.AddListener(delegate { ChangeHeadSize(); });
		armWidthSlider = GameObject.Find("ArmWidthSlider").GetComponent<Slider>();
		armWidthSlider.onValueChanged.AddListener(delegate { ChangeArmWidth(); });
		foreArmWidthSlider = GameObject.Find("ForeArmWidthSlider").GetComponent<Slider>();
		foreArmWidthSlider.onValueChanged.AddListener(delegate { ChangeForeArmWidth(); });
		handSizeSlider = GameObject.Find("HandSizeSlider").GetComponent<Slider>();
		handSizeSlider.onValueChanged.AddListener(delegate { ChangeHandSize(); });
		armSizeSlider = GameObject.Find("ArmSizeSlider").GetComponent<Slider>();
		armSizeSlider.onValueChanged.AddListener(delegate { ChangeArmSize(); });
		upperMuscleSlider = GameObject.Find("UpperMuscleSlider").GetComponent<Slider>();
		upperMuscleSlider.onValueChanged.AddListener(delegate { ChangeUpperMuscle(); });

		origPos = root.transform.localPosition.y;
	}
	public void ChangeHeight()
	{
		root.localScale = Vector3.one * (heightSlider.value + 0.5f);
	}

	public void ChangeHeadSize()
	{
		head.localScale = Vector3.one * (headSizeSlider.value + 0.5f);
	}

	public void ChangeArmWidth()
	{
		float factor = armWidthSlider.value + 0.5f;
		leftArmAdjust.localScale = new Vector3(leftArmAdjust.localScale.x, factor, factor);
		rightArmAdjust.localScale = new Vector3(rightArmAdjust.localScale.x, factor, factor);
	}

	public void ChangeForeArmWidth()
	{
		float factor = foreArmWidthSlider.value + 0.5f;
		leftForeArmAdjust.localScale = new Vector3(leftForeArmAdjust.localScale.x, factor, factor);
		rightForeArmAdjust.localScale = new Vector3(rightForeArmAdjust.localScale.x, factor, factor);
	}

	public void ChangeHandSize()
	{
		leftHand.localScale = Vector3.one * (handSizeSlider.value + 0.5f);
		rightHand.localScale = Vector3.one * (handSizeSlider.value + 0.5f);
	}

	public void ChangeArmSize()
	{
		leftArm.localScale = Vector3.one * (armSizeSlider.value + 0.5f);
		rightArm.localScale = Vector3.one * (armSizeSlider.value + 0.5f);
	}

	public void ChangeUpperMuscle()
	{
		float factor = upperMuscleSlider.value + 0.5f;
		leftShoulderAdjust.localScale = new Vector3(leftShoulderAdjust.localScale.x, factor, factor);
		rightShoulderAdjust.localScale = new Vector3(rightShoulderAdjust.localScale.x, factor, factor);
	}

	private void LateUpdate()
	{
		leftShoulder.localPosition = new Vector3(leftShoulder.localPosition.x, leftShoulder.localPosition.y, leftShoulder.localPosition.z + ((upperMuscleSlider.value * 0.02f) - 0.01f));
		rightShoulder.localPosition = new Vector3(rightShoulder.localPosition.x, rightShoulder.localPosition.y, rightShoulder.localPosition.z + ((upperMuscleSlider.value * -0.02f) + 0.01f));

		UpdateHeel();
	}

	[SerializeField] public float heelRotate = 0;
	[SerializeField] public float heelHight = 0;

	void UpdateHeel()
	{
		UpdateHeel(leftFoot, leftToe, true);
		UpdateHeel(rightFoot, rightToe, false);

		if (heelHight != 0.0f)
		{
			var pos = root.transform.localPosition;
			pos.y = origPos + heelHight;
			root.transform.localPosition = pos;
		}
	}

	void UpdateHeel(Transform foot, Transform toe, bool isLeft)
	{
		if (foot != null)
		{
			Vector3 rotate = new Vector3(0.0f, 0.0f, 0.0f);

			var origValue = foot.eulerAngles;
			rotate.z = isLeft == true ? heelRotate : -heelRotate;
			origValue += rotate;
			foot.eulerAngles = origValue;
		}

		if (toe != null)
		{
			Vector3 rotate = new Vector3(180.0f, 180.0f, 180.0f);

			var origValue = toe.eulerAngles;
			rotate.z = 180.0f + heelRotate;
			origValue += rotate;
			toe.eulerAngles = origValue;
		}
	}

	public void ChangeHighHeel(float rotate, float hight)
	{
		heelRotate = rotate;
		heelHight = hight;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			animator.SetTrigger("isAni1");
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			animator.SetTrigger("isAni2");
		}
	}

	public void ResetSlieders()
	{
		heightSlider.value = 0.5f;
		headSizeSlider.value = 0.5f;
		armWidthSlider.value = 0.5f;
		foreArmWidthSlider.value = 0.5f;
		handSizeSlider.value = 0.5f;
		armSizeSlider.value = 0.5f;
		upperMuscleSlider.value = 0.5f;
	}
}
