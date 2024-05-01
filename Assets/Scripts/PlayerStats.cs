using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [SerializeField]public int health;
    [SerializeField]private GameObject healthBar;
    [SerializeField]private GameObject healthText;
    [SerializeField]public bool mainDoorKey;
    [SerializeField]public bool basementDoorKey;

    [SerializeField]private TMP_Text prompt;
    public bool isInverted;
    
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
        healthBar.GetComponent<Slider>().value = health;
        

        if(health-damage <= 0){
            SceneManager.LoadScene("UI");
        }
    }

    public void pickUpKey(int keyId){
        Debug.Log("tried to pick up the key");
        if(keyId == 0)
        {
            mainDoorKey = true;
            prompt.text = "You have obtained the main door key";
        }
            
        else{
            basementDoorKey = true;
            prompt.text = "You have obtained the basement door key";
        }
            
        
        Invoke("removePrompt",2f);
    }



    void removePrompt(){
        prompt.text = "";
    }
    // Update is called once per frame
    public void updateHealth(){
        healthText.GetComponent<TMP_Text>().text = health.ToString();
        healthBar.GetComponent<Slider>().value = health;
    }
}
