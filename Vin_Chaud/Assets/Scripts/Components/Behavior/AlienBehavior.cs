using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

[AddComponentMenu("Playground/Movement/Alien Behavior")]
[RequireComponent(typeof(Rigidbody2D))]
public class AlienBehavior : EnemyBehavior
{
	// This is the target the object is going to move towards
    private void OnEnable()
    {
        base.OnEnable();
    }
}
