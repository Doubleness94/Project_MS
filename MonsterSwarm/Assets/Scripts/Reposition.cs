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

        Vector3 playerPos = GameManager.instance.playerMov.transform.position;
        Vector3 myPos = transform.position;
        Vector3 playerDir = GameManager.instance.playerMov.moveVec;

        float dirX = playerPos.x - myPos.x;
        float dirZ = playerPos.z - myPos.z;

        float differX = Mathf.Abs(dirX);
        float differZ = Mathf.Abs(dirZ);

        dirX = dirX > 0 ? 1 : -1;
        dirZ = dirZ > 0 ? 1 : -1;

        switch (transform.tag)
        {
            case "Ground":
                if(differX > differZ)
                {
                    transform.Translate(Vector3.right * dirX * 60);
                }
                else if(differX < differZ)
                {
                    transform.Translate(Vector3.forward * dirZ * 60);
                }
                else if(differZ == differX)
                {
                    transform.Translate(Vector3.right * dirX * 60);
                    transform.Translate(Vector3.forward * dirZ * 60);
                }
                break;
        }

        
    }
}
