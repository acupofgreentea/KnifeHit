using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] knivesOnLog;
    [SerializeField] private GameObject[] tomatos;
    [SerializeField] private GameObject knife;
    [SerializeField] private LogMotor logMotor;
    [SerializeField] private int knifeCount;
    
    private int level = 1;
    [SerializeField] private int remainingKnifeCount;

    public static Action LogMotorOnNextLevel;
    public static Action<int> UIOnNextLevel;

    private delegate void NextLevelProps();

    private event NextLevelProps LeveLProps;

    private void OnEnable()
    {
        LeveLProps += SetKnivesForNextLevel;
        LeveLProps += ClearKnivesOnLog;
        LeveLProps += SetLevel;
    }
    private void OnDisable()
    {
        LeveLProps -= SetKnivesForNextLevel;
        LeveLProps -= ClearKnivesOnLog;
        LeveLProps -= SetLevel;
    }
    private void Awake()
    {
        remainingKnifeCount = knifeCount;
    }
    
    private void Start()
    {
        SpawnKnife();
        UIOnNextLevel(knifeCount);
    }

    private void SpawnKnife()
    {
        Instantiate(knife, transform.position, Quaternion.identity);
        remainingKnifeCount--;
    }

    private void ResetRemainingKnifeCount()
    {
        int tempKnifeCount = remainingKnifeCount;
        remainingKnifeCount -= tempKnifeCount;
    }

    public void ContinueWithRewardAd()
    {
        ResetRemainingKnifeCount();

        UIManager.Instance.HideGameOverPanel();
        UIManager.Instance.DestroyAllKnives();
        GameManager.Instance.ResumeGame();
        
        LoadNextLevel();
    }
    private void LoadNextLevel()
    {
        LeveLProps();
        
        LogMotorOnNextLevel();
        
        SpawnKnife();

        UIOnNextLevel(knifeCount);
    }
    public void OnSuccessfullHit()
    {
        if(remainingKnifeCount > 0)
        {
            GameManager.Instance.UpdateScore(10);
            SpawnKnife();
        }
        else
        {
            Invoke(nameof(LoadNextLevel), 0.5f);
        }
    }
    private void SetKnivesForNextLevel()
    {
        knifeCount++;
        remainingKnifeCount = knifeCount;
    }
    
    private int GetRandomKnifeNumber()
    {
        return Random.Range(0, knivesOnLog.Length);
    }
    private void SpawnKnivesOnLog()
    {
        knivesOnLog[GetRandomKnifeNumber()].SetActive(true);    
    }

    private void SetLevel()
    {
        level++;

        if(level > 1)
        {
            for (int i = 0; i < GetRandomKnifeNumber() + 1; i++)
            {
                SpawnKnivesOnLog();
            }
        }
        
        if(level % 4 == 3)
        {
            ClearKnivesOnLog();
            Invoke(nameof(LoadBossLevel), 0f);
        }
        else
        {
            UIManager.Instance.HideBossLevelText();
            ClearTomatosOnLog();
        }
    }
    private void LoadBossLevel()
    {
        //change log sprite
        UIManager.Instance.ShowBossLevelText();

        SpawnTomatos();
    }

    private void SpawnTomatos()
    {
        foreach (var tom in tomatos)
        {
            tom.SetActive(true);
        }
    }
    private void ClearTomatosOnLog()
    {
        foreach (var tom in tomatos)
        {
            tom.SetActive(false);
        }
    }
    private void ClearKnivesOnLog()
    {
        foreach (var knife in knivesOnLog)
        {
            knife.SetActive(false);
        }
    }
}
