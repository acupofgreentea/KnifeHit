using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static Action LevelProperties;

    public static Action<int> UIOnNextLevel;

    public static Action LogMotorOnNextLevel;
    
    private ControlKnives controlKnives;

    private void Awake() 
    {
        controlKnives = GetComponent<ControlKnives>();
    }

    private void Start() 
    {
        UIOnNextLevel(controlKnives.KnifeCount);
    }

    public void ContinueWithRewardAd()
    {
        controlKnives.ResetRemainingKnifeCount();

        UIManager.Instance.HideGameOverPanel();
        UIManager.Instance.DestroyAllKnives();
        GameManager.Instance.ResumeGame();
        
        LoadNextLevel();
    }
    
    public void LoadNextLevel()
    {
        LevelProperties();
        
        LogMotorOnNextLevel();
        
        controlKnives.SpawnKnife();

        UIOnNextLevel(controlKnives.KnifeCount);
    }
    
}