using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMultiTag : MonoBehaviour
{
    // Allows gameObjects to have multiple tags. This is used to give info on fish characteristics
    // https://discussions.unity.com/t/multiple-tags-for-one-gameobject/203921/4

    [HideInInspector]
    public List<string> tags = new List<string>();

    public bool HasTag(string tag)
    {
        return tags.Contains(tag);
    }

    public void AddTag(string tag)
    {
        if (!tags.Contains(tag))
        {
            tags.Add(tag);
        }
    }

    public void RemoveTag(string tag)
    {
        if (tags.Contains(tag))
        {
            tags.Remove(tag);
        }
    }
}
