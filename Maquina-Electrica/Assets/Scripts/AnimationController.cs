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

    // Function that get the AnimationController's class instance
    public static AnimationController GetInstance()
    {
        return instance == null
            ? instance = new AnimationController()
            : instance;
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
            if (angle == 0)
            {
                newAnimation = 2;
            }
            else if (angle == 2)
            {
                newAnimation = 3;
            }
            else if (angle == 4)
            {
                newAnimation = 4;
            }
            else if (angle == 8)
            {
                newAnimation = 5;
            }
            else if (angle == 15)
            {
                newAnimation = 6;
            }
            else if (angle == 30)
            {
                newAnimation = 7;
            }
            else if (angle == 45)
            {
                newAnimation = 8;
            }
            else if (angle == 90)
            {
                newAnimation = 9;
            }
        }
        else if (volt == 15)
        {
            if (angle == 0)
            {
                newAnimation = 10;
            }
            else if (angle == 2)
            {
                newAnimation = 11;
            }
            else if (angle == 4)
            {
                newAnimation = 12;
            }
            else if (angle == 8)
            {
                newAnimation = 13;
            }
            else if (angle == 15)
            {
                newAnimation = 14;
            }
            else if (angle == 30)
            {
                newAnimation = 15;
            }
            else if (angle == 45)
            {
                newAnimation = 16;
            }
            else if (angle == 90)
            {
                newAnimation = 17;
            }
        }
        else if (volt == 30)
        {
            if (angle == 0)
            {
                newAnimation = 18;
            }
            else if (angle == 2)
            {
                newAnimation = 19;
            }
            else if (angle == 4)
            {
                newAnimation = 20;
            }
            else if (angle == 8)
            {
                newAnimation = 21;
            }
            else if (angle == 15)
            {
                newAnimation = 22;
            }
            else if (angle == 30)
            {
                newAnimation = 23;
            }
            else if (angle == 45)
            {
                newAnimation = 24;
            }
            else if (angle == 90)
            {
                newAnimation = 25;
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
            }
            oldAnimation = newAnimation;
        }
    }
}
