using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private GameObject sword;
    private Rigidbody2D rigid;
    public Animator anim;
    
    // 플레이어가 달리는 것과 멈추어 서있는 것을 가르는 기준점
    private const float IdleToRunSpd = 0.5f;

    private bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //공격키를 입력했을 경우
         if (Input.GetButtonDown("Attack"))
         {
             PlayerAttack();
         }
    }

    private void FixedUpdate()
    {
        //플레이어 달리는 애니메이션
        if (Mathf.Abs(rigid.velocity.x) <= IdleToRunSpd && Mathf.Abs(rigid.velocity.y) <= IdleToRunSpd)
        {
            anim.SetBool("isRunning", false);
        }
        else if (Mathf.Abs(rigid.velocity.x) >= IdleToRunSpd || Mathf.Abs(rigid.velocity.y) >= IdleToRunSpd)
        {
            
            anim.SetBool("isRunning", true);
        }

        #region 플레이어가 맵 밖으로 이탈하지 않도록 제한
        if (this.transform.position.y >= 15f)
            this.transform.position = new Vector2(
                this.transform.position.x,
                15f
            );
        ;

        if (this.transform.position.x <= -16.7f)
            this.transform.position = new Vector2(
                -16.7f,
                this.transform.position.y
            );
        ;
        
        if (this.transform.position.y <= -11.5f)
            this.transform.position = new Vector2(
                this.transform.position.x,
                -11.5f
            );
        ;
        
        if (this.transform.position.x >= 14.7f)
            this.transform.position = new Vector2(
                14.7f,
                this.transform.position.y
            );
        
        #endregion
    }

    private void PlayerAttack()
    {
        sword.GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<HealthSystemAttribute>().ChangeDamageableStatus(false);
        if (anim.GetBool("isAttacking")) return;
        //작동함
        anim.SetBool("isAttacking", true);
        
        //애니메이터에서 다시 isAttacking을 false로 만들어 공격 가능하게 함
    }
}
