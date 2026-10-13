
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// UI の基底クラス
/// </summary>
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public class UIComponent : UIBehaviour
{

	private RectTransform m_RectTransform;
	private CanvasGroup m_CanvasGroup;

	public RectTransform rectTransform
	{
		get {
			if (m_RectTransform == null) {
				m_RectTransform = this.GetComponent<RectTransform>();
			}
			return m_RectTransform;
		}
	}
	public CanvasGroup canvasGroup
	{
		get {
			if (m_CanvasGroup == null) {
				m_CanvasGroup = this.GetComponent<CanvasGroup>();
			}
			return m_CanvasGroup;
		}
	}
	public float alpha
	{
		get { return canvasGroup.alpha; }
		set { canvasGroup.alpha = value; }
	}
	public bool blockRaycasts
	{
		get { return canvasGroup.blocksRaycasts; }
		set { canvasGroup.blocksRaycasts = value; }
	}
	public bool interactable {
		get { return canvasGroup.interactable; }
		set { canvasGroup.interactable = value; }
	}

	public void SetVisible(bool _value)
	{
		alpha = _value == true ? 1f : 0f;
	}
	public void SetInteract(bool _value)
	{
		blockRaycasts = _value;
		interactable = _value;
	}

	protected override void Awake()
	{
		base.Awake();

		UIManager.Add(this);
	}
	public virtual void OnUpdate(float _dt)
	{

	}
	protected override void OnDestroy()
	{
		UIManager.Remove(this);
	}

}