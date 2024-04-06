using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] float slowDownFactor = 0.1f;
    

    public void SlowMotionEffect()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void NormalSpeed()
    {
        Time.timeScale = 1;
    }
}
