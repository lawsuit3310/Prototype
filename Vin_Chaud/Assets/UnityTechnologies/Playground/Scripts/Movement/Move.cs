using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

[AddComponentMenu("Playground/Movement/Move With Arrows")]
[RequireComponent(typeof(Rigidbody2D))]
public class Move : Physics2DObject
{
	[Header("Input keys")]
	public Enums.KeyGroups typeOfControl = Enums.KeyGroups.ArrowKeys;

	[Header("Movement")]
	[Tooltip("Speed of movement")]
	public float speed = 5f;
	public Enums.MovementType movementType = Enums.MovementType.AllDirections;

	[Header("Orientation")]
	public bool orientToDirection = false;
	// The direction that will face the player
	public Enums.Directions lookAxis = Enums.Directions.Up;

    private Vector2 movement, cachedDirection;
    private static Vector2 LastDirection;
    private static Animator anim;
	private float moveHorizontal;
	private float moveVertical;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update gets called every frame
	void Update ()
	{	
		// Moving with the arrow keys
		if(typeOfControl == Enums.KeyGroups.ArrowKeys)
		{
			moveHorizontal = Input.GetAxis("Horizontal");
			moveVertical = Input.GetAxis("Vertical");
		}
		else
		{
			moveHorizontal = Input.GetAxis("Horizontal2");
			moveVertical = Input.GetAxis("Vertical2");
		}

		//zero-out the axes that are not needed, if the movement is constrained
		switch(movementType)
		{
			case Enums.MovementType.OnlyHorizontal:
				moveVertical = 0f;
				break;
			case Enums.MovementType.OnlyVertical:
				moveHorizontal = 0f;
				break;
		}

        movement = new Vector2(moveHorizontal, moveVertical);

        //rotate the GameObject towards the direction of movement
		//the axis to look can be decided with the "axis" variable
		if(orientToDirection)
		{
			if(movement.sqrMagnitude >= 0.01f)
			{
				cachedDirection = movement;;
                LastDirection = cachedDirection;
			}
			Utils.SetAxisTowards(lookAxis, transform, cachedDirection);
		}
    }



	// FixedUpdate is called every frame when the physics are calculated
	void FixedUpdate ()
	{
		// Apply the force to the Rigidbody2d\

        
        // 대각선 방향으로 이동할 경우 이동 알고리즘에 의해 한 방향 만으로 이동할 때 마다 1.414배 만큼 빨리 움직이므로 개선하기 위해 대각선으로 이동 중일 경우 1.414로 나눠줌.
        float flag = (Mathf.Abs(rigidbody2D.velocity.x) > 0.2f && Mathf.Abs(rigidbody2D.velocity.y) > 0.2f) ? Mathf.Sqrt(2) : 1;
        
        //이동
        rigidbody2D.velocity =
            (Vector2.right + Vector2.up) / flag *
            (movement * speed) * Time.deltaTime;

        var scale = transform.localScale;
        
        #region 플레이어 이동에 따른 방향 전환
        if (rigidbody2D.velocity.x > 0 && !anim.GetBool("isAttacking"))
            transform.localScale = new Vector3(
            -1f * Mathf.Abs(scale.x) ,
            scale.y,
            scale.z
            );
        else if (rigidbody2D.velocity.x <  0 && !anim.GetBool("isAttacking"))
            transform.localScale = new Vector3(
                Mathf.Abs(scale.x),
                scale.y,
                scale.z
            );
        #endregion
    }

    public Vector2 GetLastDirection()
    {
        return LastDirection;
    }
}