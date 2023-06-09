using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

[AddComponentMenu("Playground/Movement/EnemyBehavior")]
[RequireComponent(typeof(Rigidbody2D))]
public class BeeBehavior : EnemyBehavior
{
    private void OnEnable()
    {
        base.OnEnable();
    }
}
