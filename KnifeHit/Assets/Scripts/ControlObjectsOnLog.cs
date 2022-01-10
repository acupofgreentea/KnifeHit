using UnityEngine;
public class ControlObjectsOnLog : MonoBehaviour, IControlObjects
{
    [SerializeField] private GameObject[] knivesOnLog;
    [SerializeField] private GameObject[] tomatos;
    
    private int level = 1;
    private void OnEnable() 
    {
        LevelManager.LevelProperties += ClearKnivesOnLog;
        LevelManager.LevelProperties += SetLevel;
    }
    private void OnDisable() 
    {
        LevelManager.LevelProperties -= ClearKnivesOnLog;
        LevelManager.LevelProperties -= SetLevel;
    }

    public void ClearKnivesOnLog()
    {
        foreach (var knife in knivesOnLog)
        {
            knife.SetActive(false);
        }
    }

    public void ClearTomatosOnLog()
    {
        foreach (var tom in tomatos)
        {
            tom.SetActive(false);
        }
    }

    public void SpawnKnivesOnLog()
    {
        knivesOnLog[GetRandomKnifeNumber()].SetActive(true);
    }

    public void SpawnTomatosOnLog()
    {
        foreach (var tom in tomatos)
        {
            tom.SetActive(true);
        }
    }
    public int GetRandomKnifeNumber()
    {
        return Random.Range(0, knivesOnLog.Length);
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

        SpawnTomatosOnLog();
    }
    
}

public interface IControlObjects
{
    void ClearTomatosOnLog();
    void ClearKnivesOnLog();
    void SpawnTomatosOnLog();
    void SpawnKnivesOnLog();   
    int GetRandomKnifeNumber();
}