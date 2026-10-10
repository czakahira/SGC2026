using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance;
	public int score = 0;

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

	public int GetScore()
	{
		return score;
	}

	public void AddScore(int amount)
	{
		score += amount;
		Debug.Log($"スコア +{amount} → 現在 {score}");
	}

	public void ResetScore()
	{
		score = 0;
	}
}
