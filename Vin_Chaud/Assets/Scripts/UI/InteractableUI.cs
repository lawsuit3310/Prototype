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

        if (this.transform.parent.localScale.x < 0)
            this.transform.localScale = new Vector3()
            {
                x = Mathf.Abs(this.transform.localScale.x) * -1,
                y = this.transform.localScale.y,
                z = this.transform.localScale.z
            };
        else
            this.transform.localScale = new Vector3()
            {
                x = Mathf.Abs(this.transform.localScale.x),
                y = this.transform.localScale.y,
                z = this.transform.localScale.z
            };
    }
}
