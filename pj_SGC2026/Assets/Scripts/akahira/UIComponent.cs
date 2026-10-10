
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public class UIComponent : UIBehaviour
{

	private RectTransform m_RectTransform;
	private CanvasGroup m_CanvasGroup;

	public RectTransform rectTransform {
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
	public bool blockRaycasts
	{
		get { return canvasGroup.blocksRaycasts; }
		set { canvasGroup.blocksRaycasts = value; }
	}
	public bool interactable {
		get { return canvasGroup.interactable; }
		set { canvasGroup.interactable = value; }
	}

	protected override void Awake()
	{
		base.Awake();

		UIManager.instance.onUpdate += EachUpdate;
	}
	protected virtual void EachUpdate(float _dt)
	{

	}

}