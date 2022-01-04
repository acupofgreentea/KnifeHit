using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public int Score {get; set;}
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        Score = 0;
    }
    public void UpdateScore(int scoreToAdd)
    {
        Score += scoreToAdd;
    }
    
    void GameOver()
    {
        UIManager.Instance.GameOverPanel();
        Time.timeScale = 0;
    }

    public IEnumerator GameOverSequence(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameOver();
    }
}
