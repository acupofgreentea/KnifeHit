using UnityEngine;

public class ControlKnives : MonoBehaviour
{
    [SerializeField] private GameObject knife;

    [SerializeField] private int remainingKnifeCount;

    [SerializeField] private int _knifeCount;

    public int KnifeCount
    {
        get => _knifeCount; 
        private set => _knifeCount = value;
    }

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
        remainingKnifeCount = _knifeCount;
        
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
        _knifeCount++;
        remainingKnifeCount = _knifeCount;
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
