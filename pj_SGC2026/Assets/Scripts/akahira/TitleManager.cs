
using UnityEngine;
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
	[SerializeField] protected Button m_Btn_Start;
	/// <summary>
	/// ボタン：クレジット
	/// </summary>
	[SerializeField] protected Button m_Btn_ToCredit;
	/// <summary>
	/// ボタン：クレジットを閉じる
	/// </summary>
	[SerializeField] protected Button m_Btn_FromCredit;

	/// <summary>
	/// ステート
	/// </summary>
	[SerializeField] protected EState m_State;


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
	protected void OnStart()
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
	protected void OnCredit_Open()
	{
		if (IsState(EState.Title_Loop) == false) { return; }

		// クレジットを開く
		SetState(EState.Credit_Open);
	}
	/// <summary>
	/// コールバック：クレジットを閉じる
	/// </summary>
	protected void OnCredit_Close()
	{
		if (IsState(EState.Credit_Loop) == false) { return; }

		// 
		SetState(EState.Credit_Close);
	}

	void Awake()
	{
		m_CreditSlidTimer = new SimpleTimer();
		m_CreditSlidTimer.duration = m_CreditAnimDuration;
		m_CreditSlidTimer.reverse = false;

		m_Credit.anchoredPosition = m_Credit_StartPos;

		// コールバック
		m_Btn_Start.onClick.AddListener(OnStart);
		m_Btn_ToCredit.onClick.AddListener(OnCredit_Open);
		m_Btn_FromCredit.onClick.AddListener(OnCredit_Close);

		AudioManager.PlayBGN(EBGM.Title);

		SetState(EState.Title_Loop);
	}
	void Start()
	{
		
	}
	void Update()
	{
		float dt = Time.deltaTime;


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
