
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public static class Utility_RectTransform
{
	/// <summary>
	/// アンカーを考慮したサイズを設定します(推奨)
	/// </summary>
	/// <param name="_rectTransform"></param>
	/// <param name="_size"> サイズ </param>
	public static void SetSizeWithCurrentAnchors(this RectTransform _rectTransform, Vector2 _size)
	{
		_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _size.x);
		_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _size.y);
	}

}