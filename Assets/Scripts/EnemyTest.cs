using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{

    [SerializeField] float health;

    // Start is called before the first frame update
    void Start()
    {
        health = 20;    
    }

    
    public void takeDamage(float damage){
        health-=damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
