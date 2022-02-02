using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text timeText;
    
    private float _levelTime;

    private void Update()
    {
        if (timeText == null)
            return;
        
        UpdateLevelTime();
    }

    private void UpdateLevelTime()
    {
        _levelTime += Time.deltaTime;
        timeText.text = _levelTime.ToString("F2");
    }
}
