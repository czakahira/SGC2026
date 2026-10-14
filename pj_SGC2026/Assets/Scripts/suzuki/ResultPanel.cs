using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class ResultPanel : MonoBehaviour
{
	[Header("表示するテキスト")]
	public TMP_Text resultText;

	[Header("画像（Canvasに置いてあるやつ）")]
	public GameObject clearImage;
	public GameObject gameOverImage;

	[Header("アニメーション")]
	public ResultPanelAnimator animator;

	public Button titleButton;
	private void Start()
	{
		// タイトルに戻るボタン
		titleButton.onClick.AddListener(() =>
		{
			// スコアリセット
			ScoreManager.instance.ResetScore();

			// タイトルへ戻る
			GameSceneUtility.LoadSceneAsyncOnLoading("Title").Forget();
		});
	}
	private void OnEnable()
	{
		// アニメーション開始
		if (animator != null)
		{
			animator.PlayAnimation();
		}
	}

	public void ShowClear()
	{
		resultText.text = "Clear!";

		clearImage.SetActive(true);
		gameOverImage.SetActive(false);

		gameObject.SetActive(true);
	}

	public void ShowGameOver()
	{
		resultText.text = "GameOver";

		clearImage.SetActive(false);
		gameOverImage.SetActive(true);

		gameObject.SetActive(true);
	}
}
