using System;
using UnityEngine;
using System.Collections;

// All component that require physics inherit from this class, for easy access to the Rigidbody2D component

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Physics2DObject : MonoBehaviour
{
	[HideInInspector]
	public new Rigidbody2D rigidbody2D;
    
    public int gatherableItemId = 2;

	void Awake ()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

    private void OnEnable()
    {
        
    }
}
