using UnityEngine;

public class ClearObject : MonoBehaviour
{
	[Header("リザルトパネル（Canvas内のUI）")]
	public GameObject resultPanel;

	private bool isCleared = false;

	private void OnTriggerEnter(Collider other)
	{
		if (isCleared) return;

		var player = other.GetComponent<PlayerColor>();
		if (player == null) return;

		isCleared = true;

		Debug.Log("ゲームクリア！");

		// ★リザルトパネル表示（アニメーション開始）
		if (resultPanel != null)
		{
			resultPanel.SetActive(true);

			var anim = resultPanel.GetComponent<ResultPanelAnimator>();
			if (anim != null)
			{
				anim.PlayAnimation();
			}
		}
	}
}
