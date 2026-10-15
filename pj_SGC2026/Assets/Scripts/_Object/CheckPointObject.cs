
using UnityEngine;
using UnityEngine.Events;

namespace SGC2026
{
	/// <summary>
	/// オブジェクト：チェックポイント
	/// </summary>
	public class CheckPointObject : DynamicObject
	{
		/// <summary>
		/// // ★ 好きな高さに調整できる
		/// </summary>
		[SerializeField] private float m_RespawnHeightOffset = 3.0f; 

		public float respawnHeightOffset
		{
			get { return m_RespawnHeightOffset; }
			set { m_RespawnHeightOffset = value; }
		}

		public UnityAction<CheckPointObject, Collider> onCheckPoint;

		public override void Initialize()
		{
			base.Initialize();

			collision.onTrigger += OnTriggerEnter_CheckPoint;
		}

		protected void OnTriggerEnter_CheckPoint(ECollisionEvent _event, Collider _other)
		{
			if (_event != ECollisionEvent.TriggerEnter) { return; }

			onCheckPoint?.Invoke(this, _other);
		}

	}
}