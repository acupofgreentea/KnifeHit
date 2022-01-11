using UnityEngine;

public class SetNextLevel : MonoBehaviour
{
    private KnivesOnLog knives;
    private TomatosOnLog tomatos;
    
    private int level = 1;

    private void Awake() 
    {
        knives = GetComponent<KnivesOnLog>();
        tomatos = GetComponent<TomatosOnLog>();
    }
    private void OnEnable() 
    {
        LevelManager.LevelProperties += SetLevel;
    }
    private void OnDisable() 
    {
        LevelManager.LevelProperties -= SetLevel;
    }

    private int GetRandomKnifeNumber()
    {
        return Random.Range(1, 5);
    }

    private void SetLevel()
    {
        level++;

        if(level > 1)
        {
            knives.SetActiveObjects(true, GetRandomKnifeNumber());
        }
        
        if(level % 4 == 3)
        {
            knives.SetActiveObjects(false, GetRandomKnifeNumber());
            LoadBossLevel();
        }
        else
        {
            UIManager.Instance.HideBossLevelText();
            tomatos.SetActiveObjects(false, tomatos.ObjectsCount);
        }
    }
    
    private void LoadBossLevel()
    {
        //change log sprite
        UIManager.Instance.ShowBossLevelText();

        tomatos.SetActiveObjects(true, tomatos.ObjectsCount);
    }
}