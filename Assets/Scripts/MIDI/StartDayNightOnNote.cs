using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDayNightOnNote : NoteReceiver
{
    public int _StartOnNote;

    private void Start()
    {
        
    }
    public override void HandleNote(int noteNumber)
    {
        if (noteNumber == _StartOnNote)
        {
            GetComponent<GameManager>().StartDayNightCycle();
        }
        base.HandleNote(noteNumber);
    }
}
