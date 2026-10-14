
using System.Collections;
using UnityEngine;

namespace SGC2026
{
	/// <summary>
	/// 残機と死亡演出から復活（またはゲームオーバー）までの時間を管理します。
	/// </summary>
	public class PlayerLife : MonoBehaviour
	{
		public static PlayerLife instance;
		public int life = 3;

		[Header("やられ演出の待機時間（秒）")]
		[Min(0f)]
		[SerializeField] private float _deathDelaySeconds = 2f;

		[Header("リザルトパネル")]
		public ResultPanel resultPanel;

		[Header("プレイヤーのリスポーン管理")]
		public PlayerRespawn playerRespawn;

		[Header("カメラ追従スクリプト")]
		public CameraFollow cameraFollow;

		[Header("ScoreImageView")]
		public ScoreImageView scoreImageView;

		/// <summary>
		/// 死亡演出中かどうか。多重に残機を減らさないためにも使用します。
		/// </summary>
		public bool IsProcessingDeath { get; private set; }

		private void Awake()
		{
			if (instance == null) {
				instance = this;
			}
			else {
				Destroy(gameObject);
			}
		}

		/// <summary>
		/// 残機を1減らし、死亡演出→2秒待機→復活／ゲームオーバーを開始します。
		/// ダメージ側からはこのメソッドを呼び、Respawn() を続けて呼ばないでください。
		/// </summary>
		public void ReduceLife()
		{
			if (IsProcessingDeath || life <= 0) {
				return;
			}

			IsProcessingDeath = true;
			life--;
			Debug.Log($"[PlayerLife] 残機: {life} / やられ開始: {Time.realtimeSinceStartup:F2}秒");
			StartCoroutine(DeathAndRespawnRoutine());
		}

		private IEnumerator DeathAndRespawnRoutine()
		{
			// Inspector未設定でも、シーンに存在するプレイヤーを可能なら取得する。
			PlayerRespawn respawn = playerRespawn != null ? playerRespawn : PlayerRespawn.instance;
			if (respawn != null) {
				respawn.PlayDeathAnimation();
			}
			else {
				Debug.LogWarning("[PlayerLife] PlayerRespawn が設定されていないため死亡アニメーションを再生できません。");
			}

			// Time.timeScale に影響されず、現実時間で指定秒数待つ。
			yield return new WaitForSecondsRealtime(_deathDelaySeconds);

			Debug.Log($"[PlayerLife] 待機終了: {Time.realtimeSinceStartup:F2}秒");

			if (life <= 0) {
				GameOver();
			}
			else if (respawn != null) {
				// ここだけが、死亡演出後の実際の位置移動を実行する。
				respawn.CompleteRespawnAfterDeath();
			}

			IsProcessingDeath = false;
		}

		private void GameOver()
		{
			Debug.Log("ゲームオーバー");

			if (cameraFollow != null) {
			}
			cameraFollow.SetTarget(null);

			if (playerRespawn != null) {
				playerRespawn.enabled = false;
			}

			scoreImageView.UpdateScore(ScoreManager.instance.score);
			resultPanel.ShowGameOver();
		}

	}
}