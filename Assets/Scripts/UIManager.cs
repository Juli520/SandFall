using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text timeText;
    
    private float _timeToStart;

    private void Start()
    {
        _timeToStart = LevelManager.Instance.timeToStart;
    }

    private void Update()
    {
        if (timeText == null)
            return;
        
        if (_timeToStart > 0)
            UpdateLevelTime();
        else if (_timeToStart <= 0 && timeText.gameObject.activeSelf)
            timeText.gameObject.SetActive(false);
    }

    private void UpdateLevelTime()
    {
        _timeToStart -= Time.deltaTime;
        timeText.text = _timeToStart <= 1 ? "GO!" : _timeToStart.ToString("F0");
    }
}
