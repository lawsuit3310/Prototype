using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

[AddComponentMenu("Playground/Movement/Alien Behavior")]
[RequireComponent(typeof(Rigidbody2D))]
public class AlienBehavior : Physics2DObject
{
	// This is the target the object is going to move towards
	public Transform target;

	[Header("Movement")]
	// Speed used to move towards the target
	public float speed = 1f;

	// Used to decide if the object will look at the target while pursuing it
	public bool lookAtTarget = false;

	// The direction that will face the target
	public Enums.Directions useSide = Enums.Directions.Up;

    void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // FixedUpdate is called once per frame
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
		rigidbody2D.MovePosition(Vector2.Lerp(transform.position, target.position, Time.fixedDeltaTime * speed));

	}

    private void OnEnable()
    {
        this.transform.position = new Vector2(
            UnityEngine.Random.Range(-16f, 14f), UnityEngine.Random.Range(-11f, 15f)
        );
        gatherableItemId = 2;
    }

    
    private void OnDisable()
    {
        if(!GameManager.IncreaseItemAmount(gatherableItemId, 1))
            Debug.Log("Alien Failed");
    }
}
