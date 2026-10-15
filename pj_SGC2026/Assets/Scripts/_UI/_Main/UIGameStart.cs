
using UnityEngine;
using UnityEngine.UI;

namespace SGC2026
{
	/// <summary>
	/// UI：ゲームスタート
	/// </summary>
	public class UIGameStart : UIComponent
	{
		/// <summary>
		/// Start + １～３
		/// </summary>
		[SerializeField] protected GameObject[] m_Images;

		/// <summary>
		/// 表示物変更
		/// </summary>
		public void SetImage(int _idx)
		{
			for (int i = 0; i < m_Images.Length; i++) {
				m_Images[i].SetActive( i == _idx );
			}
		}

	}
}