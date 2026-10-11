
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI：フェード
/// </summary>
public class UIFader : UIComponent
{
	public enum EState
	{
		None = -1,

		Fade,
	}

	/// <summary>
	/// 
	/// </summary>
	[SerializeField] protected Image m_Image;
	/// <summary>
	/// 
	/// </summary>
	protected SimpleTimer m_Timer;

	protected EState m_State;

	/// <summary>
	/// フェード中か
	/// </summary>
	public bool isFade => IsState(EState.Fade) == true;

	protected void SetState(EState _state)
	{
		m_State = _state;
	}
	public bool IsState(EState _state)
	{
		return m_State == _state;
	}
	protected override void Awake()
	{
		base.Awake();

		m_Timer = new SimpleTimer();

		SetState(EState.None);
	}
	protected override void EachUpdate(float _dt)
	{
		base.EachUpdate(_dt);

		// フェード処理
		if (isFade == true) {
			m_Timer.Run(_dt);
			this.alpha = m_Timer.progress;
			if (m_Timer.IsElapsed() == true) {
				// カウントダウン = フェードイン
				if (m_Timer.reverse == true) {
					// 遮蔽しないように
					SetInteract(false);
				}
				SetState(EState.None);
			}
		}
	}

	/// <summary>
	/// 画面が薄れていく
	/// </summary>
	/// <param name="_duration"></param>
	public void FadeOut(float _duration = 0.5f)
	{
		m_Timer.duration = _duration;
		m_Timer.reverse = false;
		m_Timer.SetElapsed(0f);
		SetInteract(true);
		SetState(EState.Fade);
	}
	/// <summary>
	/// 画面が色づいていく
	/// </summary>
	/// <param name="_duration"></param>
	public void FadeIn(float _duration = 0.5f)
	{
		m_Timer.duration = _duration;
		m_Timer.reverse = true;
		m_Timer.SetElapsed(_duration);
		SetInteract(true);
		SetState(EState.Fade);
	}

}