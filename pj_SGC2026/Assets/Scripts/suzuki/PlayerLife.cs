using UnityEngine;

public class PlayerLife : MonoBehaviour
{
	public static PlayerLife instance;
	public int life = 3;

	[Header("リザルトパネル")]
	public ResultPanel resultPanel;

	[Header("プレイヤーのリスポーン管理")]
	public PlayerRespawn playerRespawn;
	[Header("カメラ追従スクリプト")]
	public CameraFollow cameraFollow;
	[Header("ScoreImageView")]
	public ScoreImageView scoreImageView;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void ReduceLife()
	{
		life--;
		Debug.Log("Life:" + life);

		if (life <= 0)
		{
			GameOver();
		}
	}

	private void GameOver()
	{
		Debug.Log("ゲームオーバー");
		// ★ カメラ追従停止
		if (cameraFollow != null)
		{
			cameraFollow.target = null;
		}
		// リスポーン停止
		if (playerRespawn != null)
		{
			playerRespawn.enabled = false;
		}
		// ★ゲームオーバー表示
		scoreImageView.UpdateScore(ScoreManager.instance.score);
		resultPanel.ShowGameOver();
	}
}
