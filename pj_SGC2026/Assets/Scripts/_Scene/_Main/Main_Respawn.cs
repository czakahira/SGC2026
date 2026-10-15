
using UnityEngine;

namespace SGC2026
{
	/// <summary>
	/// 落下判定、死亡アニメーション、初期地点への復帰を管理します。
	/// </summary>
	public partial class Main
	{
		/// <summary>
		/// 初期地点
		/// </summary>
		private Vector3 m_StartPos;
		/// <summary>
		/// 現在のチェックポイント（復活地点）
		/// </summary>
		private Vector3 m_CheckPointPosition;

		/// <summary>
		/// 落花判定のY軸位置
		/// </summary>
		public float m_FallThreshold = -5f;

		[SerializeField] private string _deathTriggerName = "Die";
		[SerializeField] private string _respawnTriggerName = "Respawn";

		[Header("背景スクロール")]
		[SerializeField] private BackgroundLoop m_BackgroundLoop;

		/// <summary>
		/// 復活先チェックポイントを変更します。
		/// </summary>
		public void SetCheckPoint(Vector3 _pos)
		{
			m_CheckPointPosition = _pos;
			Debug.Log("チェックポイント更新: " + _pos);
		}

		/// <summary>
		/// 外部からの即時復活要求。死亡演出の待機中は位置移動を禁止します。
		/// </summary>
		public void Respawn()
		{
			// プレイヤーが復活するとき、背景もゲーム開始時の位置に戻す。
			if (m_BackgroundLoop != null) {
				m_BackgroundLoop.ResetBackgroundPosition();
			}

			// 位置を戻す
			m_Player.Respawn(m_CheckPointPosition);

			SetState(EState.Respawn);
		}

	}
}