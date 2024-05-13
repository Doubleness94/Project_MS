using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    private void OnTriggerExit(Collider collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        
    }
}
