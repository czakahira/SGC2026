using UnityEngine;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance;

	public int score = 0;

	// ★ ScoreImageView を参照する
	public ScoreImageView scoreImageView;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);

			// ルートオブジェクトであることを保証
			transform.SetParent(null);
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	public int GetScore()
	{
		return score;
	}

	public void AddScore(int amount)
	{
		score += amount;
		Debug.Log($"スコア +{amount} → 現在 {score}");

		if (scoreImageView != null)
		{
			scoreImageView.UpdateScore(score);
		}
	}

	public void ResetScore()
	{
		score = 0;

		if (scoreImageView != null)
		{
			scoreImageView.UpdateScore(score);
		}
	}
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// Main シーンに入ったら ScoreImageView を探す
		if (scene.name == "Main")
		{
			scoreImageView = FindFirstObjectByType<ScoreImageView>();
			if (scoreImageView != null)
			{
				scoreImageView.UpdateScore(score);
			}
		}
	}
}
