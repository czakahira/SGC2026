
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	// ========================================
	//  Member
	// ========================================

	/// <summary>
	/// 単一インスタンス
	/// </summary>
	private static T s_Instance;

	// ========================================
	//  Property
	// ========================================
	public static T instance
	{
		get { return InitInstance(); }
	}

	public static bool isActive => instance != null;

	// ========================================
	//  Method
	// ========================================

	/// <summary>
	/// インスタンスを用意する
	/// </summary>
	public static T InitInstance()
	{
		// 最初は空のはず
		if (s_Instance == null) {

			s_Instance = GameObject.FindAnyObjectByType<T>();
			string name = typeof(T).Name;
			GameObject obj = GameObject.Find(name);
			if (s_Instance == null) {
				if (obj == null) {
					obj = new GameObject(name);
					s_Instance = obj.AddComponent<T>();
				}
			}
			// 名前変更
			obj.name = name;
			// 常駐
			DontDestroyOnLoad(s_Instance.gameObject);
			// 初期化
			(s_Instance as SingletonBehaviour<T>).Init();
		}
		return s_Instance;
	}

	protected virtual void Init()
	{

	}

}
