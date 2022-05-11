using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    public GameObject equipClothesObject;

    GameObject tempClothesObject;
    int equipClothesIndex = -1;

    public void ChangeShirts()
    {
        if (equipClothesIndex == -1 || equipClothesIndex == CustomizationData.Instance.shirtsPrefabs.Length - 1)
        {
            equipClothesIndex = 0;
		}
        else
        {
            equipClothesIndex++;
		}

        if (equipClothesObject != null)
        {
            Destroy(equipClothesObject);
            equipClothesObject = null;
        }

        AddEquipment();
	}

    public void TakeOffShirts()
    {
        equipClothesIndex = -1;

        if (equipClothesObject != null)
        {
            Destroy(equipClothesObject);
            equipClothesObject = null;
        }
	}

    void AddEquipment()
    {
        tempClothesObject = Instantiate(CustomizationData.Instance.shirtsPrefabs[equipClothesIndex]);
        SkinnedMeshRenderer[] skinnedMeshRenderers = tempClothesObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        equipClothesObject = AddChild(tempClothesObject, transform);

        foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderers)
            ProcessBonedObject(renderer, equipClothesObject);

        // We don't need the old obj, we make it disappear.
        //pantsObject.SetActiveRecursively(false);
        Destroy(tempClothesObject);
    }

    GameObject AddChild(GameObject source, Transform parent)
    {
        // Create the SubObject
        GameObject target = new GameObject(source.name);
        target.transform.parent = parent;
        target.transform.localPosition = source.transform.localPosition;
        target.transform.localRotation = source.transform.localRotation;
        target.transform.localScale = source.transform.localScale;
        return target;
    }

    private void ProcessBonedObject(SkinnedMeshRenderer sourceRenderer, GameObject parent)
    {
        // Add the renderer
        SkinnedMeshRenderer targetRenderer = parent.AddComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;

        // Assemble Bone Structure
        Transform[] myBones = new Transform[sourceRenderer.bones.Length];

        // As clips are using bones by their names, we find them that way.
        for (int i = 0; i < sourceRenderer.bones.Length; i++)
            myBones[i] = FindChildByName(sourceRenderer.bones[i].name, transform);

        // Assemble Renderer
        targetRenderer.bones = myBones;
        targetRenderer.sharedMesh = sourceRenderer.sharedMesh;
        targetRenderer.materials = sourceRenderer.materials;
    }

    Transform FindChildByName(string name, Transform parent)
    {
        Transform[] allChildren = parent.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.name.Equals(name))
                return child;
        }

        return null;
    }
}