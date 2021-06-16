using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingChip : MonoBehaviour
{
    public Image image;
    
    private string KEY;
    
    private void Awake()
    {
        KEY = gameObject.name;
    }

    private void OnEnable()
    {
        string imageName = PlayerPrefs.GetString(KEY);

        if (imageName != String.Empty)
        {
            image.sprite = Resources.Load<Sprite>(imageName);
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetString(KEY, image.sprite.name);
    }
}
