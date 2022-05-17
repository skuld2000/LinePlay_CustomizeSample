using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAngle : MonoBehaviour
{
	[SerializeField] Transform foot;
	[SerializeField] Transform toe;
	[SerializeField] Transform heelFoot;
	[SerializeField] Transform heelToe;

	[SerializeField] Vector3 heelDiff = Vector3.zero;
	[SerializeField] Vector3 origDiff = Vector3.zero;
	[SerializeField] float heelRotate = 0.0f;
	[SerializeField] float origRotate = 0.0f;

	[SerializeField] UnityEngine.UI.Text angleText;

	[SerializeField] BoneScaleController controller;

	// Start is called before the first frame update 한글.
	void Start()
	{
		//신발의 경사 벡터.
		heelDiff = heelToe.position - heelFoot.position;
		heelDiff.x = 0.0f;
		heelDiff.Normalize();

		//원래 발의 경사벡터.
		origDiff = toe.position - foot.position;
		origDiff.x = 0.0f;
		origDiff.Normalize();

		origRotate = GetAngle(foot.position, toe.position);
	}

	// Update is called once per frame
	void Update()
	{
		UpdateLeftFoot();
	}

	void UpdateLeftFoot()
	{
		//신발의 경사 벡터.
		heelDiff = heelToe.position - heelFoot.position;
		heelDiff.x = 0.0f;
		heelDiff.Normalize();

		//신발의 경사 벡터와 원래 발의 경사 벡터 사이 각도를 계산.
		var angle = GetAngle(heelFoot.position, heelToe.position);
		heelRotate = angle - origRotate;

		angleText.text = $"Angle : {heelRotate}";

		//controller.heel
	}

	public float GetAngle(Vector3 from, Vector3 to)
	{
		Vector3 v = to - from;
		return Mathf.Atan2(v.y, v.z) * Mathf.Rad2Deg;
	}
}

