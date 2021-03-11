using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntroCamera : MonoBehaviour
{
    public UnityEvent OnIntroStart;
    public UnityEvent OnIntroEnd;

    public void IntroStarted()
    {
        OnIntroStart.Invoke();
    }

    public void IntroFinish()
    {
        OnIntroEnd.Invoke();
    }
}
