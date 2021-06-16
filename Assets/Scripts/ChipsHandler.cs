using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class ChipsHandler : MonoBehaviour
{
    public static ChipsHandler Instance;
    
    public ChipUIHandler[] chips;

    private ChipUIHandler _selectedChip;

    public bool IsHaveUnlockedChip
    {
        get
        {
            return chips.FirstOrDefault((c) => c.CurrentState == ChipUIHandler.ChipState.Unlock) != null;
        } 
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        //_selectedChip = chips?.First((c) => c.CurrentState == ChipUIHandler.ChipState.Selected);
    }

    public void SetNewSelectedShip(ChipUIHandler chip)
    {
        if (_selectedChip == true)
        {
            _selectedChip.ChangeState(ChipUIHandler.ChipState.Default);
        }

        _selectedChip = chip;
    }
}
