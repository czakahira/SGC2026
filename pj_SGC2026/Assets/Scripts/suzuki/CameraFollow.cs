
using UnityEngine;

namespace SGC2026
{
	/// <summary>
	/// カメラ追従
	/// </summary>
	public class CameraFollow : MonoBehaviour
	{
		/// <summary>
		/// 追従ターゲット
		/// </summary>
		[SerializeField] protected Transform m_Target;
		[SerializeField] protected Vector3 m_Offset;
		private float fixedZ;

		public Vector3 offset => m_Offset;

		public void SetTarget(Transform _target)
		{
			m_Target = _target;
		}
		public void SetOffset(Vector3 _value)
		{
			m_Offset = _value;
		}

		public void Initialize()
		{
			fixedZ = transform.position.z; // Zだけ固定
		}

		public void UpdateFollow()
		{
			if (m_Target == null) { return; }

			// 追従
			transform.position = new Vector3(
				m_Target.position.x + m_Offset.x,
				m_Offset.y,
				fixedZ
			);
		}
	}


}