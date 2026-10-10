using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartSequence : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _entryPoint;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private Image _countdownImage;

    [SerializeField] private Sprite _three;
    [SerializeField] private Sprite _two;
    [SerializeField] private Sprite _one;
    [SerializeField] private Sprite _start;

    [SerializeField] private float _countSeconds = 1f;
    [SerializeField] private float _startSeconds = 0.8f;
    [SerializeField] private float _entranceSeconds = 1.2f;

    private void Awake()
    {
        if (_player == null || _entryPoint == null ||
            _cameraFollow == null || _countdownImage == null)
        {
            Debug.LogError("GameStartSequence の参照が不足しています。", this);
            enabled = false;
            return;
        }

        _player.SetActive(false);
        _cameraFollow.enabled = false;
        _countdownImage.gameObject.SetActive(false);
    }

    private IEnumerator Start()
    {
        _countdownImage.gameObject.SetActive(true);

        _countdownImage.sprite = _three;
        yield return new WaitForSeconds(_countSeconds);

        _countdownImage.sprite = _two;
        yield return new WaitForSeconds(_countSeconds);

        _countdownImage.sprite = _one;
        yield return new WaitForSeconds(_countSeconds);

        // このフレームで「すたーと」表示、プレイヤー登場、追従開始
        _countdownImage.sprite = _start;
        _player.transform.position = _entryPoint.position;

        float normalOffsetX = _cameraFollow.offset.x;
        Vector3 offset = _cameraFollow.offset;
        float entranceOffsetX =
            _cameraFollow.transform.position.x - _player.transform.position.x;
        offset.x = entranceOffsetX;
        _cameraFollow.offset = offset;

        _player.SetActive(true);
        _cameraFollow.enabled = true;
        StartCoroutine(AnimateCameraOffset(entranceOffsetX, normalOffsetX));

        yield return new WaitForSeconds(_startSeconds);
        _countdownImage.gameObject.SetActive(false);
    }

    private IEnumerator AnimateCameraOffset(float from, float to)
    {
        float elapsed = 0f;

        while (elapsed < _entranceSeconds)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _entranceSeconds);
            t = Mathf.SmoothStep(0f, 1f, t);

            Vector3 offset = _cameraFollow.offset;
            offset.x = Mathf.Lerp(from, to, t);
            _cameraFollow.offset = offset;

            yield return null;
        }

        Vector3 finalOffset = _cameraFollow.offset;
        finalOffset.x = to;
        _cameraFollow.offset = finalOffset;
    }
}