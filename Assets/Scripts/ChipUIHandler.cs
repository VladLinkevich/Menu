using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipUIHandler : MonoBehaviour
{
    public ShipStateImage[] stateImage;

    public ChipState defaultState;

    private  string KEY;
    private bool _currentState;

    private void OnEnable()
    {
        KEY = gameObject.name;
        
        int save = PlayerPrefs.GetInt(KEY);

        if (save == 0)
        {
            //_currentState = defaultState;
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

    }
    
    public enum ChipState
    {
        None, 
        Default, 
        Selected,
        Block,
        Unlock,
    }
    
    [Serializable]
    public struct ShipStateImage
    {
        public ChipState state;
        public Image image;
    }
}
