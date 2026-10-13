
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
///  
/// </summary>
public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	// ========================================
	//  Member
	// ========================================

	/// <summary>
	/// 単一インスタンス
	/// </summary>
	private static T s_Instance;
	/// <summary>
	/// 
	/// </summary>
	private static bool s_IsActive;

	// ========================================
	//  Property
	// ========================================
	public static T instance => s_Instance;
	public static bool active => s_IsActive;

	// ========================================
	//  Method
	// ========================================

	/// <summary>
	/// 初期化
	/// </summary>
	protected virtual void OnInitialize()
	{

	}
	/// <summary>
	/// インスタンスを用意する
	/// </summary>
	public static void Initialize()
	{
		// 既に起動済み
		if (s_IsActive == true) { return; }

		// 最初は空のはず
		s_Instance = GameObject.FindAnyObjectByType<T>();
		string name = typeof(T).Name;
		GameObject obj = GameObject.Find(name);
		if (s_Instance == null) {
			if (obj == null) {
				obj = new GameObject();
				s_Instance = obj.AddComponent<T>();
			}
		}
		// 名前変更
		obj.name = name;
		// 常駐
		DontDestroyOnLoad(s_Instance.gameObject);
		//
		s_IsActive = true;
		// 
		(s_Instance as SingletonBehaviour<T>)?.OnInitialize();
	}
	/// <summary>
	/// 
	/// </summary>
	public static bool TryInitialize()
	{
		Initialize();
		return s_IsActive == true;
	}

	protected virtual void OnDispose()
	{

	}
	public static void Dispose()
	{
		if (s_IsActive == false) { return; }

		s_IsActive = false;

		(s_Instance as SingletonBehaviour<T>)?.OnDispose();
		s_Instance = null;
	}

	protected virtual void OnUpdate(float _dt) { }
	protected virtual void OnFixedUpdate(float _dt) { }
	protected virtual void OnLateUpdate(float _dt) { }

}
