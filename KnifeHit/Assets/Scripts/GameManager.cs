using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int knifeCount;

    [SerializeField] 
    GameObject knife;

    public UIManager uiManager;

    [SerializeField]
    LogMotor logMotor;

    private int level = 1;

    [SerializeField]
    int remainingKnifeCount;

    public int score = 0;

    public static GameManager Instance;

    void Awake()
    {   
        Instance = this;
    }
    
    void Start()
    {
        remainingKnifeCount = knifeCount;
        SpawnKnife();
        uiManager.ShowKnivesPanel(knifeCount);
    }

    void SpawnKnife()
    {
        Instantiate(knife, transform.position, Quaternion.identity);
        remainingKnifeCount--;
    }

    void NextLevel()
    {
        level++;
        knifeCount++;
        remainingKnifeCount = knifeCount;
        uiManager.ShowKnivesPanel(knifeCount);
        logMotor.NextLevelSpeed(level);
        SpawnKnife();
    }

    public void OnSuccessfullHit()
    {
        score += 10;

        if(remainingKnifeCount > 0)
        {
            SpawnKnife();
        }
        else
        {
            StartCoroutine(NextLevelSequence(0.3f));
        }
    }

    IEnumerator NextLevelSequence(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //play sound
        NextLevel();
    }

    void GameOver()
    {
        uiManager.GameOverPanel();
        Time.timeScale = 0;
    }

    public IEnumerator GameOverSequence(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameOver();
    }
}
