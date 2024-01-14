using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteReceiver : MonoBehaviour
{
    public virtual void HandleNote(int noteNumber)
    {
    }

    public virtual void HandleCC(int ccNumber, int ccValue)
    {
        // 
    }

    public virtual void HandleNoteOff(int noteNumber)
    {

    }
}
