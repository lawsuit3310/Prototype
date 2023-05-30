using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Attributes/Health System")]
public class HealthSystemAttribute : MonoBehaviour
{
	public int health = 3;

	private UIScript _ui;
    private Collider2D _collider;
    private SpriteRenderer _sprite;
	private int _maxHealth;

	// Will be set to 0 or 1 depending on how the GameObject is tagged
	// it's -1 if the object is not a player
	private int _playerNumber;

    [SerializeField] private bool isDamageable = true;
    [SerializeField] private float freeTimeForDamage = 1f;

	private void Start()
	{
		// Find the UI in the scene and store a reference for later use
        _ui = GameObject.FindObjectOfType<UIScript>();
        _collider = this.gameObject.GetComponent<BoxCollider2D>();
        Debug.Log(_collider.gameObject.name);

		// Set the player number based on the GameObject tag
		switch(gameObject.tag)
		{
			case "Player":
				_playerNumber = 0;
				break;
			case "Player2":
				_playerNumber = 1;
				break;
			default:
				_playerNumber = -1;
				break;
		}

		// Notify the UI so it will show the right initial amount
		if(_ui != null
			&& _playerNumber != -1)
		{
			_ui.SetHealth(health, _playerNumber);
		}

        health = PlayerData.GetMaxHP();
		_maxHealth = health; //note down the maximum health to avoid going over it when the player gets healed
        _ui.SetHealth(health, _playerNumber);
	}


	// changes the energy from the player
	// also notifies the UI (if present)
	public void ModifyHealth(int amount)
    {
        if (!isDamageable && this.gameObject.CompareTag("Player")) return;
		//avoid going over the maximum health by forcin
		if(health + amount > _maxHealth)
		{
			amount = _maxHealth - health;
		}
			
		health += amount;

		// Notify the UI so it will change the number in the corner
		if(_ui != null
			&& _playerNumber != -1)
		{
			_ui.ChangeHealth(amount, _playerNumber);
		}

		//DEAD
		if(health <= 0)
        {
			Destroy(gameObject);
		}

        isDamageable = false;
        
        //이 오브젝트가 플레이어일 경우
        //플레이어가 데미지를 입었을 때의 데미지 처리
        if (this.gameObject.CompareTag("Player"))
        {
            SpriteRenderer[] spr = GetComponentsInChildren <SpriteRenderer>();
            StartCoroutine(FadeInOut(false, 0.01f, spr));

        }
        
        Invoke(nameof(ChangeDamageableStatus), freeTimeForDamage);
    }

    public void ChangeDamageableStatus()
    {
        isDamageable = isDamageable ? isDamageable : !isDamageable;
    }
    
    public void ChangeDamageableStatus(bool status)
    {
        isDamageable = !status ? isDamageable : !isDamageable;
    }

    IEnumerator FadeInOut(bool isFadeIn, float period, SpriteRenderer[] spr)
    {
        float status = 1f;
        //이 메소드를 사용하는 것은 플레이어 뿐이므로, 상관 없음
        Collider2D col = gameObject.GetComponent<Collider2D>();
        col.isTrigger = true;
        while (!isDamageable)
        {
            status += isFadeIn ? 0.1f : -0.1f;
            foreach (var _sprite in spr)
            {
                _sprite.color = new Color()
                {
                    r = _sprite.color.r,
                    g = _sprite.color.g,
                    b = _sprite.color.b,
                    a = status
                };
            }
            
            
            //float은 부동소수점 방식이므로, 미세한 오차가 발생하여 int로 형변환
            if ((int)status == 1)
                isFadeIn = false;
            if (status <= 0f)
                isFadeIn = true;
            yield return new WaitForSeconds(period);
        }
        
        //페이드 인 & 아웃 효과가 끝나면 투명도 초기화
        foreach (var _sprite in spr)
        {
            _sprite.color = new Color()
            {
                r = _sprite.color.r,
                g = _sprite.color.g,
                b = _sprite.color.b,
                a = 1
            };
        }
        
        col.isTrigger = false;

    }
}
