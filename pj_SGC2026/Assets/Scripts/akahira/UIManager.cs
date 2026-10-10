
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// マネージャー：UI
/// </summary>
public class UIManager : SingletonBehaviour<UIManager>
{
	/// <summary>
	/// 
	/// </summary>
	[SerializeField] protected EventSystem m_EventSystem;

	/// <summary>
	/// フェーダー
	/// </summary>
	[SerializeField] protected CanvasGroup m_Fader;


	protected override void Init()
	{
		base.Init();

		if (m_EventSystem != null) {
			EventSystem.current = m_EventSystem;
		}

		GameManager.instance.onUpdate += OnUpdate;
	}

	protected void OnUpdate(float _dt)
	{

	}

}
