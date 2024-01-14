using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseScale : MonoBehaviour
{
    public float _DecayRate;
    public float _IncreaseSmoothAmount = 0.5f;
    public float _DecreaseSmoothAmount = 0.5f;

    Vector3 _initialScale;
    float _currentRate;
    float _targetRate;
    float _currentVelocity;

    private void Start()
    {
        _initialScale = transform.localScale;
    }

    public void Pulse(float amount)
    {
        _targetRate += amount;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentRate > 0f)
        {
            transform.localScale = _initialScale + _currentRate * Vector3.one;
        }

        float smoothAmount = _currentRate < _targetRate ? _IncreaseSmoothAmount : _DecreaseSmoothAmount;
        _currentRate = Mathf.SmoothDamp(_currentRate, _targetRate, ref _currentVelocity, smoothAmount);
        _targetRate *= _DecayRate;

        if (_currentRate < 0.0001f)
        {
            _currentRate = 0f;
            transform.localScale = _initialScale;
        }
    }
}
