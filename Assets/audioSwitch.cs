using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audioSwitch : MonoBehaviour
{
    public AudioSource audioSource;
    public Image audioOnButton;
    public Image audioffButton;

    public void audioOn()
    {
        audioSource.Play();
        audioOnButton.enabled = false;
        audioffButton.enabled = true;
    }

    public void audioOff()
    {
        audioSource.Stop();
        audioOnButton.enabled = true;
        audioffButton.enabled = false;
    }
}
