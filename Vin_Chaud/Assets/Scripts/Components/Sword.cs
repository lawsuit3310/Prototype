using System;
using UnityEngine;
public class Sword : MonoBehaviour
{
    private int ATK = 10;

    private void Start()
    {
        ATK = PlayerData.GetATK();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        try
        {
            //공격하는 중이 아닌데 그냥 무기에 닿은 경우 종료
            if (!GetComponentInParent<Animator>().GetBool("isAttacking")) return;
            //적과 충돌한 것이 아닐경우 종료
            if (!other.gameObject.CompareTag("Enemy")) return;
            
            var healthSystem = other.gameObject.GetComponent<EnemyHealthSystemAttribute>();
            healthSystem.ModifyHealth(ATK * -1);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            // 적이 아닌 물체와 충돌한 경우로 무시
        }
    }
}
