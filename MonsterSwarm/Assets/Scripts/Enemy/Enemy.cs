using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    public LayerMask whatIsTarget; //추적 대상 레이어

    public LivingEntity targetEntity; //추적할 대상

    private Animator enemyAnimator;
    private Rigidbody rigid;

    public float damage;
    public float timeBetAttack = 0.5f;//공격 간격
    public float speed;
    public float angleSpeed;
    private float lastAttackTime;


    private bool hasTarget
    {
        get
        {
            if(targetEntity != null && !targetEntity.dead)
            {
                return true;
            }

            return false;
        }
    }

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    public void Setup(float newHealth, float newDamage, float newSpeed)
    {
        Collider[] enemyColliders = GetComponents<Collider>();
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            enemyColliders[i].enabled = true;
        }
        maxHealth = newHealth;
        health = newHealth;
        damage = newDamage;
        speed = newSpeed;
        angleSpeed = 120f;
        base.dead = false;
        StartCoroutine(UpdateTarget());

    }

    private void Update()
    {
        enemyAnimator.SetBool("HasTarget", hasTarget);
        if (hasTarget)
        {
            Vector3 direction = targetEntity.transform.position - transform.position;
            direction.Normalize();
            direction.y = 0;

            //회전
            transform.forward = Vector3.Lerp(transform.forward, direction, angleSpeed * Time.deltaTime);
            //이동
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {

            speed = 0;
            angleSpeed = 0;
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
    public IEnumerator UpdateTarget()
    {
        while (!dead)
        {
            if(!hasTarget)
            {
                
                Collider[] colliders = Physics.OverlapSphere(transform.position, 100f, whatIsTarget);

                for(int i = 0; i < colliders.Length; i++)
                {
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();

                    if(livingEntity != null && !livingEntity.dead)
                    {
                        targetEntity = livingEntity;
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();

        Collider[] enemyColliders = GetComponents<Collider>();
        for(int i = 0;i < enemyColliders.Length;i++)
        {
            enemyColliders[i].enabled = false;
        }

        speed = 0;
        angleSpeed = 0;
        enemyAnimator.SetBool("Die", dead);
        new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(!dead && Time.time >= lastAttackTime + timeBetAttack)
        {
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();

            if(attackTarget != null && attackTarget == targetEntity)
            {
                Debug.Log("몬스터의 공격");
                lastAttackTime = Time.time;
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;
                

                attackTarget.OnDamage(damage, hitPoint, hitNormal);
            }
        }
    }
}
