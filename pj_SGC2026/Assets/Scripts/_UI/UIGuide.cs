
using TMPro;
using UnityEngine;

/// <summary>
/// UI：操作ガイド
/// </summary>
public class UIGuide : UIComponent
{
	public readonly static string[] GUIDE_TEXTS = new string[]
	{
		"W: ↑ S: ↓ Space: つぎへ BackSpace: もどる",
		"W: ↑ S: ↓ Space: つぎへ BackSpace: もどる",
		"BackSpace: もどる",
		"BackSpace: もどる",
		"←: あか ↓: あお →： みどり Space: じゃんぷ",
		"",
	};
	public const float DEFAULT_DURATION = 0.2f;

	/// <summary>
	/// 
	/// </summary>
	[SerializeField] protected TextMeshProUGUI m_Text;
	[SerializeField] protected RectTransform m_Slider;
	[SerializeField] protected Vector2 m_SlidePos_Show;
	[SerializeField] protected Vector2 m_SlidePos_Hide;
	/// <summary>
	/// スライドタイマー
	/// </summary>
	[SerializeField] protected SimpleTimer m_SlideTimer;
	[SerializeField] protected bool m_Sliding = false;


	public void SetGuide(string _text)
	{
		m_Text.text = _text;
	}
	public void SetGuide(int _idx)
	{
		if (0 <= _idx && _idx < GUIDE_TEXTS.Length ) {
			SetGuide(GUIDE_TEXTS[_idx]);
			return;
		}
		SetGuide(string.Empty);
	}

	// ----------------------------------------
	//  表示 / 非表示
	// ----------------------------------------
	public void Open(int _idx = -1, float _duration = DEFAULT_DURATION)
	{
		SetGuide(_idx);

		m_SlideTimer.duration = _duration;
		m_SlideTimer.reverse = false;
		m_Sliding = true;
	}
	public void Close(int _idx = -1, float _duration = DEFAULT_DURATION)
	{
		SetGuide(_idx);

		m_SlideTimer.duration = _duration;
		m_SlideTimer.reverse = true;
		m_Sliding = true;
	}

	// ----------------------------------------
	//  即座に表示 / 非表示
	// ----------------------------------------
	public void ForceOpen(int _idx = -1)
	{
		Open(_idx, 0f);
		m_SlideTimer.IsElapsed(m_SlideTimer.duration);
	}
	public void ForceClose(int _idx = -1)
	{
		Close(_idx, 0f);
		m_SlideTimer.IsElapsed(0f);
	}

	protected override void Awake()
	{
		base.Awake();

		m_SlideTimer = new SimpleTimer();
		m_SlideTimer.duration = DEFAULT_DURATION;
		m_SlideTimer.reverse = false;
		m_SlideTimer.Reset();
	}
	public override void OnUpdate(float _dt)
	{
		base.OnUpdate(_dt);

		if (m_Sliding == true) {
			m_SlideTimer.Run(_dt);
			m_Slider.anchoredPosition = Vector2.Lerp(m_SlidePos_Hide, m_SlidePos_Show, m_SlideTimer.progress);
			if (m_SlideTimer.complete == true) {
				m_Sliding = false;
			}
		}

	}

}