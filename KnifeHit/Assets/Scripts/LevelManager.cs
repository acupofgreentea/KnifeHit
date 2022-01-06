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
    private int remainingKnifeCount;

    public static Action LogOnNextLevel;
    public static Action<int> UIOnNextLevel;
    
    private void Start()
    {
        remainingKnifeCount = knifeCount;
        SpawnKnife();
        UIManager.Instance.ShowKnivesPanel(knifeCount);
    }

    private void SpawnKnife()
    {
        Instantiate(knife, transform.position, Quaternion.identity);
        remainingKnifeCount--;
    }
    
    private int GetRandomKnifeNumber()
    {
        return Random.Range(0, knivesOnLog.Length);
    }
    
    private void SpawnKnivesOnLog()
    {
        knivesOnLog[GetRandomKnifeNumber()].SetActive(true);    
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
            Invoke("LoadNextLevel", 0.5f);
        }
    }

    private void SetKnivesForNextLevel()
    {
        knifeCount++;
        remainingKnifeCount = knifeCount;
    }

    private void SetLevel()
    {
        level++;

        // Spawn Knives on Log with another algorithm
        
        if(level % 4 == 3)
        {
            LoadBossLevel();
        }
        else
        {
            ClearTomatosOnLog();
        }
    }
    private void LoadNextLevel()
    {
        SetKnivesForNextLevel();

        SetLevel();
        
        LogOnNextLevel();

        UIOnNextLevel(knifeCount);

        SpawnKnife();
        
        ClearKnivesOnLog();
    }
    private void LoadBossLevel()
    {
        //change log sprite

        foreach (var tom in tomatos)
        {
            tom.SetActive(true);
        }
    }

    private void ClearKnivesOnLog()
    {
        foreach (var knife in knivesOnLog)
        {
            knife.SetActive(false);
        }
    }
    private void ClearTomatosOnLog()
    {
        foreach (var tom in tomatos)
        {
            tom.SetActive(false);
        }
    }
}
