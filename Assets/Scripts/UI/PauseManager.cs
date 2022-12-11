using UnityEngine;

public static class PauseManager
{
    public static bool _pauseStatus { get; private set; } = false;

    public static void Pause()
    {
        if (_pauseStatus)
        {
            Time.timeScale = 1f;
            GameParameters.GameRunning = true;
        }
        else
        {
            Time.timeScale = 0f;
            GameParameters.GameRunning = false;
        }

        _pauseStatus = !_pauseStatus;
    }
}
