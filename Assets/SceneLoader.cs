using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public TextMeshProUGUI counterText; // Assign this in the inspector
    public GameObject LoadingScreen; // Assign this in the inspector
    public GameObject MainMenu; // Assign this in the inspector
    public Image playButton;
    public Image exitButton;

    public void SceneLoaderMethod(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/TestEnv");
    }

    public void disableMainMenu(){
        MainMenu.SetActive(false);
        playButton.enabled = false;
        exitButton.enabled = false;   
    }


    public void StartCounter()
    {
        StartCoroutine(CounterCoroutine());
    }

    private IEnumerator CounterCoroutine()
    {
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Scenes/TestEnv");
        LoadingScreen.SetActive(true);
        MainMenu.SetActive(false);
     

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log("Loading progress: " + progress);
            counterText.text = "Loading... " + progress * 100f + "%";
            yield return null;
        }
    }
}