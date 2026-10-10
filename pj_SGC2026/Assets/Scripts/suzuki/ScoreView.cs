using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
	public TMP_Text scoreText;

	public void UpdateScore(int score)
	{
		scoreText.text = $"Score: {score}";
	}
}
