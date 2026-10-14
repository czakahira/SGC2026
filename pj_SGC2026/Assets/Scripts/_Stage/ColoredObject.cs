
using System;
using UnityEngine;
using UnityEngine.Events;

namespace SGC2026
{
	/// <summary>
	/// 色付きオブジェクト
	/// </summary>
	public class ColoredObject : DynamicObject
	{
		/// <summary>
		/// 色
		/// </summary>
		[SerializeField] protected EColorType m_Color;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected Collider m_Collider;

		public EColorType color => m_Color;
		public Collider collider => m_Collider;
		public event UnityAction<float, ColoredObject> onFixedUpdateObjected;


		public override void Initialize()
		{
			base.Initialize();

			m_Collider = this.GetComponent<Collider>();
		}
		public override void Dispose()
		{
			base.Dispose();

			onFixedUpdateObjected = null;
		}
		public override void OnFixedUpdate(float _dt)
		{
			base.OnFixedUpdate(_dt);

			onFixedUpdateObjected?.Invoke(_dt, this);
		}

	}

}