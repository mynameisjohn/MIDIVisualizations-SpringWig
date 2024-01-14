using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCameraUp : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.up = FindObjectOfType<Camera>().transform.up;
    }
}
