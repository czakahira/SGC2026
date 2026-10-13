
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// マネージャー：ゲーム
/// </summary>
public class GameManager : SingletonBehaviour<GameManager>
{

	// ========================================
	//  Member
	// ========================================

	/// <summary>
	/// アプリケーション終了時に破棄されるトークン
	/// </summary>
	protected CancellationTokenSource m_ApplicationQuitToken;

	// ========================================
	//  Property
	// ========================================
	/// <summary>
	/// デリゲート：更新
	/// </summary>
	public event UnityAction<float> onUpdate;
	public event UnityAction<float> onFixedUpdate;
	public event UnityAction<float> onLateUpdate;
	public event UnityAction onDispose;
	public CancellationToken quitToken => m_ApplicationQuitToken.Token;

	// ========================================
	//  Method
	// ========================================

	protected override void OnInitialize()
	{
		base.OnInitialize();

		m_ApplicationQuitToken = new CancellationTokenSource();

	}
	protected override void OnUpdate(float _dt)
	{
		base.OnUpdate(_dt);
		onUpdate?.Invoke(_dt);
	}
	protected override void OnFixedUpdate(float _dt)
	{
		base.OnFixedUpdate(_dt);
		onFixedUpdate?.Invoke(_dt);
	}
	protected override void OnLateUpdate(float _dt)
	{
		base.OnLateUpdate(_dt);
		onLateUpdate?.Invoke(_dt);
	}
	public static void Quit()
	{
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
	protected override void OnDispose()
	{
		base.OnDispose();

		m_ApplicationQuitToken.Cancel();
		m_ApplicationQuitToken.Dispose();
		m_ApplicationQuitToken = null;

		onDispose?.Invoke();
		onDispose = null;

		onUpdate = null;
		onFixedUpdate = null;
		onLateUpdate = null;
	}


	private void Update()
	{

		// 強制停止
		Keyboard kb = Keyboard.current;
		if (kb.escapeKey.wasPressedThisFrame == true) {
			Quit();
			return;
		}

		OnUpdate(Time.deltaTime);
	}
	private void FixedUpdate()
	{
		OnFixedUpdate(Time.fixedDeltaTime);
	}
	private void LateUpdate()
	{
		OnLateUpdate(Time.deltaTime);
	}
	private void OnApplicationQuit()
	{
		Dispose();
	}
	private void OnDestroy() { }

}
