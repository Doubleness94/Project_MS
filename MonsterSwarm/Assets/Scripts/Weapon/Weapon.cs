using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected int level;
    public int damage;
    public float rate;

    public int RandomDamage(int damage)
    {
        int minDamage = (int)(damage * 0.8f);
        int maxDamage = (int)(damage * 1.2f);

        damage = Random.Range(minDamage, maxDamage +1);
        return damage;
    }

    public int CriticalDamage(int damage)
    {
        int critical = (int)(damage * 1.5f);
        return damage;
    }
}
