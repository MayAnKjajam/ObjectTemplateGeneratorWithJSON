using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GUIElementLoader : MonoBehaviour
{
    public void GenrateCanvas(string jsonContent)
    {
        if (jsonContent != null)
        {
            // Load JSON data
            CanvasData canvasData = JsonUtility.FromJson<CanvasData>(jsonContent);         


            CreateNewObject(canvasData.Canvas,null);

        }
    }
  
    public void CreateNewObject(CanvasObject obj,GameObject parent)
    {
        GameObject newObject = new GameObject(obj.ObjectName);
        if (parent != null)
        { 
            newObject.transform.SetParent(parent.transform);
        }
        newObject.AddComponent<RectTransform>();
        RectTransform rect = newObject.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(obj.Dimension.x  , obj.Dimension.y);
        
        rect.anchoredPosition = new Vector2(obj.Position.x,obj.Position.y);
        AttachComponents(newObject, obj.Components);

        if (obj.Children.Count > 0)
        {
            foreach (CanvasObject c in obj.Children)
            CreateNewObject(c,newObject);
        }
    }

    public void AttachComponents(GameObject obj, List<ComponentData> Components)
    {
        foreach (ComponentData componentData in Components)
        {
            if (componentData.ComponentType == "UnityEngine.Canvas")
            {
                Canvas canvas = obj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }
            else if (componentData.ComponentType == "UnityEngine.CanvasScaler")
            {
                CanvasScaler scaler = obj.AddComponent<CanvasScaler>();
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1920, 1080);
            }
            else if (componentData.ComponentType == "UnityEngine.GraphicRayCaster")
            {
                GraphicRaycaster scaler = obj.AddComponent<GraphicRaycaster>();
            }
            else if (componentData.ComponentType == "UnityEngine.Image")
            {
                Image image = obj.AddComponent<Image>();
                image.color = componentData.Color;
            }
            else if (componentData.ComponentType == "UnityEngine.Text")
            {
                TextMeshProUGUI text = obj.AddComponent<TextMeshProUGUI>();
                text.color = componentData.Color;
                text.text = componentData.Text.text;
                text.fontSize = componentData.Text.fontSize;
                string align = componentData.Text.alignment.ToLower();
                switch (align)
                {
                    case "left":
                        text.alignment = TextAlignmentOptions.Left;
                        break;
                    case "center":
                        text.alignment = TextAlignmentOptions.Center;
                        break;
                    case "right":
                        text.alignment = TextAlignmentOptions.Right;
                        break;
                    // Add more cases for other alignment options as needed
                    default:
                        Debug.LogError("Invalid alignment string: " + align);
                        break;
                }
            }
            else if (componentData.ComponentType == "UnityEngine.Button")
            { 
                Button button = obj.AddComponent<Button>();
            }

        }
    }

}

