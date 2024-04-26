using UnityEngine.UI;
using Microsoft.Unity.VisualStudio.Editor;

using UnityEngine;
using Unity.VisualScripting;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField]private int NumberOfWeapons;
    private int currentWeaponIndex;
    [SerializeField]private UnityEngine.UI.Image currentGunUi;

    [SerializeField]private Sprite[] gunSilloetes;

    void Start()
    {
        Debug.Log(transform.childCount);
        NumberOfWeapons = transform.childCount;
        currentWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            transform.GetChild(currentWeaponIndex).gameObject.SetActive(false);
            currentWeaponIndex = 0;
            transform.GetChild(0).gameObject.SetActive(true);
            currentGunUi.sprite = gunSilloetes[0];
        }
        else if(Input.GetKey(KeyCode.Alpha2))
        {
            transform.GetChild(currentWeaponIndex).gameObject.SetActive(false);
            currentWeaponIndex = 1;
            transform.GetChild(1).gameObject.SetActive(true);
            currentGunUi.sprite = gunSilloetes[1];
        }

        // else if(Input.GetKey(KeyCode.Alpha3))
        // {
        //     transform.GetChild(currentWeaponIndex).gameObject.SetActive(false);
        //     currentWeaponIndex = 2;
        //     transform.GetChild(2).gameObject.SetActive(true);
        // }
        
    }
}
