using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    private AsyncOperation _operation;

    private void OnEnable()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForEndOfFrame();

        _operation = SceneManager.LoadSceneAsync(AppScenes.GAME_SCENE, LoadSceneMode.Single);
        _operation.allowSceneActivation = false;

        while(!(_operation.progress >= 0.9f) || !Input.GetKeyDown(KeyCode.Space))
        {
            loadingSlider.value = _operation.progress;

            Debug.Log(_operation.progress.ToString("0.000"));
            yield return null;
        }

        FinishLoading();
    }

    private void FinishLoading()
    {
        _operation.allowSceneActivation = true;
    }

}
