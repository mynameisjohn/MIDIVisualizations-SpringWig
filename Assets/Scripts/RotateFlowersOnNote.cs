using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotateFlowersOnNote : NoteReceiver
{
    public int[] _RotateNotes;
    public float _PulseAmount = 0.01f;
    public float _DecayRate = 0.985f;

    public float _ScalePulseAmount = 0.01f;
    public float _ScaleDecayRate = 0.985f;
    public float _ScaleIncreaseSmoothAmount = 0.5f;
    public float _ScaleDecreaseSmoothAmount = 0.5f;

    public GameObject _Sun;
    public float _SunPulseAmount = 1f;

    public int _FlowerScaleNoteMin = 60;
    public int _FlowerScaleNoteMax = 70;

    public KeyCode _DebugKey = KeyCode.Space;

    private void Start()
    {
        ApplyValues();
    }
    public override void HandleNote(int noteNumber)
    {
        if (_RotateNotes.Contains(noteNumber))
        {
            pulseRotate();
        }
        if (noteNumber >= _FlowerScaleNoteMin && noteNumber <= _FlowerScaleNoteMax)
        {
            pulseScale();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(_DebugKey))
        {
            pulseRotate();
            pulseScale();
        }
    }

    public void ApplyValues()
    {
        foreach (PulseRotate r in GetComponentsInChildren<PulseRotate>())
        {
            r._DecayRate = _DecayRate;
        }

        foreach (PulseScale s in GetComponentsInChildren<PulseScale>())
        {
            s._DecayRate = _ScaleDecayRate;
            s._IncreaseSmoothAmount = _ScaleIncreaseSmoothAmount;
            s._DecreaseSmoothAmount = _ScaleDecreaseSmoothAmount;
        }
    }

    void pulseRotate()
    {
        foreach (PulseRotate r in GetComponentsInChildren<PulseRotate>())
        {
            r.Pulse(_PulseAmount);
        }
        
        _Sun.GetComponentInChildren<PulseScale>().Pulse(_SunPulseAmount);
    }

    void pulseScale()
    {
        int numFlowers = FindObjectOfType<GameManager>()._VisibleFlowers.Count;
        int idxOfFlowerToPulse = Random.Range(0, numFlowers);
        GameObject flowerToPulse = FindObjectOfType<GameManager>()._VisibleFlowers[idxOfFlowerToPulse];
        flowerToPulse.GetComponent<PulseScale>().Pulse(_ScalePulseAmount);
    }
}
