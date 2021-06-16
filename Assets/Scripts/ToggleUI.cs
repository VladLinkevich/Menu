using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    public Animator animator;
    
    private string KEY;
    
    private bool _isActive;
    private static readonly int Active = Animator.StringToHash("Active");
    private static readonly int Deactive = Animator.StringToHash("Deactive");

    private void Awake()
    {
        KEY = gameObject.name;
    }

    private void OnEnable()
    {
        int state = PlayerPrefs.GetInt(KEY);

        if (state == 0 || state == 2)
        {
            _isActive = true;
        }
        else
        {
            _isActive = false;
        }

        TriggerAnim();
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(KEY, _isActive ? 2 : 1);
    }

    public void OnToggleButton()
    {
        _isActive = !_isActive;
        
        TriggerAnim();
    }
    
    public void OnResetButton()
    {
        if (_isActive == false)
        {
            _isActive = true;
            TriggerAnim();
        }
    }
    
    private void TriggerAnim()
    {
        animator.ResetTrigger(Deactive);
        
        if (_isActive)
        {
            animator.SetTrigger(Active);
        }
        else
        {
            animator.SetTrigger(Deactive);
        }
    }
}
