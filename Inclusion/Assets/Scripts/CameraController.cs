using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 gamePos;

    private void Awake()
    {
        gamePos = new Vector3(9, 1, 3);
    }

    public void StartGame()
    {
        transform.position = new Vector3(15, 1, 3);
        StartCoroutine(MoveCamera(2f));
    }

    private IEnumerator MoveCamera(float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, gamePos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = gamePos;
    }
}
