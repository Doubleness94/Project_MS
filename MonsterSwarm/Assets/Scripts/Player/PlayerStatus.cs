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
        //maxExp ����
        exp = 0;
        level = 1;
        //���� ���ݷ�
        //���� ����
        //���� ȸ�Ƿ�
        //���� ũ��Ƽ�� Ȯ��
        //���� ũ��Ƽ�� ������
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
            //�ִϸ��̼�, ����, ����Ʈ ���
        }
        base.OnDamage(damage, hitPoint, hitNormal);
    }
}
