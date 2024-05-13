using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit[] targets;
    public Transform nearestTarget;

    private void FixedUpdate()
    {
        targets = Physics.SphereCastAll(transform.position, scanRange, Vector3.up,0,targetLayer);
        
    }

    private Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach(RaycastHit target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if(curDiff > diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, scanRange);
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
