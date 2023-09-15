using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Model: MonoBehaviour
{
}

[System.Serializable]
public class CanvasData
{
    public CanvasObject Canvas;
}

[System.Serializable]
public class CanvasObject
{
    public string ObjectName;
    public Vector3 Position;
    public Vector2 Dimension;
    public List<ComponentData> Components;
    public List<CanvasObject> Children;
}

[System.Serializable]
public class ComponentData
{
    public string ComponentType;
   // public string ComponentType;
    public Color Color;
    public CustomText Text; 
}
[System.Serializable]
public class CustomText
{
    public string text;
    public float fontSize;
    public string alignment;
}