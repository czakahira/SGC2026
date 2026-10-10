
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

	protected CancellationTokenSource m_ApplicationQuitToken;

	/// <summary>
	/// デリゲート：更新
	/// </summary>
	public event UnityAction<float> onUpdate;

	public CancellationToken quitToken => m_ApplicationQuitToken.Token;

	protected override void Init()
	{
		base.Init();

		m_ApplicationQuitToken = new CancellationTokenSource();
	}
	protected void Update()
	{

		onUpdate?.Invoke(Time.deltaTime);

		Keyboard kb = Keyboard.current;

		// 強制停止
		if (kb.escapeKey.wasPressedThisFrame == true) {
#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif

		}
	}
	private void OnDestroy()
	{
		m_ApplicationQuitToken.Cancel();
		m_ApplicationQuitToken.Dispose();
		m_ApplicationQuitToken = null;


	}

}
