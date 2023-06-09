using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

[AddComponentMenu("Playground/Movement/Orc Behavior")]
[RequireComponent(typeof(Rigidbody2D))]
public class OrcBehavior : EnemyBehavior
{
    private void OnEnable()
    {
        base.OnEnable();
    }
}
