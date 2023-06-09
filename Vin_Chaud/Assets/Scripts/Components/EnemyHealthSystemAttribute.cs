using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Attributes/Enemy Health System")]
public class EnemyHealthSystemAttribute : MonoBehaviour
{
	public int health = 3;
    private EnemyBehavior _enemyBehavior;

    private void Start()
    {
        _enemyBehavior = GetComponent<EnemyBehavior>();
    }
    
	public void ModifyHealth(int amount)
	{
        health += amount;

        if (amount < 0)
            StartCoroutine(_enemyBehavior.Damaged());

        //DEAD
		if(health <= 0)
		{
            MonsterSpawner.Bleed(_enemyBehavior);
            Destroy(gameObject);
		}
    }


}
