using System;
using System.Collections;
using UnityEngine;

public abstract class EnemyBehavior : Physics2DObject
{
    private SpriteRenderer _renderer;
    private bool isDamaged = false;
    
    public static Transform target;

    [Header("Movement")]
    public float speed = 1f;
    public bool lookAtTarget = false;
    public Enums.Directions useSide = Enums.Directions.Up;
    
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        target = MonsterSpawner.Target;
    }

    void FixedUpdate ()
    {
        //do nothing if the target hasn't been assigned or it was detroyed for some reason
        if (target.Equals(null))
            return;


        //look towards the target
        if(lookAtTarget)
        {
            Utils.SetAxisTowards(useSide, transform, target.position - transform.position);
        }
		
        //Move towards the target
        if(!isDamaged)
            rigidbody2D.MovePosition(Vector2.Lerp(transform.position, target.position, Time.fixedDeltaTime * speed));

    }

    public IEnumerator Damaged()
    {
        isDamaged = true;
        var vel = rigidbody2D.velocity;
        rigidbody2D.AddForce(new Vector2()
        {
            x = vel.x * -50,
            y = vel.y * -50
        });
        var originColor = _renderer.color;
        for (int i = 0; i < 2; i++)
        {
            _renderer.color =
                new Color()
                {
                    r = 255,
                    g = 0,
                    b = 0,
                    a = 1
                };
            yield return new WaitForSeconds(0.05f);
            _renderer.color = originColor;
            yield return new WaitForSeconds(0.05f);

        }

        isDamaged = false;
        yield return null;

    }
    protected void OnEnable()
    {
        this.transform.position = new Vector2(
            UnityEngine.Random.Range(-16f, 14f), UnityEngine.Random.Range(-11f, 15f)
        );
        gatherableItemId = 0;
    }
    private void OnDisable()
    {
        if(GameManager.IncreaseItemAmount(gatherableItemId, 1))
            Debug.Log("아이템 획득");
        MonsterSpawner._monsterCount--;
    }
}