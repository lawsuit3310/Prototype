using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WizardBehavior : InteractableBehavior
{
    [SerializeField] private float Speed = 1;
    
    public static GroceryUI StoreUI;
    private Converse _converse;
    private Rigidbody2D _rigid;

    private void Awake()
    {
        base.Awake();   
        StoreUI = GameObject.FindWithTag("GROCERYMANAGER").GetComponent<GroceryUI>();
        _converse = GameObject.FindWithTag("CONVERSEMANAGER").GetComponent<Converse>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartWandering();
    }

    private void AnimationChecker()
    {
        var velocity = _rigid.velocity;
        var scale = transform.localScale;
        if(velocity.magnitude >= 0.1f)
        {
            WizardAnimHandler.Run();

            this.transform.localScale =
                new Vector3()
                {
                    x = Mathf.Abs(scale.x) * (velocity.x > 0 ? 1 : -1),
                    y = scale.y
                };
        }
        else
        {
            if (Random.Range(0, 5f) < 1.5f )
            {
                WizardAnimHandler.ResetAnimation();
                WizardAnimHandler.LookUp();
                return;
            }
            else
                WizardAnimHandler.Idle();
        }
    }
    private void StartWandering()
    {
        StartCoroutine(Wandering());
    }
    private IEnumerator Wandering()
    {
        while (true)
        {
            float nextTime = Random.Range(3, 6f);
            var absSpeed = Speed * Vector2.right * Random.Range(-1,2);
            _rigid.velocity = absSpeed;
            AnimationChecker();
            yield return new WaitForSeconds(nextTime);
        }
    }

    public override void Interact()
    {
        Time.timeScale = 0;
        _converse.showTextPanel("Wizard", "Text Text");
        _converse.isShowStorePanelEnd = true;
    }
}

    