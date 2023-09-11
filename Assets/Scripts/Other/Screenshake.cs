using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{
    public static bool Shake;
    float _shakeDuration = .1f;
    void Update()
    {
        if(Shake)
        {
            StartCoroutine(CameraShake());
            Shake = false;
        }
    }

    IEnumerator CameraShake()
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < _shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * .02f;
            float y = Random.Range(-1f, 1f) * .02f;
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
