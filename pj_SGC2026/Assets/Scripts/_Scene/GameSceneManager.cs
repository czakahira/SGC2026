
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

namespace SGC2026
{
	/// <summary>
	/// マネージャー：シーン
	/// </summary>
	public class GameSceneManager : SystemSingletonBehaviour<GameSceneManager>
	{
		/// <summary>
		/// 入室済みのシーン一覧
		/// </summary>
		private static List<GameSceneHandle> s_Scenes;

		static GameSceneManager()
		{
			s_Scenes = new List<GameSceneHandle>(10);
		}

		protected override void OnUpdate(float _dt)
		{
			base.OnUpdate(_dt);

			if (s_Scenes != null) {
				if (s_Scenes != null) {
					foreach (var hdl in s_Scenes) {
						hdl.Update(_dt);
					}
				}
			}

		}
		protected override void OnFixedUpdate(float _dt)
		{
			base.OnFixedUpdate(_dt);

			if (s_Scenes != null) {
				if (s_Scenes != null) {
					foreach (var hdl in s_Scenes) {
						hdl.FixedUpdate(_dt);
					}
				}
			}
		}
		protected override void OnLateUpdate(float _dt)
		{
			base.OnLateUpdate(_dt);

			if (s_Scenes != null) {
				if (s_Scenes != null) {
					foreach (var hdl in s_Scenes) {
						hdl.LateUpdate(_dt);
					}
				}
			}
		}


		private static bool Add(GameSceneHandle _scene)
		{
			if (s_Scenes.Contains(_scene) == true) { return false; }

			s_Scenes.Add(_scene);
			return true;
		}
		private static bool Remove(GameSceneHandle _scene)
		{
			if (s_Scenes.Contains(_scene) == false) { return false; }

			s_Scenes.Remove(_scene);
			return true;
		}
		private static GameSceneHandle GetHandle(Scene _scene)
		{
			foreach (var hdl in s_Scenes) {
				if (hdl.scene == _scene) {
					return hdl;
				}
			}
			return default;
		}
		private static GameSceneHandle GetHandle(string _name)
		{
			Scene scene = SceneManager.GetSceneByName(_name);
			return GetHandle(_name);
		}
		public static T GetScene<T>(Scene _scene) where T : GameScene
		{
			GameSceneHandle handle = GetHandle(_scene);
			if (string.IsNullOrEmpty(handle.scene.name) == false) {
				foreach (var scene in handle.scenes) {
					T target = (scene as T);
					if (target == null) { continue; }

					return target;
				}
			}
			return null;
		}
		public static T GetScene<T>(string _name) where T : GameScene
		{
			return GetScene<T>(SceneManager.GetSceneByName(_name));
		}
		private static bool IsHandling(Scene _scene)
		{
			foreach (var hdl in s_Scenes) {
				if (hdl.scene == _scene) {
					return true;
				}
			}
			return false;
		}

		public static void Enter(Scene _scene)
		{
			if (IsHandling(_scene) == true) { return; }

			GameSceneHandle handle = new GameSceneHandle(_scene);
			Add(handle);
			handle.EnterHandlingScene();
		}
		public static void Exit(Scene _scene)
		{
			if (IsHandling(_scene) == false) { return; }

			GameSceneHandle handle = GetHandle(_scene);
			handle.ExitHandlingScene();
			Remove(handle);
			handle.Dispose();
		}

		/// <summary>
		/// シーンを動機ロード
		/// </summary>
		/// <param name="_sceneName"></param>
		public void Load(string _sceneName)
		{
			SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
		}

		/// <summary>
		/// 
		/// </summary>
		public async UniTask LoadAsync(string _sceneName, LoadSceneMode _loadSceneMode = LoadSceneMode.Single, UnityAction<Scene> _onLoad = null)
		{
			LoadSceneParameters param = default;
			param.loadSceneMode = _loadSceneMode;

			// 全てのシーンから退室
			if (_loadSceneMode == LoadSceneMode.Single) {
				foreach (var hdl in s_Scenes) {
					hdl.ExitHandlingScene();
				}
				s_Scenes.Clear();
			}

			// ロード
			await SceneManager.LoadSceneAsync(_sceneName, param);

			// 入室
			Scene loadedScene = SceneManager.GetSceneByName(_sceneName);
			GameSceneManager.Enter(loadedScene);
			// 完了
			_onLoad?.Invoke(loadedScene);
		}

	}
}