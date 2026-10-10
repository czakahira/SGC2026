
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// UI：ボタン（アニメ）
/// </summary>
public class UIButton : UIBehaviour
	, IPointerEnterHandler
	, IPointerMoveHandler
	, IPointerExitHandler
	, IPointerClickHandler
{
	protected RectTransform m_RectTransform;

	/// <summary>
	/// コールバック：矩形に入った
	/// </summary>
	public event UnityAction<PointerEventData> onEnter;
	/// <summary>
	/// コールバック：矩形に入っている
	/// </summary>
	public event UnityAction<PointerEventData> onMove;
	/// <summary>
	/// コールバック：矩形から出た
	/// </summary>
	public event UnityAction<PointerEventData> onExit;
	/// <summary>
	/// コールバック：クリック
	/// </summary>
	public event UnityAction<PointerEventData> onClick;


	public RectTransform rectTransform
	{
		get {
			if (m_RectTransform == null) {
				m_RectTransform = this.GetComponent<RectTransform>();
			}
			return m_RectTransform;
		}
	}


	protected override void Awake()
	{
		base.Awake();

		UIManager.instance.onUpdate += EachUpdate;
	}
	protected virtual void EachUpdate(float _dt)
	{

	}

	public virtual void OnPointerEnter(PointerEventData _data)
	{
		onEnter?.Invoke(_data);
	}
	public virtual void OnPointerMove(PointerEventData _data)
	{
		onMove?.Invoke(_data);
	}
	public virtual void OnPointerExit(PointerEventData _data)
	{
		onExit?.Invoke(_data);
	}
	public virtual void OnPointerClick(PointerEventData _data)
	{
		onClick?.Invoke(_data);
	}

}
