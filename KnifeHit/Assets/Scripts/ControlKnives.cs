using UnityEngine;

public class ControlKnives : MonoBehaviour
{
    [SerializeField] private GameObject knife;

    [SerializeField] private int remainingKnifeCount;

    public int knifeCount;

    private LevelManager levelManager;


    private void OnEnable() 
    {
        LevelManager.LevelProperties += SetKnivesForNextLevel;
    }
    
    private void OnDisable() 
    {
        LevelManager.LevelProperties += SetKnivesForNextLevel;
    }

    private void Awake() 
    {
        remainingKnifeCount = knifeCount;
        
        levelManager = GetComponent<LevelManager>();
    }

    private void Start() 
    {
        SpawnKnife();
    }

    public void SpawnKnife()
    {
        Instantiate(knife, transform.position, Quaternion.identity);
        remainingKnifeCount--;
    }

    private void SetKnivesForNextLevel()
    {
        knifeCount++;
        remainingKnifeCount = knifeCount;
    }
    
    public void ResetRemainingKnifeCount()
    {
        int tempKnifeCount = remainingKnifeCount;
        remainingKnifeCount -= tempKnifeCount;
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
            levelManager.Invoke(nameof(levelManager.LoadNextLevel), 0.5f);
        }
    }

}
