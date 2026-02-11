using UnityEngine;
using System.Collections;

public class MoveDamageText : MonoBehaviour
{
    Vector3 pointA = new Vector3(8, -4, 0);
    Vector3 pointB = new Vector3(7.5f, -2.5f, 0);


    void Start()
    {
        StartCoroutine(MoveBetweenPoints(pointA, pointB, 0.5f));
    }

    IEnumerator MoveBetweenPoints(Vector3 start, Vector3 end, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = end; // snap exactly at end
    }
}
