using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableUI : MonoBehaviour
{
    private InteractableBehavior _parent;
    // Start is called before the first frame update
    private void Start()
    {
        _parent = this.transform.parent.gameObject.GetComponent<InteractableBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_parent.isInteractable)
        {
            Destroy(this.gameObject);
        }
    }
}
