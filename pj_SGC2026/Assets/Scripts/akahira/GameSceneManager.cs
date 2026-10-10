
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// マネージャー：シーン
/// </summary>
public class GameSceneManager : SingletonBehaviour<GameSceneManager>
{

	/// <summary>
	/// シーンを動機ロード
	/// </summary>
	/// <param name="_sceneName"></param>
	public void Load(string _sceneName)
	{
		SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
	}

}
