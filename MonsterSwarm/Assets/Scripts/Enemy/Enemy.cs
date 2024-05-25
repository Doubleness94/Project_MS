using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    public LayerMask whatIsTarget; //추적 대상 레이어

    public LivingEntity targetEntity; //추적할 대상
    public NavMeshAgent pathFinder; //경로계산 AI 에이전트

    private Animator enemyAnimator;
    //private Renderer enemyRenderer;

    public float damage;
    public float timeBetAttack = 0.5f;//공격 간격
    public float speed;
    public float angleSpeed = 120;
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
        pathFinder = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
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
        pathFinder.speed = newSpeed;
        speed = newSpeed;
        pathFinder.enabled = true;
        pathFinder.isStopped = false;
        base.dead = false;

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public IEnumerator UpdatePath()
    {
        while (!dead)
        {
            if (hasTarget)
            {
                pathFinder.isStopped = false;
                pathFinder.SetDestination(targetEntity.transform.position);
            }
            else
            {
                pathFinder.isStopped=true;
                speed = 0;
                angleSpeed = 0;

                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, whatIsTarget);

                for(int i =  0; i < colliders.Length; i++)
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
}
