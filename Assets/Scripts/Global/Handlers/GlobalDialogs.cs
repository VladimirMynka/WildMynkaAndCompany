using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDialogs : MonoBehaviour {
    public Topic[] topics;
}

[System.Serializable]
public class Topic{
    public string name;
    [TextAreaAttribute(5, 20)] public string[] texts;
    public AudioClip[] replicas;
    public int usingCount;
}