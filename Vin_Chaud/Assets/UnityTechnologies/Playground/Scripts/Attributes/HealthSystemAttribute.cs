using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Attributes/Health System")]
public class HealthSystemAttribute : MonoBehaviour
{
	public int health = 3;

	private UIScript ui;
    private Collider2D _collider;
    private SpriteRenderer sprite;
	private int maxHealth;

	// Will be set to 0 or 1 depending on how the GameObject is tagged
	// it's -1 if the object is not a player
	private int playerNumber;

    [SerializeField] private bool isDamageable = true;
    [SerializeField] private float freeTimeForDamage = 1f;

	private void Start()
	{
		// Find the UI in the scene and store a reference for later use
        ui = GameObject.FindObjectOfType<UIScript>();
        _collider = this.gameObject.GetComponent<BoxCollider2D>();
        Debug.Log(_collider.gameObject.name);

		// Set the player number based on the GameObject tag
		switch(gameObject.tag)
		{
			case "Player":
				playerNumber = 0;
				break;
			case "Player2":
				playerNumber = 1;
				break;
			default:
				playerNumber = -1;
				break;
		}

		// Notify the UI so it will show the right initial amount
		if(ui != null
			&& playerNumber != -1)
		{
			ui.SetHealth(health, playerNumber);
		}

		maxHealth = health; //note down the maximum health to avoid going over it when the player gets healed
	}


	// changes the energy from the player
	// also notifies the UI (if present)
	public void ModifyHealth(int amount)
    {
        if (!isDamageable) return;
		//avoid going over the maximum health by forcin
		if(health + amount > maxHealth)
		{
			amount = maxHealth - health;
		}
			
		health += amount;

		// Notify the UI so it will change the number in the corner
		if(ui != null
			&& playerNumber != -1)
		{
			ui.ChangeHealth(amount, playerNumber);
		}

		//DEAD
		if(health <= 0)
		{
			Destroy(gameObject);
		}

        isDamageable = false;
        Invoke(nameof(changeDamageableStatus), freeTimeForDamage);
    }

    private void changeDamageableStatus()
    {
        isDamageable = !isDamageable;
    }
}
