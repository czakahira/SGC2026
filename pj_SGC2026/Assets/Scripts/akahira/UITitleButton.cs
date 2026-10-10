
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// UI：タイトル用ボタン
/// </summary>
public class UITitleButton : UIButton
{
	public enum EState
	{
		None = -1,

		In,
		Out,

		/// <summary>
		/// 決定
		/// </summary>
		Decide,
		Decided,
	}

	/// <summary>
	/// ▶
	/// </summary>
	[SerializeField] protected CanvasGroup m_Triangle_Alpha;
	/// <summary>
	/// 黄色い楕円：透明度
	/// </summary>
	[SerializeField] protected RectTransform m_Circle_Yellow_Rect;
	/// <summary>
	/// 黄色い楕円：矩形
	/// </summary>
	[SerializeField] protected CanvasGroup m_Circle_Yellow_Alpha;
	/// <summary>
	/// 文字テククチャ
	/// </summary>
	[SerializeField] protected Image m_Text;
	/// <summary>
	/// 
	/// </summary>
	[SerializeField] protected float m_HoverAnim_Duration;
	[SerializeField] protected SimpleTimer m_HoverAnim_Timer;

	/// <summary>
	/// 文字色（外）
	/// </summary>
	[SerializeField] protected Color m_TextColor_Wait;
	/// <summary>
	/// 文字色（内）
	/// </summary>
	[SerializeField] protected Color m_TextColor_Decide;

	[SerializeField] protected Vector3 m_Scale_Out;
	[SerializeField] protected Vector3 m_Scale_In;

	[SerializeField] protected EState m_State;

	

	protected void SetState(EState _state)
	{
		m_State = _state;
		switch (m_State) {

			case EState.In:
				m_Triangle_Alpha.alpha = 1f;
				break;

			case EState.Out:
				m_Triangle_Alpha.alpha = 0f;
				break;

			case EState.Decide:
				m_Triangle_Alpha.alpha = 0f;
				m_Circle_Yellow_Alpha.alpha = 1f;
				m_Text.color = m_TextColor_Decide;
				break;

			case EState.None:
			default:
				break;

		}
	}
	protected bool IsState(EState _state)
	{
		return m_State == _state;
	}
	/// <summary>
	/// 決定された
	/// </summary>
	/// <returns></returns>
	protected bool IsDecide()
	{
		return IsState(EState.Decide) == true || IsState(EState.Decided) == true;
	}
	/// <summary>
	/// 決定済み
	/// </summary>
	public bool IsDecided()
	{ 
		return IsState(EState.Decided) == true;	
	}

	/// <summary>
	/// 初期化
	/// </summary>
	public void Init()
	{
		// 非表示
		// ▶
		m_Triangle_Alpha.alpha = 0f;
		// 黄色い楕円
		m_Circle_Yellow_Alpha.alpha = 0f;
		// テキストの色
		m_Text.color = m_TextColor_Wait;
		// スケール
		rectTransform.localScale = m_Scale_Out;

		SetState(EState.None);
	}

	protected override void Awake()
	{
		base.Awake();

		m_HoverAnim_Timer = new SimpleTimer();
		m_HoverAnim_Timer.duration = m_HoverAnim_Duration;


		Init();
	}

	protected override void EachUpdate(float _dt)
	{
		base.EachUpdate(_dt);

		float rate = 0f;

		switch (m_State) {

			case EState.In:
				m_HoverAnim_Timer.Run(_dt);
				rate = m_HoverAnim_Timer.progress;
				rectTransform.localScale = Vector3.Lerp(m_Scale_Out, m_Scale_In, rate);
				if (m_HoverAnim_Timer.IsElapsed() == true) {
					SetState(EState.None);
				}
				break;

			case EState.Out:
				m_HoverAnim_Timer.Run(_dt);
				rate = m_HoverAnim_Timer.progress;
				rectTransform.localScale = Vector3.Lerp(m_Scale_Out, m_Scale_In, rate);
				if (m_HoverAnim_Timer.IsElapsed() == true) {
					SetState(EState.None);
				}

				break;

			case EState.Decide:
				SetState(EState.Decided);
				break;

			case EState.Decided:
				break;

			case EState.None:
			default:
				break;
		}


	}

	public override void OnPointerEnter(PointerEventData _data)
	{
		base.OnPointerEnter(_data);

		if (IsDecide() == true) { return; }

		m_HoverAnim_Timer.reverse = false;

		AudioManager.PlaySE(ESE.Decide_12, 0);

		SetState(EState.In);
	}
	public override void OnPointerExit(PointerEventData _data)
	{
		base.OnPointerExit(_data);

		if (IsDecide() == true) { return; }

		m_HoverAnim_Timer.reverse = true;
		SetState(EState.Out);
	}
	public override void OnPointerClick(PointerEventData _data)
	{
		base.OnPointerClick(_data);

		AudioManager.PlaySE(ESE.Decide_31, 0);

		// 決定
		SetState(EState.Decide);
	}



}
