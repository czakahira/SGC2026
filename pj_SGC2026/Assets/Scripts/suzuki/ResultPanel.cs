
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace SGC2026
{
	/// <summary>
	/// UI：リザルト
	/// </summary>
	public class ResultPanel : UIComponent
	{
		[Header("表示するテキスト")]
		public TMP_Text resultText;

		[Header("画像（Canvasに置いてあるやつ）")]
		public GameObject clearImage;
		public GameObject gameOverImage;

		[Header("アニメーション")]
		public ResultPanelAnimator animator;
		/// <summary>
		/// ボタン：タイトルへ
		/// </summary>
		[SerializeField] protected UIButton m_Btn_Title;

		public UIButton btnTItle => m_Btn_Title;

		public void ShowClear()
		{
			resultText.text = "Clear!";

			clearImage.SetActive(true);
			gameOverImage.SetActive(false);

			gameObject.SetActive(true);
			animator.PlayAnimation();
		}

		public void ShowGameOver()
		{
			resultText.text = "GameOver";

			clearImage.SetActive(false);
			gameOverImage.SetActive(true);

			gameObject.SetActive(true);
			animator.PlayAnimation();
		}
	}

}