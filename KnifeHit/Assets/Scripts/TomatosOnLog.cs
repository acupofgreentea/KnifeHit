using UnityEngine;

public class TomatosOnLog : MonoBehaviour, IControlObjectsOnLog
{
    [SerializeField] private GameObject[] tomato;

    private int _tomatoCount;

    public int ObjectsCount { get => _tomatoCount; set => _tomatoCount = tomato.Length;}

    public void SetActiveObjects(bool isActive, int count)
    {
        for (int i = 0; i < count; i++)
        {
            tomato[i].SetActive(isActive);
        }
    }
}
