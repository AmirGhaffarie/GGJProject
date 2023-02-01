using System;
using System.Collections;
using UnityEngine;

public class ActionAfter : MonoBehaviour
{
    public void SetupAction(float delay, Action action, bool destroyAfter = false)
    {
        StartCoroutine(SetupRoutine(delay, action, destroyAfter));
    }

    IEnumerator SetupRoutine(float delay, Action action, bool destroyAfter = false)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
        if (destroyAfter)
            Destroy(gameObject);
    }
}
