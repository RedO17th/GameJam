using UnityEngine;

public class Game : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnGameOver.AddListener(GameOver);
    }

    void Start()
    {
        StartGame();
    }
 
    private void StartGame()
    {
        Score.SetDefaultScore();

        Time.timeScale = 1;
        GameParameters.GameRunning = true;
    }

    private void GameOver()
    {
        if (Score.Hidhscore > PlayerPrefs.GetInt("highscore"))
            PlayerPrefs.SetInt("highscore", Score.Hidhscore);

        PauseManager.Pause();
    }

}
