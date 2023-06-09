using System;
using UnityEngine;

public class Calculator : InteractableBehavior
{
    private void OnEnable()
    {
        InteractDistance = 1.5f;
    }

    public override void Interact()
    {
        Debug.Log(this.gameObject.name);
    }
}