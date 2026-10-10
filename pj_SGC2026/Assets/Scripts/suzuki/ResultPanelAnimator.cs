using UnityEngine;

public class ResultPanelAnimator : MonoBehaviour
{
	public RectTransform panel;
	public float duration = 0.5f;      // 降りてくる時間
	public float startY = 800f;        // 画面外の初期位置
	public float endY = 0f;            // 最終位置

	private float time = 0f;
	private bool playing = false;

	private void Awake()
	{
		// 初期位置を画面外にセット
		Vector2 pos = panel.anchoredPosition;
		pos.y = startY;
		panel.anchoredPosition = pos;
	}

	public void PlayAnimation()
	{
		time = 0f;
		playing = true;
	}

	private void Update()
	{
		if (!playing) return;

		time += Time.deltaTime;
		float t = Mathf.Clamp01(time / duration);

		// イージング（ふわっと降りる）
		t = Mathf.Sin(t * Mathf.PI * 0.5f);

		Vector2 pos = panel.anchoredPosition;
		pos.y = Mathf.Lerp(startY, endY, t);
		panel.anchoredPosition = pos;

		if (t >= 1f)
		{
			playing = false;
		}
	}
}
