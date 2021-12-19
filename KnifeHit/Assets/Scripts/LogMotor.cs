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

    int rotationIndex = 0;
    
    void Awake()
    {
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();
        StartCoroutine(StartRotation());
    }

    public void NextLevelSpeed(int level)
    {
        for (int i = 0; i < rotationElements.Length; i++)
        {
            rotationElements[i].speed *= 1.3f;
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

    IEnumerator StartRotation()
    {

        while(true)
        {
            yield return new WaitForFixedUpdate();

            motor.motorSpeed = rotationElements[rotationIndex].speed;
            motor.maxMotorTorque = 10000;
            wheelJoint.motor = motor;

            yield return new WaitForSeconds(rotationElements[rotationIndex].duration);
            rotationIndex++;

            rotationIndex = rotationIndex < rotationElements.Length ? rotationIndex : 0;
        }

    }
}
