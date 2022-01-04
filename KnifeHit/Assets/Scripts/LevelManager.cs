using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] LogMotor logMotor;
    
    [SerializeField] GameObject knife;

    [SerializeField] int knifeCount;

    [SerializeField] int remainingKnifeCount;

    private int level;

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
    public void OnSuccessfullHit()
    {
        if(remainingKnifeCount > 0)
        {
            GameManager.Instance.UpdateScore(10);
            SpawnKnife();
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
        
        logMotor.NextLevelSpeed(level);

        SpawnKnife();
    }

    IEnumerator NextLevelSequence(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //play sound
        NextLevel();
    }
}
