using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [SerializeField]private int health;
    [SerializeField]private GameObject healthBar;
    [SerializeField]private GameObject healthText;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        
    }

    public void takeDamage(int damage){
        if(health > 0){
            health-=damage;
        }
        healthText.GetComponent<TMP_Text>().text = health.ToString();
        healthBar.GetComponent<Slider> ().value = health;
    }
    // Update is called once per frame
    void Update()
    {
        takeDamage(1);
    }
}
