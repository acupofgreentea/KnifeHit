using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int Score {get; set;}
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Score = 0;
    }
    public void UpdateScore(int scoreToAdd)
    {
        Score += scoreToAdd;
    }
    
    private void GameOver()
    {
        UIManager.Instance.ShowGameOverPanel();
        PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
