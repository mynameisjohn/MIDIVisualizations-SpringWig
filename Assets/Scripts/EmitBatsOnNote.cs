using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmitBatsOnNote : NoteReceiver
{
    public ParticleSystem _BatParticleSystem;
    public int _ParticlesToEmit;
    public int[] _NotesForEmission;

    public override void HandleNote(int noteNumber)
    {
        _BatParticleSystem.Emit(_ParticlesToEmit);
    }
}
