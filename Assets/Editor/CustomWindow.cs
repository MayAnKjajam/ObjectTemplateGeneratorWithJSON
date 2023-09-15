using System;
using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomWindow : EditorWindow
{
    private string jsonFileName = "";
    private string jsonPathtoSavefile = "";
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
        jsonPathtoSavefile = EditorGUILayout.TextField("save file at path", jsonPathtoSavefile);
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

    public void SaveJSONData()
    {
        try
        {
            // Attempt to parse the JSON string
            JObject jsonObject = JObject.Parse(jsonContent);
            // Validate the JSON data (add your validation logic here)
            if (IsValidJsonData(jsonObject))
            {
                if (!String.IsNullOrEmpty(jsonPathtoSavefile))
                // Serialize the JSON object and write it to the file
                {
                    File.WriteAllText(jsonPathtoSavefile, jsonObject.ToString());

                    Debug.Log("JSON data successfully written to: " + jsonPathtoSavefile);
                }
                else
                {
                    Debug.LogError("Enter valid path to save file");
                }
            }
            else
            {
                Debug.LogError("JSON data is invalid. Please check your data.");
            }
        }
        catch (JsonReaderException jsonEx)
        {
            Debug.LogError("Invalid JSON syntax: " + jsonEx.Message);
        }
        catch (Exception ex)
        {
            Debug.LogError("An error occurred: " + ex.Message);
        }
    }

    private bool IsValidJsonData(JObject jsonObject)
    {
        // Add your custom JSON validation logic here
        // For demonstration purposes, we assume the JSON data is valid.
        return true;
    }
}

