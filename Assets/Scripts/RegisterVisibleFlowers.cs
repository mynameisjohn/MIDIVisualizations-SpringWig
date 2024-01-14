using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterVisibleFlowers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameVisible()
    {
        FindObjectOfType<GameManager>().RegisterVisibleFlower(gameObject);
    }
    private void OnBecameInvisible()
    {
        FindObjectOfType<GameManager>().UnregisterInvisibleFlower(gameObject);
    }
}
