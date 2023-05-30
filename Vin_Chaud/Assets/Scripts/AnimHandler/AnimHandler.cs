using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour
{ 
    // Start is called before the first frame update
    public void attackAnimOFF()
    {
        Animator anim = GetComponent<Animator>();
        GetComponentInChildren<BoxCollider2D>().isTrigger = true;
        anim.SetBool("isAttacking", false);
        GetComponentInParent<HealthSystemAttribute>().ChangeDamageableStatus(false);
    }
}
