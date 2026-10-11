using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
		// スコアを文字列化
		string scoreStr = score.ToString();

		
		// 1文字ずつ画像を生成
		foreach (char c in scoreStr)
		{
			if (score == 0)
			{
				scoreStr = "0";
			}
			else
			{
				scoreStr = "+" + score.ToString();
			}
			GameObject obj = Instantiate(digitPrefab, digitRoot);
			Image img = obj.GetComponent<Image>();
			Debug.Log("Digit created: " + obj.name);
			if (c == '+')
			{
				img.sprite = spriteSet.plusSprite;
			}
			else if (char.IsDigit(c))
			{
				int num = c - '0';
				img.sprite = spriteSet.numberSprites[num];
			}
		}
		// 桁数に応じて中央に補正する
		int digitCount = scoreStr.Length;
		// 1桁あたりの幅（digitPrefab の Width）
		float digitWidth = 53f; // Inspector の Width と同じ値にする
		// Spacing（Layout Group の spacing）
		float spacing = 10f;
		// 合計幅
		float totalWidth = digitCount * digitWidth + (digitCount - 1) * spacing;
		// 親の RectTransform を中央に補正
		RectTransform rt = GetComponent<RectTransform>();
		rt.anchoredPosition = new Vector2(-totalWidth / 2f, rt.anchoredPosition.y);
	}
}
