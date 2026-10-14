
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// UI：ボタン（アニメ）
/// </summary>
public class UIButton : UIComponent
	, IPointerEnterHandler
	, IPointerMoveHandler
	, IPointerExitHandler
	, IPointerClickHandler
{
	

	/// <summary>
	/// コールバック：矩形に入った
	/// </summary>
	public event UnityAction<UIBehaviour, PointerEventData> onEnter;
	/// <summary>
	/// コールバック：矩形に入っている
	/// </summary>
	public event UnityAction<UIBehaviour, PointerEventData> onMove;
	/// <summary>
	/// コールバック：矩形から出た
	/// </summary>
	public event UnityAction<UIBehaviour, PointerEventData> onExit;
	/// <summary>
	/// コールバック：クリック
	/// </summary>
	public event UnityAction<UIBehaviour, PointerEventData> onClick;


	public virtual void OnPointerEnter(PointerEventData _data)
	{
		onEnter?.Invoke(this, _data);
	}
	public virtual void OnPointerMove(PointerEventData _data)
	{
		onMove?.Invoke(this, _data);
	}
	public virtual void OnPointerExit(PointerEventData _data)
	{
		onExit?.Invoke(this, _data);
	}
	public virtual void OnPointerClick(PointerEventData _data)
	{
		onClick?.Invoke(this, _data);
	}

}
