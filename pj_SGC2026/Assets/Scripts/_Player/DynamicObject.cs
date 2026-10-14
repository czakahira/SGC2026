
using UnityEngine;
using UnityEngine.Events;

namespace SGC2026
{
	public enum ECollisionEvent
	{
		None = -1,

		CollisionEnter,
		CollisionStay,
		CollisionExit,

		TriggerEnter,
		TriggerStay,
		TriggerExit,

	}

	/// <summary>
	/// 動体オブジェクト
	/// </summary>
	public class DynamicObject : MonoBehaviour
	{

		public event UnityAction<ECollisionEvent, DynamicObject, Collision> onCollision;
		public event UnityAction<ECollisionEvent, DynamicObject, Collider> onTrigger;

		public virtual void Initialize()
		{

		}
		public virtual void Dispose()
		{
			onCollision = null;
		}
		public virtual void OnUpdate(float _dt)
		{

		}
		public virtual void OnFixedUpdate(float _dt)
		{

		}
		public virtual void OnLateUpdate(float _dt)
		{

		}

		private void OnCollisionEnter(Collision _collision)
		{
			onCollision?.Invoke(ECollisionEvent.CollisionEnter, this, _collision);
		}
		private void OnTriggerEnter(Collider _collider)
		{
			onTrigger?.Invoke(ECollisionEvent.TriggerEnter, this, _collider);
		}

	}
}