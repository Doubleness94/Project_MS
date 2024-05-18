using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float hAxis { get; private set; }
    public float vAxis { get; private set; }
    public bool isMove = false;
    [SerializeField]
    private bool isAtkRdy;
    private float atkDelay = 0;
    private Rigidbody playerRigid;
    private Animator anim;
    public Vector3 moveVec;
    public Vector3 inputVec;
    public PlayerStatus status;
    

    private void Awake()
    {
        playerRigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        status = GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        if (status.dead)
        {
            hAxis = 0;
            vAxis = 0;
            return;
        }

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        if(moveVec != Vector3.zero)
        {
            isMove = true;
        }
        else
        {
            isMove = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
        anim.SetBool("isMove", isMove);
        BasicAttack();
    }

    private void Move()
    {
        inputVec = new Vector3(hAxis, 0, vAxis);
        moveVec = Quaternion.Euler(0, 45, 0) * inputVec;
        transform.position += moveVec.normalized * status.moveSpeed * Time.deltaTime;
        //transform.LookAt(transform.position + moveVec);
        
    }
    
    private void BasicAttack()
    {
        atkDelay += Time.deltaTime;
        isAtkRdy = status.atkRate < atkDelay;
        if(isAtkRdy && !status.dead)
        {
            anim.SetTrigger("Attack");
            atkDelay = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //아이템, 경험치 습득
        if (!status.dead)
        {

        }
    }

}
