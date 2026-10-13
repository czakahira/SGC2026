
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンのハンドル
/// </summary>
public struct GameSceneHandle
{
	// ========================================
	//  Member
	// ========================================
	private Scene m_Scene;
	private List<GameScene> m_Scenes;

	// ========================================
	//  Property
	// ========================================
	public string name => m_Scene.name;
	public Scene scene => m_Scene;
	public IReadOnlyCollection<GameScene> scenes => m_Scenes;


	public GameSceneHandle(Scene _scene)
	{
		m_Scene = _scene;

		m_Scenes = new List<GameScene>(4);

		// 登録
		var roots = _scene.GetRootGameObjects();
		foreach (var root in roots) {
			GameScene[] scenes = root.GetComponentsInChildren<GameScene>();
			foreach (var scene in scenes) {
				Add(scene);
			}
		}
	}
	public void Dispose()
	{
		m_Scenes.Clear();
		m_Scenes = null;

		m_Scene = default;
	}

	// ========================================
	//  Method
	// ========================================

	public void Update(float _dt)
	{
		if (m_Scenes != null) {
			foreach (var scene in m_Scenes) {
				scene.OnUpdate(_dt);
			}
		}
	}
	public void FixedUpdate(float _dt)
	{
		if (m_Scenes != null) {
			foreach (var scene in m_Scenes) {
				scene.OnFixedUpdate(_dt);
			}
		}
	}
	public void LateUpdate(float _dt)
	{
		if (m_Scenes != null) {
			foreach (var scene in m_Scenes) {
				scene.OnLateUpdate(_dt);
			}
		}
	}

	public bool Add(GameScene _scene)
	{
		if (m_Scenes.Contains(_scene) == true) { return false; }

		m_Scenes.Add(_scene);
		return true;
	}
	public bool Remove(GameScene _scene)
	{
		if (m_Scenes.Contains(_scene) == false) { return false; }

		m_Scenes.Remove(_scene);
		return true;
	}

	public bool Enter(GameScene _scene)
	{
		if (Add(_scene) == true) {
			_scene.OnEnter();
			return true;
		}
		return false;
	}
	public bool Exit(GameScene _scene)
	{
		if (Remove(_scene) == true) {
			_scene.OnExit();
			return true;
		}
		return false;
	}

	public void EnterHandlingScene()
	{
		foreach (var scene in scenes) {
			scene.OnEnter();
		}
	}
	public void ExitHandlingScene()
	{
		foreach (var scene in scenes) {
			scene.OnExit();
		}
	}

}
