
using UnityEngine;

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

		public EColorType color => m_Color;


	}

}