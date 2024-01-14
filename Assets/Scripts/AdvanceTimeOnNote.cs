using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceTimeOnNote : NoteReceiver
{
    public float _IncreaseTimeBy = 0.1f;
    int _noteCount = 0;
    public override void HandleNote(int noteNumber)
    {
        if (_noteCount == 0)
            FindObjectOfType<GameManager>().IncreaseTimeInCycleOffset(_IncreaseTimeBy);
        _noteCount++;
    }
    public override void HandleNoteOff(int noteNumber)
    {
        _noteCount--;
        base.HandleNoteOff(noteNumber);
    }
}
