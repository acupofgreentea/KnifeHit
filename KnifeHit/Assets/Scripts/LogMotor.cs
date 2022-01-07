using System.Collections;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LogMotor : MonoBehaviour
{
    public RotationElements[] rotationElements;

    private WheelJoint2D wheelJoint;

    private JointMotor2D motor;

    private int rotationIndex;

    [SerializeField] private float speedMultiplier = 2;


    private const string usableKnife = "UsableKnife";
    
    void OnEnable()
    {
        LevelManager.LogMotorOnNextLevel += SetLogForNextLevel;
    }
    void OnDisable()
    {
        LevelManager.LogMotorOnNextLevel -= SetLogForNextLevel;
    }
    private void Awake()
    {
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();
    }
    private void Start()
    {
        StartCoroutine(StartRotation());
    }
    
    private void SetMotor()
    {
        motor.motorSpeed = rotationElements[GetRandomIndex()].speed;
        motor.maxMotorTorque = 10000;
        wheelJoint.motor = motor;
    }
    
    public void SetLogForNextLevel()
    {
        SetSpeedForNextLevel();
        DestroyUsedKnives();
    }
    
    private void SetSpeedForNextLevel()
    {
        for (int i = 0; i < rotationElements.Length; i++)
        {
            rotationElements[i].speed *= speedMultiplier;
        }
    }
    
    private void DestroyUsedKnives()
    {
        GameObject[] childKnives;

        childKnives = GameObject.FindGameObjectsWithTag(usableKnife);

        foreach (var knives in childKnives)
        {
            Destroy(knives);
        }
    }
    private int GetRandomIndex()
    {
        rotationIndex = Random.Range(0, rotationElements.Length);
        return rotationIndex;
    }
    
    private IEnumerator StartRotation()
    {
        while(true)
        {
            yield return new WaitForFixedUpdate();
            SetMotor();
            yield return new WaitForSeconds(rotationElements[GetRandomIndex()].duration);
        }

    }
    
    [Serializable]
    public class RotationElements
    {
        public float speed;
        public float duration;
    }
    
}
