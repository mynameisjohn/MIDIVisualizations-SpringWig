using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroveTrigger : MonoBehaviour
{
    List<MoveUp> _moversToActivate;

    private void Awake()
    {
        _moversToActivate = new List<MoveUp>();
        foreach (MoveUp m in GetComponentsInChildren<MoveUp>())
        {
            m.enabled = false;
            _moversToActivate.Add(m);
        }
    }

    private void Start()
    {
        if (GetComponent<Collider>().bounds.Contains(FindObjectOfType<Camera>().transform.position))
        {
            enableMovers();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Camera>() != null)
        {
            enableMovers();

            FindObjectOfType<GameManager>().CreateNewGrove();
        }
    }

    void enableMovers()
    {
        foreach (MoveUp m in _moversToActivate)
        {
            m.enabled = true;
        }
    }
}
