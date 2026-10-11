using UnityEngine;

[DefaultExecutionOrder(100)]
public class CameraStartBoundary : MonoBehaviour
{
    private float _startX;

    private void Awake()
    {
        _startX = transform.position.x;
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Max(_startX, position.x);
        transform.position = position;
    }
}