using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomWindow : EditorWindow
{
    private string jsonFileName = "";
    private string jsonContent = "";
    private Vector2 scrollPosition;
    GameObject emptyObject=null;

    [MenuItem("Window/UI Genrator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CustomWindow), false, "UI Genrator");
    }
    
    private void OnGUI()
    {
        jsonFileName = EditorGUILayout.TextField("JSON File Name", jsonFileName);
        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Load JSON"))
        {
            LoadJSONData(jsonFileName);
        }
        EditorGUILayout.EndHorizontal();

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));
        jsonContent = EditorGUILayout.TextArea(jsonContent, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndScrollView();

        // Buttons to Save and Generate
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            SaveJSONData();
        }
        if (GUILayout.Button("Generate"))
        {
            GenerateHierarchy();
        }
        EditorGUILayout.EndHorizontal();
    }


    private void LoadJSONData(string fileName)
    {
        string content = "";
        if (fileName != null)
        {
            TextAsset guiElementsJson = (TextAsset)Resources.Load(fileName);
            content = guiElementsJson.text;
            jsonContent = content;
        }
    }
    private void GenerateHierarchy()
    {
        GUIElementLoader loaderScript = null;
        if (string.IsNullOrEmpty(jsonContent))
        {
            Debug.LogWarning("No JSON content to generate hierarchy from.");
            return;
        }
        if (emptyObject == null)
        {
            emptyObject = new GameObject("GUIGenrator");
            loaderScript = emptyObject.AddComponent<GUIElementLoader>();
        }
        else {
            loaderScript = emptyObject.GetComponent<GUIElementLoader>();
        }
        loaderScript.GenrateCanvas(jsonContent);
    }

    private void SaveJSONData()
    {
      
    }

}

