using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] LoadSceneMode mode;
    [SerializeField] bool async;
    [SerializeField] float minLoadTime;
    [SerializeField] bool id;
    [SerializeField] string sceneName;
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onEnd;

    public void Load()
    {
        if (async)
        {
            StartCoroutine(AsyncLoadScene());
        }
        else
        {
            onStart.Invoke();
            if (id)
                SceneManager.LoadScene(int.Parse(sceneName), mode);
            else
                SceneManager.LoadScene(sceneName, mode);
            onEnd.Invoke();
        }
    }

    private IEnumerator AsyncLoadScene()
    {
        onStart.Invoke();
        AsyncOperation load;
        if (id)
            load = SceneManager.LoadSceneAsync(int.Parse(sceneName), mode);
        else
            load = SceneManager.LoadSceneAsync(sceneName, mode);
        load.allowSceneActivation = false;
        float t = 0;
        while (t < minLoadTime || load.progress < .9f)
        {
            t += Time.deltaTime;
            yield return null;
        }
        load.allowSceneActivation = true;
        onEnd.Invoke();
    }
}
