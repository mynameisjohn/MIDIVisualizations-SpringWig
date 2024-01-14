using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PulseRotate : MonoBehaviour
{
    public Vector3 _Axis;
    public float _DecayRate;

    float _currentRate;

    public void Pulse(float amount)
    {
        _currentRate += amount;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentRate > 0f)
        {
            transform.Rotate(_Axis, _currentRate, Space.Self);
        }

        _currentRate *= _DecayRate;
        
        if (_currentRate < 0.0001f)
        {
            _currentRate = 0f;
        }
    }
}
