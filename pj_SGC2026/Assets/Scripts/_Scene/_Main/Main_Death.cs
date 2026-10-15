
using System.Collections;
using UnityEngine;

namespace SGC2026
{
	/// <summary>
	/// 残機と死亡演出から復活（またはゲームオーバー）までの時間を管理します。
	/// </summary>
	public partial class Main
	{
		/// <summary>
		/// 残機
		/// </summary>
		public int life = 3;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected LifeDisplay m_lifeDisplay;

		/// <summary>
		/// 死亡演出用タイマー
		/// </summary>
		protected SimpleTimer m_DeathTimer;

		[Header("やられ演出の待機時間（秒）")]
		[Min(0f)]
		[SerializeField] private float _deathDelaySeconds = 2f;

		[Header("ScoreImageView")]
		public ScoreImageView scoreImageView;

		/// <summary>
		/// 死亡演出中かどうか。多重に残機を減らさないためにも使用します。
		/// </summary>
		public bool IsProcessingDeath { get; private set; }

		/// <summary>
		/// 落下４チェック
		/// </summary>
		protected void Check_FallDeath()
		{
			if (m_Player.transform.position.y < m_FallThreshold) {
				// ここでは位置を変更しない。演出と待機の開始だけ。
				StartDeath();
			}
		}
		/// <summary>
		/// 残機が残っているか
		/// </summary>
		/// <returns></returns>
		protected bool IsAlive()
		{
			return life > 0;
		}
		protected void SetLife(int _value)
		{
			life = _value;
			m_lifeDisplay.SetLife(life);
		}

		/// <summary>
		/// 死亡演出開始
		/// <para>
		/// 残機を1減らし、死亡演出→2秒待機→復活／ゲームオーバーを開始します。
		/// ダメージ側からはこのメソッドを呼び、Respawn() を続けて呼ばないでください。
		/// </para>
		/// </summary>
		public void StartDeath()
		{
			IsProcessingDeath = true;

			// 残機減らし
			SetLife(--life);
//			StartCoroutine(DeathAndRespawnRoutine());

			// タイマー設定
			m_DeathTimer.duration = _deathDelaySeconds;
			m_DeathTimer.Reset();

			// アニメーション
			m_Player.SetState(PlayerController.EState.Death);

			//
			SetState(EState.Death);

			Debug.Log($"[PlayerLife] 残機: {life} / やられ開始: {Time.realtimeSinceStartup:F2}秒");
		}

		/// <summary>
		/// ゲームオーバーへ
		/// </summary>
		private void GameOver()
		{
			m_CameraFollow.SetTarget(null);

			scoreImageView.UpdateScore(ScoreManager.instance.score);

			m_Result.ShowGameOver();

			SetState(EState.Result);
		}
		/// <summary>
		/// クリア
		/// </summary>
		private void GameClear()
		{
			// ★ カメラ追従停止
			m_CameraFollow.SetTarget(null);
			// スコア
			scoreImageView.UpdateScore(ScoreManager.instance.score);
			// ★ リザルトパネル表示（クリア用画像をON）
			m_Result.ShowClear();
			// 
			SetState(EState.Result);
		}

	}
}