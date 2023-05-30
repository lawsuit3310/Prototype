using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBehavior : MonoBehaviour
{
    public bool isInteractable = false;
    public abstract void DistanceChecker();
    public abstract void Interact();
}
