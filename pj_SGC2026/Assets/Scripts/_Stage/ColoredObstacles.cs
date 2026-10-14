
using UnityEngine;
using UnityEngine.Events;

namespace SGC2026
{
	/// <summary>
	/// 色：障害物
	/// </summary>
	public class ColoredObstacles : ColoredObject
	{
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected CollisionBahaviour m_ScoreTrigger;

		/// <summary>
		/// 
		/// </summary>
		public event UnityAction<ECollisionEvent, ColoredObject, Collision> onCollisionColored;
		/// <summary>
		/// ObstacleEnd
		/// </summary>
		public event UnityAction<ECollisionEvent, ColoredObject, Collider> onTriggerScore;

		public override void Initialize()
		{
			base.Initialize();

			collision.onCollision += OnCollision_Colored;
			m_ScoreTrigger.onTrigger += OnTrigger_Score;
		}
		public override void Dispose()
		{
			base.Dispose();

			collision.onCollision -= OnCollision_Colored;
			m_ScoreTrigger.onTrigger -= OnTrigger_Score;
		}

#if false
		private void Update()
		{
			var player = PlayerColor.instance;
			if (player == null) return;

			var playerCol = player.GetComponent<Collider>();
			if (playerCol == null) return;

			bool sameColor = (player.currentColor == obstacleColor);

			// 壁本体との衝突だけを無効化
			Physics.IgnoreCollision(playerCol, obstacleCol, sameColor);
		}

		private void OnCollisionEnter(Collision collision)
		{
			var player = collision.gameObject.GetComponent<PlayerColor>();
			if (player == null) return;

			if (player.currentColor != obstacleColor) {
				PlayerLife.instance.ReduceLife();

				var respawn = collision.gameObject.GetComponent<PlayerRespawn>();
				if (respawn != null)
					respawn.Respawn();
			}
		}
#endif
		protected void OnCollision_Colored(ECollisionEvent _event, Collision _other)
		{
			onCollisionColored?.Invoke(_event, this, _other);
		}
		protected void OnTrigger_Score(ECollisionEvent _event, Collider _other)
		{
			onTriggerScore?.Invoke(_event, this, _other);
		}

	}

}