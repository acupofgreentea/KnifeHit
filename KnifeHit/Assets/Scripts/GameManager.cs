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
        Time.timeScale = 0;
    }
}
