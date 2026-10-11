
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// シーン：起動
/// </summary>
public class BootManager : MonoBehaviour
{

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	public static void Boot()
	{
		bool isBoot = true;
		isBoot &= GameManager.isActive == true;
		isBoot &= GameSceneManager.isActive == true;
		isBoot &= UIManager.isActive == true;
		isBoot &= AudioManager.isActive == true;

		// 準備完了したらタイトルへ
		if (isBoot == true) {
//			GameSceneManager.instance.Load("Title");
			GameSceneUtility.LoadSceneAsyncOnFade("Title").Forget();
		}

	}

}
