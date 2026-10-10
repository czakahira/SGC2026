
using UnityEngine;
using UnityEngine.Events;
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
	[SerializeField] protected UIFader m_Fader;

	/// <summary>
	/// UI：ロード中
	/// </summary>
	[SerializeField] protected UILoading m_Loading;


	public event UnityAction<float> onUpdate;


	protected override void Init()
	{
		base.Init();

		if (m_EventSystem != null) {
			EventSystem.current = m_EventSystem;
		}

		GameManager.instance.onUpdate += OnUpdate;

		m_Fader.SetVisible(false);
		m_Loading.SetVisible(false);
	}

	protected void OnUpdate(float _dt)
	{
		onUpdate?.Invoke(_dt);
	}


	public static void LoadStart()
	{
		instance.m_Loading.Open();
	}
	public static void LoadEnd()
	{
		instance.m_Loading.Close();
	}

}
