using UnityEngine;

/// <summary>
/// 2枚の背景をスクロールさせ、復活時は開始位置に戻します。
/// </summary>
public class BackgroundLoop : MonoBehaviour
{
    [SerializeField] private Transform _backgroundA;
    [SerializeField] private Transform _backgroundB;

    private Vector3 _backgroundPositionA;
    private Vector3 _backgroundPositionB;

    private SpriteRenderer _spriteA;
    private SpriteRenderer _spriteB;
    [SerializeField] private float _scrollSpeed = 5f;

    // 復活直後に古いカメラ座標を参照して背景が移動するのを防ぐ。
    private int _skipWrapUntilFrame = -1;

    private void Start()
    {
        _backgroundPositionA = _backgroundA.position;
        _backgroundPositionB = _backgroundB.position;

        _spriteA = _backgroundA.GetComponentInChildren<SpriteRenderer>();
        _spriteB = _backgroundB.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        _backgroundA.position += Vector3.left * _scrollSpeed * Time.deltaTime;
        _backgroundB.position += Vector3.left * _scrollSpeed * Time.deltaTime;

        // リスポーン直後はカメラが元の位置に追従するまで1フレームだけ再配置を避ける。
        if (Time.frameCount <= _skipWrapUntilFrame)
        {
            return;
        }

        Camera cam = Camera.main;
        if (cam == null || _spriteA == null || _spriteB == null)
        {
            return;
        }

        float distance = _spriteA.bounds.center.z - cam.transform.position.z;
        Vector3 leftEdge = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, distance));

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

    /// <summary>
    /// ゲーム開始時の背景配置に戻します。
    /// </summary>
    public void ResetBackgroundPosition()
    {
        _backgroundA.position = _backgroundPositionA;
        _backgroundB.position = _backgroundPositionB;
        _skipWrapUntilFrame = Time.frameCount + 1;
    }
}
