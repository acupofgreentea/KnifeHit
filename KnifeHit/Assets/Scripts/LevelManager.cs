using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] knivesOnLog;
    [SerializeField] private GameObject[] tomatos;
    [SerializeField] private LogMotor logMotor;
    [SerializeField] private GameObject knife;
    

    [SerializeField] private int knifeCount;
    [SerializeField] private int remainingKnifeCount;
    [SerializeField] private int logSpeedMultiplier = 2;
    [SerializeField] private int level = 1;
    
    void Start()
    {
        remainingKnifeCount = knifeCount;
        SpawnKnife();
        UIManager.Instance.ShowKnivesPanel(knifeCount);
    }

    void SpawnKnife()
    {
        Instantiate(knife, transform.position, Quaternion.identity);
        remainingKnifeCount--;
    }
    
    private void SpawnKnivesOnLog()
    {
        int randomKnifeNumber = Random.Range(0, knivesOnLog.Length);

        knivesOnLog[randomKnifeNumber].SetActive(true);    
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
            LoadNextLevel();
        }
    }
    
    void LoadNextLevel()
    {   
        Invoke("NextLevel", 0.5f);
    }
    void NextLevel()
    {
        level++;
        knifeCount++;

        remainingKnifeCount = knifeCount;

        UIManager.Instance.ShowKnivesPanel(knifeCount);
        
        logMotor.NextLevelSpeed(logSpeedMultiplier);

        SpawnKnife();
        
        ClearKnivesOnLog();
        
        if(level % 4 == 3)
        {
            SpawnBoss();
        }
        else
        {
            ClearTomatosOnLog();
        }

        
        /*if(level > 1)
        {
            SpawnKnivesOnLog();
        }
        */ 
        // Spawn Knives on Log with another algorithm
    }

    void ClearKnivesOnLog()
    {
        foreach (var knife in knivesOnLog)
        {
            knife.SetActive(false);
        }
    }
    void ClearTomatosOnLog()
    {
        
        foreach (var tom in tomatos)
        {
            tom.SetActive(false);
        }
    }

    void SpawnBoss()
    {
        foreach (var tom in tomatos)
        {
            tom.SetActive(true);
        }
    }
}
