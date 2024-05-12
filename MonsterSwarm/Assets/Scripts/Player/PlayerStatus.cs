using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : LivingEntity
{
    public float moveSpeed;
    public float atkRate;

    public float hp;
    public int maxExp;
    public int exp;
    public int level;
    public int atk;
    public int def;
    public int dodge;
    public int crc; //critical chance
    public int crd; //critical damage


    protected override void OnEnable()
    {
        base.OnEnable();
        hp = health;
        //maxExp 설정
        exp = 0;
        level = 1;
        //시작 공격력
        //시작 방어력
        //시작 회피력
        //시작 크리티컬 확률
        //시작 크리티컬 데미지
    }

    public override void Die()
    {
        base.Die();
    }

    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!dead)
        {
            //애니메이션, 사운드, 이펙트 등등
        }
        base.OnDamage(damage, hitPoint, hitNormal);
    }
}
