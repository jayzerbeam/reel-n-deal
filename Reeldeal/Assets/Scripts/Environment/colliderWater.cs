using UnityEngine;

public class colliderWater : MonoBehaviour
{ 

    private void Start()
    {
        AddTriggersRecursively(transform);
    }

    private void AddTriggersRecursively(Transform parent)
    {
        foreach (Transform child in parent)
        {
            BoxCollider collider = child.gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;

            AddTriggersRecursively(child);
        }
    }
}