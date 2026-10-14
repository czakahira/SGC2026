
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// マネージャー：UI
/// </summary>
public class UIManager : SystemSingletonBehaviour<UIManager>
{
	// ========================================
	//  Member
	// ========================================

	/// <summary>
	/// 
	/// </summary>
	[SerializeField] protected EventSystem m_EventSystem;

	/// <summary>
	/// 操作ガイド
	/// </summary>
	protected UIGuide m_Guide;
	/// <summary>
	/// フェーダー
	/// </summary>
	[SerializeField] protected UIFader m_Fader;
	/// <summary>
	/// UI：ロード中
	/// </summary>
	[SerializeField] protected UILoading m_Loading;
	/// <summary>
	/// 
	/// </summary>
	protected static List<UIComponent> s_UIs;
	/// <summary>
	/// 
	/// </summary>
	protected static event UnityAction<float> s_OnUpdate;

	// ========================================
	//  Property
	// ========================================
	public UIGuide guide => m_Guide;
	public UIFader fader => m_Fader;
	public UILoading loading => m_Loading;



	// ========================================
	//  Method
	// ========================================
	protected override void OnInitialize()
	{
		base.OnInitialize();

		s_UIs = new List<UIComponent>(100);

		Canvas canvas = Canvas.Instantiate(Resources.Load<Canvas>("UI/Canvas"));
		canvas.transform.SetParent(this.transform);

		m_Guide = this.GetComponentInChildren<UIGuide>();
		m_Fader = this.GetComponentInChildren<UIFader>();
		m_Loading = this.GetComponentInChildren<UILoading>();

		m_EventSystem = EventSystem.Instantiate(Resources.Load<EventSystem>("UI/EventSystem"));
		m_EventSystem.transform.SetParent(this.transform);
		EventSystem.current = m_EventSystem;

		m_Guide.ForceClose();
		m_Fader.SetVisible(false);
		m_Fader.SetInteract(false);
		m_Loading.SetVisible(false);
		m_Loading.SetInteract(false);
	}
	protected override void OnDispose()
	{
		base.OnDispose();

		foreach (var ui in s_UIs) {
			
		}
		s_UIs.Clear();
		s_UIs = null;
	}

	protected override void OnUpdate(float _dt)
	{
		s_OnUpdate?.Invoke(_dt);
	}

	public static bool Add(UIComponent _ui)
	{
		if (active == false) { return false; }
		if (s_UIs.Contains(_ui) == true) { return false; }

		s_UIs.Add(_ui);
		s_OnUpdate += _ui.OnUpdate;
		return true;
	}
	public static bool Remove(UIComponent _ui)
	{
		if (active == false) { return false; }
		if (s_UIs.Contains(_ui) == false) { return false; }

		s_OnUpdate -= _ui.OnUpdate;
		s_UIs.Remove(_ui);
		return true;
	}

	public static void LoadStart()
	{
		instance.m_Loading.Open();
	}
	public static void LoadEnd()
	{
		instance.m_Loading.Close();
	}


	public static void FadeOut(float _duration = 0.5f)
	{
		instance.fader.FadeOut(_duration);
	}
	public static void FadeIn(float _duration = 0.5f)
	{
		instance.fader.FadeIn(_duration);
	}


	public static void SetGuide(int _idx)
	{
		instance.guide.SetGuide(_idx);
	}

}
