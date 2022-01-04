using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] LogMotor logMotor;
    
    [SerializeField] GameObject knife;

    [SerializeField] int knifeCount;

    [SerializeField] int remainingKnifeCount;
    
    public GameObject[] knivesOnLog;

    public int level = 1;
    
    private int logSpeed = 2;
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
        if(level % 4 == 3)
        {
            SpawnBoss();
        }
        else
        {
            StartCoroutine(NextLevelSequence(0.3f));
        }

    }
    void NextLevel()
    {
        level++;
        knifeCount++;

        remainingKnifeCount = knifeCount;

        UIManager.Instance.ShowKnivesPanel(knifeCount);
        
        logMotor.NextLevelSpeed(logSpeed);

        SpawnKnife();
        
        /*if(level > 1)
        {
            SpawnKnivesOnLog();
        }
        */ 
        // Spawn Knives on Log with another algorithm
    }

    void SpawnBoss()
    {
        //spawn different log
    }

    IEnumerator NextLevelSequence(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //play sound
        NextLevel();
    }
}
