using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBehavior : MonoBehaviour
{
    protected static GameObject Player;
    public GameObject interactableUi;
    protected GameObject Instance;
    public bool isInteractable = false;
    public float[] limitPos = {0,0};
    protected float InteractDistance = 2.5f;

    public virtual void DistanceChecker()
    {
        var distance = (Player.transform.position - gameObject.transform.position).magnitude;
        isInteractable = distance < InteractDistance;
    }

    public virtual void Restriction()
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
    public virtual void Interactable()
    {
        if (this.isInteractable)
        {
            var position = this.transform.position;
            if(Input.GetKeyDown(KeyCode.Z))
                Interact();
            if (Instance == null)
            {
                Instance = Instantiate(interactableUi);
            }
            else return;
            Instance.SetActive(true);
            Instance.transform.parent = this.gameObject.transform;
            Instance.transform.position =
                new Vector3() { x = position.x, y = position.y + 0.5f };
            Instance.transform.localScale = 
                new Vector2()
                {
                    x = this.transform.localScale.x < 0 ?
                        Mathf.Abs(Instance.transform.localScale.x) * -1 :
                        Mathf.Abs(Instance.transform.localScale.x),
                    y = Instance.transform.localScale.y
                };
        }
    }
    public abstract void Interact();

    protected void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }

    protected void Update()
    {
        Restriction();
        DistanceChecker();
        Interactable();
    }

}
