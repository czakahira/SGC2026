
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
	[RequireComponent(typeof(CollisionBahaviour))]
	public class DynamicObject : MonoBehaviour
	{
		[SerializeField] protected CollisionBahaviour m_Collision;

		public CollisionBahaviour collision => m_Collision;
		

		public virtual void Initialize()
		{
			m_Collision = this.GetComponent<CollisionBahaviour>();
		}
		public virtual void Dispose()
		{
			m_Collision.Clear();
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

		

	}
}