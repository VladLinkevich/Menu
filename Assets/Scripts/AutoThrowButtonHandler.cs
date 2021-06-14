using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AutoThrowButtonHandler : MonoBehaviour
{
    public Image[] positiveImages;
    public Image[] negativeImages;

    public bool defaultState;

    private const string KEY = "AutoThrow";
    private bool _currentState;

    private void OnEnable()
    {
        int save = PlayerPrefs.GetInt(KEY);

        if (save == 0)
        {
            _currentState = defaultState;
        }
        else
        {
            _currentState = save == 1;
        }
        
        ChangeState(_currentState);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(KEY, _currentState ? 1 : 2);
    }
    
    public void OnClickAutoThrowButton()
    {
        ChangeState(!_currentState);
    }

    private void ChangeState(bool state)
    {
        _currentState = state;

        Image[] turnOnImages = _currentState == true ? positiveImages : negativeImages;
        Image[] turnOffImages = _currentState == false ? positiveImages : negativeImages;

        foreach (var image in turnOnImages)
        {
            image.gameObject.SetActive(true);
        }
        
        foreach (var image in turnOffImages)
        {
            image.gameObject.SetActive(false);
        }
    }
}
