using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[AddComponentMenu("Brewery")]
public class Brewery : InteractableBehavior
{
    [SerializeField] private GameObject player;
    [SerializeField] private int breweryID = 0;

    private GameObject instance;

    private static BrewryManager _brewryManager;
    // Start is called before the first frame update
    private void Awake()
    {
        _brewryManager = this.transform.parent.GetComponent<BrewryManager>();
    }

    private void FixedUpdate()
    {
        DistanceChecker();
        Interactable();
        if (AlcoholScheduler.IsScheduleDone(breweryID) || AlcoholScheduler.IsScheduleProcessing(breweryID))
        _brewryManager.ShowProgressUI(this);
    }

    public int getBreweryID()
    {
        return breweryID;
    }

    // Update is called once per frame
    public override void DistanceChecker()
    {
        var distance = (player.transform.position - gameObject.transform.position).magnitude;
        this.isInteractable = distance < 2.5f && !AlcoholScheduler.IsScheduleProcessing(breweryID);
    }

    public override void Interactable()
    {
        if (this.isInteractable)
        {
            var position = this.transform.position;
            if(Input.GetKeyDown(KeyCode.Z))
                Interact();
            
            //instance가 null인지 체크
            if (instance == null)
            {
                instance = Instantiate(interactableUi);
            }
            else return;
            instance.SetActive(true);
            instance.transform.parent = this.gameObject.transform;
            instance.transform.position =
                new Vector3() { x = position.x, y = position.y + 0.5f };
            instance.transform.localScale = 
                new Vector2()
                {
                    x = this.transform.localScale.x < 0 ?
                        Mathf.Abs(instance.transform.localScale.x) * -1 :
                        Mathf.Abs(instance.transform.localScale.x),
                    y = instance.transform.localScale.y
                };
        }
    }


    public override void Interact()
    {
        if (AlcoholScheduler.IsScheduleDone(breweryID))
        {
            StartCoroutine(EndScheduleProcess());
        }
        else
        {
            
            _brewryManager.ShowBreweryUI(breweryID);
        }
    }

    IEnumerator EndScheduleProcess()
    {
        GameManager.ShowToastMessage(
            $"{GroceryDataHandler.GetAlcoholDictionary()[breweryID]["NAME"]} 을(를) 획득했습니다!");
        yield return new WaitForSeconds(0.2f);
        AlcoholScheduler.EndSchedule(breweryID);
    }

}
