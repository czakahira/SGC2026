
using UnityEngine;
using UnityEngine.Events;

namespace SGC2026
{
	/// <summary>
	/// 接触判定のハンドラ
	/// </summary>
	public class CollisionBahaviour : MonoBehaviour
	{
		public event UnityAction<ECollisionEvent, Collision> onCollision;
		public event UnityAction<ECollisionEvent, Collider> onTrigger;

		public void Clear()
		{
			onCollision = null;
			onTrigger = null;
		}

		// ------------------------------
		//  Collision
		// ------------------------------
		private void OnCollisionEnter(Collision _collision)
		{
			onCollision?.Invoke(ECollisionEvent.CollisionEnter, _collision);
		}
		private void OnCollisionStay(Collision _collision)
		{
			onCollision?.Invoke(ECollisionEvent.CollisionStay, _collision);
		}
		private void OnCollisionExit(Collision _collision)
		{
			onCollision?.Invoke(ECollisionEvent.CollisionExit, _collision);
		}
		// ------------------------------
		//  Trigger
		// ------------------------------
		private void OnTriggerEnter(Collider _collider)
		{
			onTrigger?.Invoke(ECollisionEvent.TriggerEnter, _collider);
		}
		private void OnTriggerStay(Collider _collider)
		{
			onTrigger?.Invoke(ECollisionEvent.TriggerStay, _collider);
		}
		private void OnTriggerExit(Collider _collider)
		{
			onTrigger?.Invoke(ECollisionEvent.TriggerExit, _collider);
		}
	}
}