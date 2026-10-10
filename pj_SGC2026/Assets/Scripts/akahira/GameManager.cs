
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// マネージャー：ゲーム
/// </summary>
public class GameManager : SingletonBehaviour<GameManager>
{
	/// <summary>
	/// デリゲート：更新
	/// </summary>
	public event UnityAction<float> onUpdate;

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

}
