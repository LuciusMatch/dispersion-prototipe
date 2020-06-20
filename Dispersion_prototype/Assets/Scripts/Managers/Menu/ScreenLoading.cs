using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenLoading : MonoBehaviour
{
    [SerializeField]
    private Image progressbar;

    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gamelevel = SceneManager.LoadSceneAsync(2);
        while (gamelevel.progress < 1)
        {
            progressbar.fillAmount = Mathf.Clamp01(gamelevel.progress / .9f);
            yield return new WaitForEndOfFrame();
        }
    }
}
