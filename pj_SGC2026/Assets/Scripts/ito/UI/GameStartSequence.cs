
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SGC2026
{
	public class GameStartSequence : MonoBehaviour
	{
		[SerializeField] private GameObject _player;
		[SerializeField] private Transform _entryPoint;
		[SerializeField] private CameraFollow _cameraFollow;
		[SerializeField] private Image _countdownImage;
		[SerializeField] private Image _startImage;

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
				_cameraFollow == null || _countdownImage == null) {
				Debug.LogError("GameStartSequence の参照が不足しています。", this);
				enabled = false;
				return;
			}

			// ★ カウントダウン中のカメラ位置を固定 
			Vector3 camPos = new Vector3(
				-15.5f,
				5f,                        // Y = 5
				_cameraFollow.transform.position.z // Z はそのまま
			);

			_cameraFollow.transform.position = camPos;

			_cameraFollow.enabled = false;
			_countdownImage.gameObject.SetActive(false);
		}

		private IEnumerator Start()
		{
			// プレイヤー初期位置セット
			Vector3 offscreenPos = _entryPoint.position + new Vector3(-17.5f, 3f, 0f);
			_player.transform.position = offscreenPos;

			// プレイヤー移動開始
			_player.GetComponent<PlayerMove>().canMove = true;

			// ★ カウントダウン開始
			_countdownImage.gameObject.SetActive(true);
			_startImage.gameObject.SetActive(false);

			_countdownImage.sprite = _three;
			yield return new WaitForSeconds(_countSeconds);

			_countdownImage.sprite = _two;
			yield return new WaitForSeconds(_countSeconds);

			_countdownImage.sprite = _one;
			yield return new WaitForSeconds(_countSeconds);

			// ★ カウントダウン終了 → START 表示
			_countdownImage.gameObject.SetActive(false);

			_startImage.sprite = _start;   // ← START の画像をセット
			_startImage.gameObject.SetActive(true);

			// カメラ追従開始
			_cameraFollow.enabled = true;

			yield return new WaitForSeconds(_startSeconds);

			// START を消す
			_startImage.gameObject.SetActive(false);
		}
		private IEnumerator StartCameraFollowDelayed(float delay)
		{
			yield return new WaitForSeconds(delay);
			_cameraFollow.enabled = true;
		}
		private IEnumerator PlayerRunIn(Vector3 from, Vector3 to)
		{
			float duration = 1.0f; // ★ 走ってくる時間
			float elapsed = 0f;

			while (elapsed < duration) {
				elapsed += Time.deltaTime;
				float t = Mathf.Clamp01(elapsed / duration);

				// ★ プレイヤーを滑らかに移動
				_player.transform.position = Vector3.Lerp(from, to, t);

				yield return null;
			}

			// 最終位置を保証
			_player.transform.position = to;
		}


		private IEnumerator AnimateCameraOffset(float from, float to)
		{
			float elapsed = 0f;

			while (elapsed < _entranceSeconds) {
				elapsed += Time.deltaTime;
				float t = Mathf.Clamp01(elapsed / _entranceSeconds);
				t = Mathf.SmoothStep(0f, 1f, t);

				Vector3 offset = _cameraFollow.offset;
				offset.x = Mathf.Lerp(from, to, t);
				_cameraFollow.SetOffset(offset);

				yield return null;
			}

			Vector3 finalOffset = _cameraFollow.offset;
			finalOffset.x = to;
			_cameraFollow.SetOffset(finalOffset);
		}
	}
}