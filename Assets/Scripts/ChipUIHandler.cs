using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChipUIHandler : MonoBehaviour
{
    public ShipStateImage[] stateImage;
    public Image chipImage;
    public TMP_Text unlockTimeText;
    
    public ChipState defaultState;
    public float unlockTime;
    
    private  string KEY;

    private const string DAY_KEY = "DAY_KEY";
    private const string HOUR_KEY = "HOUR_KEY";
    private const string MINUTE_KEY = "MINUTE_KEY";
    private const string SECOND_KEY = "SECOND_KEY";
    
    private ChipState _currentState;
    private DateTime _currentTime;
    private DateTime _endTime;

    public ChipState CurrentState => _currentState;

    private void OnEnable()
    {
        KEY = gameObject.name;
        
        int save = PlayerPrefs.GetInt(KEY);

        save = save == 0 ? (int) defaultState : save;
        
        ChangeState((ChipState)save);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(KEY, (int)_currentState);

        if (_currentState == ChipState.Unlock)
        {
            PlayerPrefs.SetInt(DAY_KEY, _endTime.Day);
            PlayerPrefs.SetInt(HOUR_KEY, _endTime.Hour);
            PlayerPrefs.SetInt(MINUTE_KEY, _endTime.Minute);
            PlayerPrefs.SetInt(SECOND_KEY, _endTime.Second);
        }
        
        ChangeState(ChipState.None);
    }
    
    public void OnClickChipButton()
    {
        if (_currentState == ChipState.Block)
        {
            ChangeState(ChipState.Unlock);
        } 
        else if (_currentState == ChipState.Default)
        {
            ChangeState(ChipState.Selected);
        }
    }

    public void ChangeState(ChipState state)
    {
        if (state == ChipState.Unlock &&
            ChipsHandler.Instance == true && 
            ChipsHandler.Instance.IsHaveUnlockedChip == true)
        {
            return;
        }
        
        Image[] images = FindImage();

        for (int i = 0; i < images?.Length; ++i)
        {
            images[i].gameObject.SetActive(false);
        }

        _currentState = state;
        
        if (_currentState == ChipState.Unlock)
        {
            CalculateTime();
        }
        
        if (_currentState == ChipState.Selected)
        {
            if (ChipsHandler.Instance == true)
                ChipsHandler.Instance.SetNewSelectedShip(this);
            else
                ChangeState(ChipState.Default);
        }
        
        images = FindImage();

        for (int i = 0; i < images?.Length; ++i)
        {
            images[i].gameObject.SetActive(true);
        }
    }

    private void CalculateTime()
    {
        _currentTime = System.DateTime.Now;

        int day = PlayerPrefs.GetInt(DAY_KEY);
        int hour = PlayerPrefs.GetInt(HOUR_KEY);
        int minute = PlayerPrefs.GetInt(MINUTE_KEY);
        int second = PlayerPrefs.GetInt(SECOND_KEY);

        if ((day == 0 && hour == 0 && minute == 0 && second == 0))
        {
            _endTime = _currentTime.AddSeconds(unlockTime);
        }
        else
        {
            float time = (_currentTime.Day - day) * 3600 * 24 -
                         (_currentTime.Hour - hour) * 3600 -
                         (_currentTime.Minute - minute) * 60 -
                         (_currentTime.Second - second);

            _endTime = _currentTime.AddSeconds(time);
        }
    }

    private Image[] FindImage()
    {
        foreach (var i in stateImage)
        {
            if (i.state == _currentState)
            {
                return i.images;
            }
        }

        return null;
    }

    private void Update()
    {
        if (_currentState == ChipState.Unlock)
        {
            if (_currentTime < _endTime)
            {
                _currentTime = DateTime.Now;
                DisplayTime(_endTime - _currentTime);
            }
            else
            {
                ChangeState(ChipState.Default);
                
                PlayerPrefs.SetInt(DAY_KEY, 0);
                PlayerPrefs.SetInt(HOUR_KEY, 0);
                PlayerPrefs.SetInt(MINUTE_KEY, 0);
                PlayerPrefs.SetInt(SECOND_KEY, 0);
            }
        }
    }

    private void DisplayTime(TimeSpan timeToDisplay)
    {
        unlockTimeText.text = $"{timeToDisplay.Minutes:00}:{timeToDisplay.Seconds:00}";
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
        public Image[] images;
    }
}
