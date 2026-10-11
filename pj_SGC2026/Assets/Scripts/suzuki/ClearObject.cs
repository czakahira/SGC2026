using UnityEngine;

public class ClearObject : MonoBehaviour
{
	[Header("リザルトパネル")]
	public ResultPanel resultPanel;

	[Header("カメラ追従スクリプト")]
	public CameraFollow cameraFollow;

	private bool isCleared = false;

	private void OnTriggerEnter(Collider other)
	{
		if (isCleared) return;

		var player = other.GetComponent<PlayerColor>();
		if (player == null) return;

		isCleared = true;

		Debug.Log("ゲームクリア！");

		// ★ カメラ追従停止
		if (cameraFollow != null)
		{
			cameraFollow.target = null;  
		}

		// ★ リザルトパネル表示（クリア用画像をON）
		resultPanel.ShowClear();
	}
}
