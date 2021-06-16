using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorTools : EditorWindow
{
    [MenuItem("Tools/Reset PlayerPref")]
    public static void ResetPlayerPref()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("*** Reset PlayerPref ***");
    }
}
