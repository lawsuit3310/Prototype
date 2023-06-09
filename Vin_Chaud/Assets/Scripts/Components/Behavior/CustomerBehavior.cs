using Data;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class CustomerBehavior : InteractableBehavior
{
    [SerializeField] private float spd = 1f;

    private static bool _isCalculating = false;
    
    private float _coefficient;
    private string _think;
    private Rigidbody2D _rigid;
    private Collider2D _col;
    private Animator _anim;
    private GameObject _textInstance = null;
    private TMP_Text _text;
    
    public static GameObject CustomerText;
    public static GroceryUI GroceryUI;
    private void OnEnable()
    {
        InteractDistance = 1.5f;

        this.transform.position = new Vector3(
            12, -8);
        spriteUtillity.flip(this.transform);
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _col = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        RayCasting();
        AnimationChecker();
    }
    
    public override void Restriction()
    {
        if (this.transform.position.x < limitPos[0] || this.transform.position.x > limitPos[1])
        {
            if (_col.enabled)
            {
                this.transform.position = new Vector3()
                {
                    x = this.transform.position.x < limitPos[0] ? limitPos[0] : limitPos[1],
                    y = this.transform.position.y
                };
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public override void Interact()
    {
        Debug.Log(true);
    }

    private void RayCasting()
    {
        _coefficient = transform.localScale.x > 0 ? 1 : -1;
        RaycastHit2D isHit = Physics2D.Raycast(
            new Vector2()
            {
                x = transform.position.x + 0.4f * _coefficient,
                y = transform.position.y + 0.5f
            }, transform.right * _coefficient, 1f);
        if (!isHit)
        {
            _coefficient *= spd;
            _rigid.velocity = Vector2.right * _coefficient;
        }
        else
        {
            _rigid.velocity = Vector2.zero;
            if (isHit.collider.name == "Calculator" && !_isCalculating)
                StartCoroutine(Calculate());
        }
    }

    private void AnimationChecker()
    {
        if (_rigid.velocity.magnitude >= 0.2f)
        {
            _anim.SetBool("isRun", true);
        }
        else
        {
            
            _anim.SetBool("isRun", false);
            _anim.SetTrigger("idle");
        }
    }
    
    private void SmallTalk()
    {
        if (_textInstance.IsUnityNull())
        {
            _textInstance = Instantiate(CustomerText);
            _textInstance.transform.SetParent(this.transform);
            _textInstance.transform.position = this.transform.position + new Vector3(){y = 4f};
            _text = _textInstance.GetComponentInChildren<TMP_Text>();
        }

        _text.text = _think;
    }
    
    
    public static bool BuyAlcohol(int AlcoholIndex)
    {
        bool result = true;

        if (!GameManager.DecreaseAlcoholAmount(AlcoholIndex))
            result = false;
        else
        {
            GameManager.IncreaseMoney(100);
        }
        
        return result;
    }
    
    IEnumerator Calculate()
    {
        _isCalculating = true;

        //Calculating Process
        
        int index = Random.Range(0, GameManager.GetInventory().Count);

        //구매 성공했으면 true 실패하면 false
        bool flag = BuyAlcohol(index);
        _think = "";
        
        for (int i = 0; i < 4; i++)
        {
            _think += '.';
            SmallTalk();
            yield return new WaitForSeconds(1f);
        }

        _think = flag ? $"좋아" : $"뭐야, {GroceryDataHandler.GetAlcoholDictionary()[index]["NAME"]} 도 없어?";
        
        SmallTalk();
        yield return new WaitForSeconds(1.5f);
        
        GroceryUI.RefreshUI();
        _textInstance.SetActive(false);
        spriteUtillity.flip(this.transform);
        this.transform.position = new Vector3()
        {
            x = transform.position.x,
            y = transform.position.y - 1f
        };

        _col.enabled = false;
        
        CustomerManager.CustomerInstance.Pop();
        _isCalculating = false;
        yield return null;
    }
}