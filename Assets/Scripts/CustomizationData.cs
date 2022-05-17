using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomizationData", menuName = "Customization/Resource Data", order = int.MaxValue)]
public class CustomizationData : ScriptableObject
{
	public const string SETTINGS_ASSET_PATH = "CustomizationData";

	private static CustomizationData s_instance = null;
	public static CustomizationData Instance
	{
		get
		{
			if (s_instance == null)
			{
				s_instance = Resources.Load<CustomizationData>(SETTINGS_ASSET_PATH);
				if (s_instance == null)
				{
					Debug.LogError("Missing CustomizationData. Create CustomizationData Under Resources Folder.");
				}
				else
				{

				}
			}

			return s_instance;
		}
	}

	[Header("ChangeCloth")]
	public GameObject[] shirtsPrefabs;

	[Header("ChangeShoes")]
	public ShoesInfo[] shoesInfos;
}
