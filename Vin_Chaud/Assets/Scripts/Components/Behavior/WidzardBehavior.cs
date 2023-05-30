using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WidzardBehavior : InteractableBehavior
{
    [SerializeField] private GameObject player;
    [SerializeField] private float Speed = 1;
    
    private GroceryUI _storeUI;
    private Rigidbody2D _rigid;

    private void Awake()
    {
        _storeUI = GameObject.FindWithTag("GROCERYMANAGER").GetComponent<GroceryUI>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartWandering();
    }

    private void Update()
    {
        DistanceChecker();
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
    private int Act()
    {
        int result = 0;
        
        return result;
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

    public override void DistanceChecker()
    {
        var distance = (player.transform.position - gameObject.transform.position).magnitude;
        isInteractable = distance < 3.5f;
    }
    public override void Interact()
    {
        _storeUI.ShowStorePanel();
    }
}

    