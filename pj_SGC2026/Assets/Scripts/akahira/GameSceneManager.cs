
using UnityEngine;
using UnityEngine.SceneManagement;

using Cysharp.Threading.Tasks;

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

	public async UniTask LoadAsync(string _sceneName, LoadSceneMode _loadSceneMode = LoadSceneMode.Single)
	{
		var op = SceneManager.LoadSceneAsync(_sceneName, _loadSceneMode);
		await op;
	}

}
