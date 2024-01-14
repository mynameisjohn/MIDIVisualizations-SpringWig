using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float _Tempo;
    public float _BeatsPerCycle;

    bool _hasStarted;
    float _startTime;

    public List<GameObject> _GrovePrefabs;
    public Vector3 _GrovePlacementOffset;
    public int _SunMoonEvery = 3;
    int _lastGroveIndex;
    GameObject _lastGroveCreated;

    public GameObject _SunPrefab;
    public GameObject _MoonPrefab;
    public Vector3 _SunMoonPositionOffset;
    public int _SunMoonEveryOffset = 2;

    public List<GameObject> _VisibleFlowers;

    public float _TimeInCycleOffset;
    public float _TimeInCycleOffsetSpeedChange = 0.1f;
    float _currentTimeInCycleOffset;
    float _timeInCycleOffsetVelocity;
    public KeyCode _DebugIncreaseTimeInCycleOffsetKey = KeyCode.UpArrow;
    public float _DebugIncreaseTimeInCycleOffsetAmount = 0.01f;

    public float _MaxGroves = 5;
    List<GameObject> _createdGroves = new List<GameObject>();

    private void Awake()
    {
        FindObjectOfType<Camera>().gameObject.GetComponent<MoveUp>().enabled = false;
        FindObjectOfType<Camera>().gameObject.GetComponentInChildren<AutoRotate>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartDayNightOnNote startDayNightCycleFromNote = GetComponent<StartDayNightOnNote>();
        if (startDayNightCycleFromNote == null || !startDayNightCycleFromNote.enabled)
        {
            StartDayNightCycle();
        }

        CreateNewGrove();
    }

    public void StartDayNightCycle()
    {
        _hasStarted = true;
        _startTime = Time.time;
        FindObjectOfType<Camera>().gameObject.GetComponent<MoveUp>().enabled = true;
        FindObjectOfType<Camera>().gameObject.GetComponentInChildren<AutoRotate>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasStarted)
        {
            float timeInSeconds = Time.time - _startTime;
            float tempo = _Tempo;
            float timeInBeats = timeInSeconds * tempo / 60.0f;
            float timeInCycle = currentTimeInCycleForTimeInBeats(timeInBeats);
            float x = timeInCycle / _BeatsPerCycle;
            // Debug.Log(timeInBeats + " " + timeInCycle);
            
            // Transform sunMoonTransform = FindObjectOfType<Camera>().gameObject.GetComponentInChildren<AutoRotate>().gameObject.transform;
            // sunMoonTransform.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0f, 0f, 180), x);
            if (Input.GetKey(_DebugIncreaseTimeInCycleOffsetKey))
            {
                IncreaseTimeInCycleOffset(_DebugIncreaseTimeInCycleOffsetAmount);
            }
            _currentTimeInCycleOffset = Mathf.SmoothDamp(_currentTimeInCycleOffset, _TimeInCycleOffset, ref _timeInCycleOffsetVelocity, _TimeInCycleOffsetSpeedChange);
            // Debug.Log(_currentTimeInCycleOffset);

            GetComponent<GradientSampler>().UpdateGradients(Mathf.Repeat(timeInCycle + _currentTimeInCycleOffset, 1f));

        }
    }
    float currentTimeInCycleForTimeInBeats(float timeInBeats)
    {
        float timeInCycle = timeInBeats / _BeatsPerCycle;
        int wholeTimeInCycle = (int)timeInCycle;
        float currentTimeInCycle = timeInCycle - wholeTimeInCycle;
        return currentTimeInCycle;
    }


    public void CreateNewGrove()
    {
        Camera c = FindObjectOfType<Camera>();
        GameObject prefab = _GrovePrefabs[_lastGroveIndex++ % _GrovePrefabs.Count];
        GameObject grove = Instantiate(prefab, transform.position + _lastGroveIndex * _GrovePlacementOffset, Quaternion.identity, transform);
        GetComponent<RotateFlowersOnNote>().ApplyValues();

        if (_MoonPrefab != null)
        {
            if ((_lastGroveIndex + _SunMoonEveryOffset) % _SunMoonEvery == 0)
            {
                GameObject sunMoon = Instantiate(_MoonPrefab, grove.transform.position + _SunMoonPositionOffset, _MoonPrefab.transform.rotation, grove.transform);
                GetComponent<RotateFlowersOnNote>()._Sun = sunMoon;
            }
        }

        _createdGroves.Add(grove);
        if (_createdGroves.Count > _MaxGroves)
        {
            GameObject groveToDestroy = _createdGroves[0];
            groveToDestroy.SetActive(false);
            _createdGroves.RemoveAt(0);
            Destroy(groveToDestroy);
        }
    }

    public void RegisterVisibleFlower(GameObject flower)
    {
        _VisibleFlowers.Add(flower);
    }

    public void UnregisterInvisibleFlower(GameObject flower)
    {
        _VisibleFlowers.Remove(flower);
    }

    public void IncreaseTimeInCycleOffset(float increaseBy)
    {
        _TimeInCycleOffset += increaseBy;
    }
}
