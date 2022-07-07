using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private static AnimationController instance;

    private GameObject machine;

    private int newAnimation = -1;

    private int oldAnimation = -1;

    private int volt = 0;

    private int angle = 0;

    private void Awake()
    {
        instance = this;
    }

    public static AnimationController GetInstance()
    {
        return instance;
    }

    public void SetMachineObject(GameObject _machine)
    {
        machine = _machine;
    }

    public void SetVolt(int _volt)
    {
        volt = _volt;
    }

    public void SetAngle(int _angle)
    {
        angle = _angle;
    }

    public void RunAnimation()
    {
        if (volt == 5)
        {
            if (angle == 8)
            {
                newAnimation = 2;
            }
            else if (angle == 15)
            {
                newAnimation = 3;
            }
        }
        else
        {
            newAnimation = -1;
        }

        if (newAnimation != oldAnimation)
        {
            if (oldAnimation != -1)
            {
                machine
                    .transform
                    .GetChild(oldAnimation)
                    .gameObject
                    .SetActive(false);
            }
            if (newAnimation != -1)
            {
                machine
                    .transform
                    .GetChild(newAnimation)
                    .gameObject
                    .SetActive(true);
                oldAnimation = newAnimation;
            }
        }
    }
}
