using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;
    private Image fadeImage;
    float transitionTime = 0.5f;
    int progress = 0;
    public int Progress { get { return progress; } }
    private void Awake() {
        fadeImage = fadeAnimator.GetComponent<Image>();
    }
    public void CallWithFade(UnityAction call){
        StartCoroutine(FadeCall(call));
    }
    public void LoadLevel(int sceneIndex){
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex){
        fadeImage.raycastTarget = true;
        yield return new WaitForSeconds(1.2f);
        AsyncOperation o = SceneManager.LoadSceneAsync(sceneIndex);
        o.allowSceneActivation = false;
        while(o.progress < 0.9f){
            progress = (int)Mathf.Clamp01(o.progress/ 0.9f);
            yield return null;
        }
        progress = 100;
        Debug.Log("Scene loaded!");
        fadeAnimator.SetBool("FadeIn", true);
        yield return new WaitForSeconds(transitionTime);
        o.allowSceneActivation = true;
    }

    IEnumerator FadeCall(UnityAction call){
        fadeImage.raycastTarget = true;
        fadeAnimator.SetBool("FadeIn", true);
        yield return new WaitForSeconds(transitionTime);
        call.Invoke();
        fadeAnimator.SetBool("FadeIn", false);
        yield return new WaitForSeconds(transitionTime);
        fadeImage.raycastTarget = false;
    }
}
