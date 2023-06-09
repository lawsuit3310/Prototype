using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] monsters;
    [SerializeField] private float spawnLimit = 100f;
    [SerializeField] private float  cooldown= 0.8f;
    public bool isSpawnable = true;
    public static Transform Target;
    public static int _monsterCount = 0;
    private static GameObject _blood;


    private void Awake()
    {
        Target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _blood = Resources.Load("Particle/BloodEffect") as GameObject;
    }

    void Start()
    {
        if (!monsters.Equals(null))
            StartCoroutine(SpawnMonster(monsters));
        else
            Debug.LogError("Cannot Find bee");
    }

    public static void Bleed(EnemyBehavior enemyBehavior)
    {
        var Instance = GameObject.Instantiate(_blood);
        Instance.transform.position = enemyBehavior.transform.position;
    }
    
    public static void Bleed(PlayerBehavior playerBehavior)
    {
        var Instance = GameObject.Instantiate(_blood);
        Instance.transform.position = playerBehavior.transform.position;
    }
     IEnumerator SpawnMonster(GameObject[] monsters)                                  
 {                                                                             
     //minCooldown is 0.1s                                                     
     cooldown = cooldown <= 0.05f ? 0.05f : cooldown;                                           
     while (!UIScript.stopGame)
     {
         if (!isSpawnable)
         {
             yield return null;
             continue;
         }
         int monsterIdx = Random.Range(0, monsters.Length);
         GameObject monster = monsters[monsterIdx];
         monster.tag = "Enemy";

         if (_monsterCount <= spawnLimit)
         {
             var Instance = Instantiate(monster, this.transform, true);
             Instance.name = Instance.name.Replace("(Clone)", "");
             Instance.GetComponent<EnemyBehavior>().gatherableItemId = monsterIdx;

             _monsterCount++;
         }                                                                     
                                                                               
         //혹시 모르니까                                                             
         else                                                                  
         {                                                                     
                                                                               
         }                                                                     
                                                                               
         yield return new WaitForSeconds(cooldown);                            
     }                                                                         
                                                                               
     yield break;                                                              
 }                                                                             
}
