
using UnityEngine;

/// <summary>
/// シーン：起動
/// </summary>
public class BootManager : MonoBehaviour
{
	void Awake()
	{
		bool isBoot = true;
		isBoot &= GameManager.isActive == true;
		isBoot &= GameSceneManager.isActive == true;
		isBoot &= UIManager.isActive == true;
		isBoot &= AudioManager.isActive == true;

		// 準備完了したらタイトルへ
		if (isBoot == true) {
			GameSceneManager.instance.Load("Title");
		}

	}

}
