using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBehavior : MonoBehaviour
{
    public bool isInteractable = false;
    public float[] limitPos = {0,0}; 
    public abstract void DistanceChecker();
    public abstract void Interactable();
    public abstract void Interact();

    protected void Update()
    {
        Restriction();
    }

    private void Restriction()
    {
        if (this.transform.position.x < limitPos[0] || this.transform.position.x > limitPos[1])
        {
            this.transform.position = new Vector3()
            {
                x = this.transform.position.x < limitPos[0] ? limitPos[0] : limitPos[1],
                y = this.transform.position.y
            };
        }
    }
}
