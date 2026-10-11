using UnityEngine;
using UnityEngine.UI;

public class ScoreImageView : MonoBehaviour
{
	public NumberSpriteSet spriteSet;
	public GameObject digitPrefab; // Image を持つプレハブ
	public Transform digitRoot;    // 画像を並べる親
	public void UpdateScore(int score)
	{
		Debug.Log("UpdateScore CALLED: " + score);
		// 既存の数字を削除
		foreach (Transform child in digitRoot)
		{
			Destroy(child.gameObject);
		}
		// ★ 0点なら "0" を強制表示
		string scoreStr = (score == 0) ? "0" : score.ToString();
		Debug.Log("scoreStr = " + scoreStr);
		// 1文字ずつ画像を生成
		foreach (char c in scoreStr)
		{
			GameObject obj = Instantiate(digitPrefab, digitRoot);
			Image img = obj.GetComponent<Image>();

			Debug.Log("Digit created: " + c);

			if (char.IsDigit(c))
			{
				int num = c - '0';
				img.sprite = spriteSet.numberSprites[num];
			}
		}
		// 桁数に応じて中央に補正する
		int digitCount = scoreStr.Length;

		float digitWidth = 53f; // 1桁の幅
		float spacing = 10f;    // 桁間スペース
		float totalWidth = digitCount * digitWidth + (digitCount - 1) * spacing;

		RectTransform rt = GetComponent<RectTransform>();
		rt.anchoredPosition = new Vector2(-totalWidth / 2f, rt.anchoredPosition.y);
	}
}
