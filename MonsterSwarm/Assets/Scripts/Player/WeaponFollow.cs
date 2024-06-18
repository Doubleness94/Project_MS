using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    public GameObject right_hand;
    private void Update()
    {
        transform.SetPositionAndRotation(right_hand.transform.position, right_hand.transform.rotation);
    }
}
