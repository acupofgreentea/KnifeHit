using UnityEngine;

public class KnivesOnLog : MonoBehaviour, IControlObjectsOnLog
{
    [SerializeField] private GameObject[] knife;

    private int _knifeCount;
    public int ObjectsCount { get => _knifeCount ; set => _knifeCount = value; }

    private void Awake() 
    {
        _knifeCount = knife.Length;
    }

    public void SetActiveObjects(bool isActive, int count) 
    {
        for (int i = 0; i < count; i++)
        {
            knife[i].SetActive(isActive);
        }
    }
}
