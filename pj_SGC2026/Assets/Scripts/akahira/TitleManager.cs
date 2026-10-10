
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// シーン：タイトル
/// </summary>
public class TitleManager : MonoBehaviour
{
	public enum EState
	{
		None,

		/// <summary>
		/// タイトル
		/// </summary>
		Title_Loop,

		/// <summary>
		/// クレジット
		/// </summary>
		Credit_Open,
		Credit_Loop,
		Credit_Close,

		/// <summary>
		/// 本ゲームへ
		/// </summary>
		ToMain,
	}

	// ========================================
	//  Member
	// ========================================

	/// <summary>
	/// ボタン：スタート
	/// </summary>
	[SerializeField] protected UITitleButton m_Btn_Start;
	/// <summary>
	/// ボタン：クレジット
	/// </summary>
	[SerializeField] protected UITitleButton m_Btn_ToCredit;
	/// <summary>
	/// ボタン：クレジットを閉じる
	/// </summary>
	[SerializeField] protected UIButton m_Btn_FromCredit;


	/// <summary>
	/// UI：クレジット
	/// </summary>
	[SerializeField] protected RectTransform m_Credit;
	/// <summary>
	/// 
	/// </summary>
	[SerializeField] protected float m_CreditAnimDuration;
	/// <summary>
	/// 
	/// </summary>
	[SerializeField] protected AnimationCurve m_CreditAnimCurve;
	/// <summary>
	/// クレジットUIの初期位置
	/// </summary>
	[SerializeField] protected Vector2 m_Credit_StartPos;
	/// <summary>
	/// 
	/// </summary>
	[SerializeField] protected SimpleTimer m_CreditSlidTimer;

	/// <summary>
	/// ニンゲン
	/// </summary>
	[SerializeField] protected RectTransform m_Player;

	[SerializeField] protected float m_PlayerWalkAnim_Duration;
	protected SimpleTimer m_PlayerWalkAnim_Timer;
	[SerializeField] protected Vector3 m_PlayerWalk_Euler_In;
	[SerializeField] protected Vector3 m_PlayerWalk_Euler_Out;

	/// <summary>
	/// ステート
	/// </summary>
	[SerializeField] protected EState m_State;

	// ========================================
	//  Method
	// ========================================

	protected void SetState(EState _state)
	{
		m_State = _state;
		switch (m_State) {
			case EState.Title_Loop:
				break;

			case EState.Credit_Open:
				m_CreditSlidTimer.reverse = false;
				break;
			case EState.Credit_Loop:

				break;
			case EState.Credit_Close:
				m_CreditSlidTimer.reverse = true;
				break;

			case EState.ToMain:

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
	/// コールバック：開始
	/// </summary>
	protected void OnStart(PointerEventData _data)
	{
		// 無効
		if (IsState(EState.Title_Loop) == false) { return; }

		// BGM止める
		AudioManager.StopBGN(EBGM.Title);

		// 次へ
		SetState(EState.ToMain);
	}
	/// <summary>
	/// コールバック：クレジットを開く
	/// </summary>
	protected void OnCredit_Open(PointerEventData _data)
	{
		if (IsState(EState.Title_Loop) == false) { return; }

		// クレジットを開く
		SetState(EState.Credit_Open);
	}
	/// <summary>
	/// コールバック：クレジットを閉じる
	/// </summary>
	protected void OnCredit_Close(PointerEventData _data)
	{
		if (IsState(EState.Credit_Loop) == false) { return; }

		m_Btn_Start.Init();
		m_Btn_ToCredit.Init();

		// 
		SetState(EState.Credit_Close);
	}

	void Awake()
	{

	}
	void Start()
	{
		m_CreditSlidTimer = new SimpleTimer();
		m_CreditSlidTimer.duration = m_CreditAnimDuration;
		m_CreditSlidTimer.reverse = false;

		m_PlayerWalkAnim_Timer = new SimpleTimer();
		m_PlayerWalkAnim_Timer.duration = m_PlayerWalkAnim_Duration;

		m_Credit.anchoredPosition = m_Credit_StartPos;

		// コールバック
		m_Btn_Start.onClick += OnStart;
		m_Btn_ToCredit.onClick += OnCredit_Open;
		m_Btn_FromCredit.onClick += OnCredit_Close;

		AudioManager.PlayBGN(EBGM.Title);

		SetState(EState.Title_Loop);
	}
	void Update()
	{
		float dt = Time.deltaTime;

		m_PlayerWalkAnim_Timer.Run(dt);
		m_Player.localRotation = Quaternion.Lerp(Quaternion.Euler(m_PlayerWalk_Euler_In), Quaternion.Euler(m_PlayerWalk_Euler_Out), m_PlayerWalkAnim_Timer.progress);
		if (m_PlayerWalkAnim_Timer.IsElapsed() == true) {
			m_PlayerWalkAnim_Timer.reverse = m_PlayerWalkAnim_Timer.reverse == false;
		}

		switch (m_State) {
			case EState.Title_Loop:
				break;

			case EState.Credit_Open: {
				m_CreditSlidTimer.Run(dt);
				float rate = m_CreditAnimCurve.Evaluate(m_CreditSlidTimer.progress);
				Vector2 pos = m_Credit.anchoredPosition;
				m_Credit.anchoredPosition = Vector2.Lerp(pos, Vector2.zero, rate);
				if (m_CreditSlidTimer.IsElapsed() == true) {
					SetState(EState.Credit_Loop);
				}
			} break;

			case EState.Credit_Loop:

				break;

			case EState.Credit_Close: {
				m_CreditSlidTimer.Run(dt);
				float rate = m_CreditAnimCurve.Evaluate(m_CreditSlidTimer.progress);
				Vector2 pos = m_Credit.anchoredPosition;
				m_Credit.anchoredPosition = Vector2.Lerp(m_Credit_StartPos, pos, rate);
				if (m_CreditSlidTimer.IsElapsed() == true) {
					SetState(EState.Title_Loop);
				}
			} break;

			case EState.ToMain:

				break;

			case EState.None:
			default:
				break;

		}

	}
}
