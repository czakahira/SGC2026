
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

public class GameSceneUtility
{

	/// <summary>
	/// 
	/// </summary>
	/// <param name="_sceneName"></param>
	/// <param name="_onComplete"></param>
	/// <param name="_loadSceneMode"></param>
	/// <returns></returns>
	public async static UniTask LoadSceneAsyncOnLoading(string _sceneName, UnityAction<Scene> _onComplete = null, LoadSceneMode _loadSceneMode = LoadSceneMode.Single)
	{
		CancellationToken token = GameManager.instance.quitToken;

		// ロードUIで隠す
		UIManager.LoadStart();
		while (UIManager.instance.loading.isloading == true) {
			await UniTask.Yield(PlayerLoopTiming.Update, token);
		}
		// シーン読み込む
		await GameSceneManager.instance.LoadAsync(_sceneName, _loadSceneMode);
		// 少し待つ
		await UniTask.Delay(2000, DelayType.UnscaledDeltaTime, PlayerLoopTiming.PreUpdate, token);
		// ロードUIを閉じる
		UIManager.LoadEnd();
		while (UIManager.instance.loading.isloading == true) {
			await UniTask.Yield(PlayerLoopTiming.Update, token);
		}
		// コールバック
		_onComplete?.Invoke(SceneManager.GetSceneByName(_sceneName));
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="_sceneName"></param>
	/// <param name="_onComplete"></param>
	/// <param name="_loadSceneMode"></param>
	/// <returns></returns>
	public async static UniTask LoadSceneAsyncOnFade(string _sceneName, UnityAction<Scene> _onComplete = null, LoadSceneMode _loadSceneMode = LoadSceneMode.Single, float _durationOut = 0.5f, float _durationIn = 0.5f)
	{
		CancellationToken token = GameManager.instance.quitToken;

		UIManager.FadeOut(_durationOut);
		while (UIManager.instance.fader.isFade == true) {
			await UniTask.Yield(PlayerLoopTiming.Update, token);
		}

		await GameSceneManager.instance.LoadAsync(_sceneName, _loadSceneMode);

		UIManager.FadeIn(_durationIn);
		while (UIManager.instance.fader.isFade == true) {
			await UniTask.Yield(PlayerLoopTiming.Update, token);
		}

		_onComplete?.Invoke(SceneManager.GetSceneByName(_sceneName));
	}

}