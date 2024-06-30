using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject LoadingScreen;
    void Start(){
        Cursor.lockState = CursorLockMode.None;
    }
    public void play(){
        LoadingScreen.SetActive(true);
        MainMenu.SetActive(false);
        
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync(){
        AsyncOperation loading = SceneManager.LoadSceneAsync(1);
        while(!loading.isDone)
            yield return null;
    }
}
