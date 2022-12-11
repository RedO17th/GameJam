using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseManager
{
    public static bool _pauseStatus { get; private set; } = false;

    public static void Pause()
    {
        if (_pauseStatus)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }

        _pauseStatus = !_pauseStatus;
    }
}
