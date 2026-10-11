using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [SerializeField] private Transform _backgroundA;
    [SerializeField] private Transform _backgroundB;
    private SpriteRenderer _spriteA;
    private SpriteRenderer _spriteB;
    [SerializeField] private float _scrollSpeed = 5f;

    private void Start()
    {
        _spriteA = _backgroundA.GetComponentInChildren<SpriteRenderer>();
        _spriteB = _backgroundB.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        _backgroundA.position += Vector3.left * _scrollSpeed * Time.deltaTime;
        _backgroundB.position += Vector3.left * _scrollSpeed * Time.deltaTime;
        

        Camera cam = Camera.main;

        float distance = _spriteA.bounds.center.z - cam.transform.position.z;

        Vector3 leftEdge = cam.ViewportToWorldPoint(
        new Vector3(0f, 0.5f, distance));

        

        if (_spriteA.bounds.max.x < leftEdge.x)
        {
            float moveX = _spriteB.bounds.max.x - _spriteA.bounds.min.x;
            _backgroundA.position += Vector3.right * moveX;
        }

        if (_spriteB.bounds.max.x < leftEdge.x)
        {
            float moveX = _spriteA.bounds.max.x - _spriteB.bounds.min.x;
            _backgroundB.position += Vector3.right * moveX;
        }

    }
}
