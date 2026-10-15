
using UnityEngine;
using UnityEngine.UI;

namespace SGC2026
{
	/// <summary>
	/// UI:残機表示用
	/// </summary>
	public class LifeDisplay : UIComponent
	{
		[SerializeField] private Image[] _digitImages;				// 左から右の順
		[SerializeField] private Sprite[] _digitSprites = new Sprite[10]; // 0～9

		private int _displayedLife = int.MinValue;


		public void SetLife(int _value)
		{
			int life = Mathf.Max(0, _value);
			if (life == _displayedLife) { return; }

			_displayedLife = life;

			string digits = life.ToString();

			for (int i = 0; i < _digitImages.Length; i++) {
				bool visible = i < digits.Length;
				_digitImages[i].gameObject.SetActive(visible);

				if (visible) {
					int number = digits[i] - '0';
					_digitImages[i].sprite = _digitSprites[number];
				}
			}
		}

	}
}