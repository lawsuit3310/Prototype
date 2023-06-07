using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WizardBehavior : InteractableBehavior
{
    [SerializeField] private GameObject player;
    [SerializeField] private float Speed = 1;
    
    public static GroceryUI StoreUI;
    private GameObject instance;
    private Converse _converse;
    private Rigidbody2D _rigid;

    private void Awake()
    {
        StoreUI = GameObject.FindWithTag("GROCERYMANAGER").GetComponent<GroceryUI>();
        _converse = GameObject.FindWithTag("CONVERSEMANAGER").GetComponent<Converse>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartWandering();
    }

    private void Update()
    {
        base.Update();
        DistanceChecker();
        Interactable();
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

    public override void DistanceChecker()
    {
        var distance = (player.transform.position - gameObject.transform.position).magnitude;
        isInteractable = distance < 3.5f;
    }

    public override void Interactable()
    {
        if (this.isInteractable)
        {
            var position = this.transform.position;
            if(Input.GetKeyDown(KeyCode.Z))
                Interact();
            if (instance == null)
            {
                instance = Instantiate(interactableUi);
            }
            else return;
            instance.SetActive(true);
            instance.transform.parent = this.gameObject.transform;
            instance.transform.position =
                new Vector3() { x = position.x, y = position.y + 0.5f };
            instance.transform.localScale = 
                new Vector2()
                    {
                        x = this.transform.localScale.x < 0 ?
                            Mathf.Abs(instance.transform.localScale.x) * -1 :
                            Mathf.Abs(instance.transform.localScale.x),
                        y = instance.transform.localScale.y
                    };
        }
    }

    public override void Interact()
    {
        Time.timeScale = 0;
        _converse.showTextPanel("Wizard", "Text Text");
        _converse.isShowStorePanelEnd = true;
    }
}

    