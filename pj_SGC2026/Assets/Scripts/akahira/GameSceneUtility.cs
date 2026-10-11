
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;

public class GameSceneUtility
{

	public async static UniTask LoadSceneAsync(string _sceneName, UnityAction<Scene> _onLoad = null, LoadSceneMode _loadSceneMode = LoadSceneMode.Single)
	{
		CancellationToken token = GameManager.instance.quitToken;

		// ロードUIで隠す
		UIManager.LoadStart();
		while (UIManager.instance.loading.isloading == true) {
			await UniTask.Yield(PlayerLoopTiming.Update, token);
		}
		// シーン読み込む
		await GameSceneManager.instance.LoadAsync(_sceneName, _loadSceneMode);
		// ロードUIを閉じる
		UIManager.LoadEnd();
		while (UIManager.instance.loading.isloading == true) {
			await UniTask.Yield(PlayerLoopTiming.Update, token);
		}
		// コールバック
		_onLoad?.Invoke(SceneManager.GetSceneByName(_sceneName));
	}

}