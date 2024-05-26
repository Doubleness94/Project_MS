using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Character : MonoBehaviour
{
    public PlayerMovement player;

    private void FixedUpdate()
    {
        Turn();
    }

    private void Turn()
    {
        transform.LookAt(transform.position + player.moveVec.normalized);
    }
}
