using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bee;
    [SerializeField] private GameObject[] monsters;
    [SerializeField] private float spawnLimit = 100f;
    [SerializeField] private float  cooldown= 0.8f;

    void Start()
    {
        if (!bee.Equals(null))
            StartCoroutine(spawnMonster(monsters));
        else
            Debug.LogError("Cannot Find bee");
    }

    IEnumerator spawnMonster(GameObject monster)
    {
        //minCooldown is 0.1s
        cooldown = cooldown <= 0.1f ? 0.1f : cooldown; 
        monster.tag = "Enemy";
        while (!UIScript.stopGame)
        {
            int monsterCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (monsterCount <= spawnLimit)
            {
                Instantiate(monster);
            }
            
            //혹시 모르니까
            else
            {
                
            }

            yield return new WaitForSeconds(cooldown);
        }

        yield break;
    }
     IEnumerator spawnMonster(GameObject[] monsters)                                  
 {                                                                             
     //minCooldown is 0.1s                                                     
     cooldown = cooldown <= 0.1f ? 0.1f : cooldown;                                           
     while (!UIScript.stopGame)                                                
     {                                                                         
         int monsterCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
         int monsterIdx = Random.Range(0, monsters.Length);
         GameObject monster = monsters[monsterIdx];        
         if (monster.IsUnityNull())                        
             monster = bee;                                
         monster.tag = "Enemy";
         if (monsterCount <= spawnLimit)
         {
             var v = Instantiate(monster, GameObject.Find("Dangers").transform, true);
             v.name = v.name.Replace("(Clone)", "");
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
