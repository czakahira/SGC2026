
using UnityEngine;

namespace SGC2026
{
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
		/// 初期化済み
		/// </summary>
		private static bool s_Initialized;

		// ========================================
		//  Property
		// ========================================
		public static T instance => s_Instance;
		public static bool active => s_Initialized;

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
			if (s_Initialized == true) { return; }

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
			// 初期化済み
			s_Initialized = true;
			// コールバック
			(s_Instance as SingletonBehaviour<T>)?.OnInitialize();
		}
		/// <summary>
		/// 初期化を試す
		/// </summary>
		public static bool TryInitialize()
		{
			Initialize();
			return s_Initialized == true;
		}

		protected virtual void OnDispose()
		{

		}
		/// <summary>
		/// 破棄 / 停止
		/// </summary>
		public static void Dispose()
		{
			if (s_Initialized == false) { return; }

			s_Initialized = false;

			(s_Instance as SingletonBehaviour<T>)?.OnDispose();
			s_Instance = null;
		}

		protected virtual void OnUpdate(float _dt) { }
		protected virtual void OnFixedUpdate(float _dt) { }
		protected virtual void OnLateUpdate(float _dt) { }


	}
}