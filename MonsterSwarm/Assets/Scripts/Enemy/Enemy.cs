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
        angleSpeed = 120f;
        base.dead = false;
        StartCoroutine(UpdateTarget());
        //pathFinder.enabled = true;
        //pathFinder.isStopped = false;
        //StartCoroutine(UpdatePathNav());
        //StartCoroutine(UpdatePath());

    }

    private void Start()
    {
        
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
        }
    }
    public IEnumerator UpdateTarget()
    {
        while (!dead)
        {
            if (hasTarget)
            {

            }
            else
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
    /*
    public IEnumerator UpdatePathNav()
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
                pathFinder.isStopped = true;
                speed = 0;

                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, whatIsTarget);

                //모든 콜라이더들을 순회하면서, 살아있는 LivingEntity 찾기
                for(int i =  0; i < colliders.Length; i++)
                {
                    //콜라이더로부터 LivingEntity 컴포넌트 가져오기
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();

                    // LivingEntity 컴포넌트가 존재하며, 해당 LivingEntity가 살아있다면,
                    if (livingEntity != null && !livingEntity.dead)
                    {
                        // 추적 대상을 해당 LivingEntity로 설정
                        targetEntity = livingEntity;

                        break;
                    }
                }
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
    */
}
