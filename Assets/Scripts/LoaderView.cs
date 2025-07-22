using System.Collections;
using UnityEngine;

public class LoaderView : MonoBehaviour
{
    private Coroutine _loadingCoroutine;

    private bool _canRotate;

    private void OnEnable()
    {
        _loadingCoroutine = StartCoroutine(DelayLoading());
    }

    private void OnDisable()
    {
        StopCoroutine(_loadingCoroutine);
    }

    private IEnumerator DelayLoading()
    {
        while (true)
        {
            float angle = gameObject.transform.localRotation.eulerAngles.z;

            gameObject.transform.localRotation = Quaternion.Euler(0, 0, angle + 350 * Time.deltaTime);

            yield return null;
        }
    }
}
