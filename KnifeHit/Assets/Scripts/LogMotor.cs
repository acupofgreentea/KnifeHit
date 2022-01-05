using System.Collections;
using System;
using UnityEngine;

public class LogMotor : MonoBehaviour
{
    [Serializable]
    public class RotationElements
    {
        public float speed;
        public float duration;
    }
    
    public RotationElements[] rotationElements;

    WheelJoint2D wheelJoint;

    JointMotor2D motor;

    int rotationIndex;
    
    void Awake()
    {
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();
        StartCoroutine(StartRotation());
    }
    public void NextLevelSpeed(int logSpeed)
    {
        for (int i = 0; i < rotationElements.Length; i++)
        {
            rotationElements[i].speed *= logSpeed;
        }

        DestroyChildKnives();
    }

    void DestroyChildKnives()
    {
        GameObject[] childKnives;

        childKnives = GameObject.FindGameObjectsWithTag("Knife");

        foreach (var knives in childKnives)
        {
            Destroy(knives);
        }
    }

    int GetRandomIndex()
    {
        rotationIndex = UnityEngine.Random.Range(0, rotationElements.Length);
        return rotationIndex;
    }

    IEnumerator StartRotation()
    {

        while(true)
        {
            yield return new WaitForFixedUpdate();

            motor.motorSpeed = rotationElements[GetRandomIndex()].speed;
            motor.maxMotorTorque = 10000;
            wheelJoint.motor = motor;

            yield return new WaitForSeconds(rotationElements[GetRandomIndex()].duration);
        }

    }
}
