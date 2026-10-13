
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using NUnit.Framework.Internal.Filters;
using Cysharp.Threading.Tasks;

/// <summary>
/// シーン：タイトル
/// </summary>
public class TitleManager : GameScene
{
	public enum EState
	{
		None = -1,

		/// <summary>
		/// 選択待ち
		/// </summary>
		Title_Open,
		Title_Loop,

		/// <summary>
		/// ステージセレクト
		/// </summary>
		StageSelect_Open,
		StageSelect_Loop,
		StageSelect_Close,

		/// <summary>
		/// クレジット
		/// </summary>
		Credit_Open,
		Credit_Loop,
		Credit_Close,

		/// <summary>
		/// 遊び方
		/// </summary>
		HowTo_Open,
		HowTo_Loop,
		HowTo_Close,

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
	[SerializeField] protected UITitleButton m_Btn_Staff;
	/// <summary>
	/// ボタン：遊び方
	/// </summary>
	[SerializeField] protected UITitleButton m_Btn_HowTo;

	/// <summary>
	/// UI：ステージセレクト
	/// </summary>
	[SerializeField] protected UITitleStageSelectWindow m_StageSelect;
	/// <summary>
	/// UI：スタッフ
	/// </summary>
	[SerializeField] protected UITitleStaffWIndow m_Staff;
	/// <summary>
	/// UI：遊び方
	/// </summary>
	[SerializeField] protected UITitleSubWindow m_HowTo;

	/// <summary>
	/// ニンゲン
	/// </summary>
	[SerializeField] protected UIRunnigMan m_RunnnigMan;

	[SerializeField] protected float m_PlayerWalkAnim_Duration;
	protected SimpleTimer m_PlayerWalkAnim_Timer;
	[SerializeField] protected Vector3 m_PlayerWalk_Euler_In;
	[SerializeField] protected Vector3 m_PlayerWalk_Euler_Out;

	/// <summary>
	/// ステート
	/// </summary>
	[SerializeField] protected EState m_State;

	/// <summary>
	/// ボタン：タイトルで選択するもの一覧
	/// </summary>
	protected UITitleButton[] m_Btn_Titles;
	/// <summary>
	/// 選択中のボタン
	/// </summary>
	protected int m_CurrentSelectedTitleBtnIdx = -1;

	// ========================================
	//  Method
	// ========================================

	protected void SetState(EState _state)
	{
		m_State = _state;
		switch (m_State) {
			case EState.Title_Open:
				UIManager.instance.guide.Open(0);
				break;
			case EState.Title_Loop:
				UIManager.SetGuide(0);
				break;

			case EState.StageSelect_Open:
				UIManager.SetGuide(1);
				m_StageSelect.btnClose.blockRaycasts = true;
				m_StageSelect.btnClose.interactable = true;
				m_StageSelect.Open();
				break;
			case EState.StageSelect_Loop:
				break;
			case EState.StageSelect_Close:
				m_StageSelect.btnClose.blockRaycasts = false;
				m_StageSelect.btnClose.interactable = false;
				m_StageSelect.Close();
				break;


			case EState.Credit_Open:
				UIManager.SetGuide(2);
				m_Staff.btnClose.blockRaycasts = true;
				m_Staff.btnClose.interactable = true;
				m_Staff.Open();
				break;
			case EState.Credit_Loop:
				break;
			case EState.Credit_Close:
				m_Staff.btnClose.blockRaycasts = false;
				m_Staff.btnClose.interactable = false;
				m_Staff.Close();
				break;

			case EState.HowTo_Open:
				UIManager.SetGuide(3);
				m_HowTo.btnClose.blockRaycasts = true;
				m_HowTo.btnClose.interactable = true;
				m_HowTo.Open();
				break;
			case EState.HowTo_Loop:
				break;
			case EState.HowTo_Close:
				m_HowTo.btnClose.blockRaycasts = false;
				m_HowTo.btnClose.interactable = false;
				m_HowTo.Close();
				break;

			case EState.ToMain:
				UIManager.instance.guide.Close(0);
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

	protected int GetTitleBtnIdx(UIBehaviour _btn)
	{
		for (int i = 0; i < m_Btn_Titles.Length; i++) {
			if (m_Btn_Titles[i] == _btn) {
				return i;
			}
		}
		return -1;
	}
	/// <summary>
	/// 指定ボタンを選択状態にする
	/// </summary>
	protected void SetSelectTitleBtn(int _idx, bool _se = true)
	{
		m_CurrentSelectedTitleBtnIdx = _idx;

		for (int i = 0; i < m_Btn_Titles.Length; i++) {
			bool isSelected = (i == _idx);
			if (_se == true) {
				if (isSelected == true) {
					AudioManager.PlaySE(ESE.Decide_12, 0);
				}
			}
			m_Btn_Titles[i].SetSelected(isSelected == true);
		}
	}
	/// <summary>
	/// ステージセレクト：開く
	/// </summary>
	protected void Open_StageSelect()
	{
		AudioManager.PlaySE(ESE.Decide_31, 0);
		// 次へ
		SetState(EState.StageSelect_Open);
	}
	/// <summary>
	/// ステージセレクト：閉じる
	/// </summary>
	protected void Close_StageSelect()
	{
		// 全てのボタンを初期化
		foreach (var btn in m_Btn_Titles) {
			btn.Init();
		}
		// 【ステージセレクト】を選択済みにしておく
		SetSelectTitleBtn(GetTitleBtnIdx(m_Btn_Start), false);

		AudioManager.PlaySE(ESE.Decide_31, 0);
		// 
		SetState(EState.StageSelect_Close);
	}
	/// <summary>
	/// スタッフ：開く
	/// </summary>
	protected void Open_Staff()
	{
		AudioManager.PlaySE(ESE.Decide_31, 0);
		// クレジットを開く
		SetState(EState.Credit_Open);
	}
	/// <summary>
	/// スタッフ：閉じる
	/// </summary>
	protected void Close_Staff()
	{
		// 全てのボタンを初期化
		foreach (var btn in m_Btn_Titles) {
			btn.Init();
		}
		// 【スタッフ】を選択済みにしておく
		SetSelectTitleBtn(GetTitleBtnIdx(m_Btn_Staff), false);

		AudioManager.PlaySE(ESE.Decide_31, 0);
		// 
		SetState(EState.Credit_Close);
	}
	/// <summary>
	/// 遊び方：開く
	/// </summary>
	protected void Open_HowTo()
	{
		AudioManager.PlaySE(ESE.Decide_31, 0);
		// クレジットを開く
		SetState(EState.HowTo_Open);
	}
	/// <summary>
	/// 遊び方：閉じる
	/// </summary>
	protected void Close_HowTo()
	{
		// 全てのボタンを初期化
		foreach (var btn in m_Btn_Titles) {
			btn.Init();
		}
		// 【スタッフ】を選択済みにしておく
		SetSelectTitleBtn(GetTitleBtnIdx(m_Btn_HowTo), false);

		AudioManager.PlaySE(ESE.Decide_31, 0);
		// 
		SetState(EState.HowTo_Close);
	}


	protected void OnEnter(UIBehaviour _ui, PointerEventData _data)
	{
		if (IsState(EState.Title_Loop) == false) { return; }

		for (int i = 0; i < m_Btn_Titles.Length; i++) {
			if (m_Btn_Titles[i] == _ui) {
				if (m_CurrentSelectedTitleBtnIdx != i) {
					SetSelectTitleBtn(i);
				}
				break;
			}
		}
	}
	protected void OnClick(UIBehaviour _ui, PointerEventData _data)
	{
		if (IsState(EState.Title_Loop) == false) { return; }

		if (_ui == m_Btn_Start) {
			Open_StageSelect();
		}
		else if (_ui == m_Btn_Staff) {
			Open_Staff();
		}
		else if (_ui == m_Btn_HowTo) {
			Open_HowTo();
		}
		else {
		
		}
		(_ui as UITitleButton)?.SetDecide(true);
	}
	protected void OnClick_BackFromnStageSelect(UIBehaviour _ui, PointerEventData _data)
	{
		Close_StageSelect();
	}
	protected void OnClick_BackFromnStaff(UIBehaviour _ui, PointerEventData _data)
	{
		Close_Staff();
	}
	protected void OnClick_BackFromnHowTo(UIBehaviour _ui, PointerEventData _data)
	{
		Close_HowTo();
	}


	public override void OnEnter()
	{
		base.OnEnter();

		m_StageSelect.Init();
		m_StageSelect.CloseForce();
		m_Staff.Init();
		m_Staff.CloseForce();
		m_HowTo.Init();
		m_HowTo.CloseForce();

		m_PlayerWalkAnim_Timer = new SimpleTimer();
		m_PlayerWalkAnim_Timer.duration = m_PlayerWalkAnim_Duration;

		m_Btn_Titles = new UITitleButton[] { m_Btn_Start, m_Btn_Staff, m_Btn_HowTo };

		// コールバック
		foreach (var btn in m_Btn_Titles) {
			btn.onEnter += OnEnter;
		}
		m_Btn_Start.onClick += OnClick;
		m_Btn_Staff.onClick += OnClick;
		m_Btn_HowTo.onClick += OnClick;
		m_StageSelect.btnClose.onClick += OnClick_BackFromnStageSelect;
		m_Staff.btnClose.onClick += OnClick_BackFromnStaff;
		m_HowTo.btnClose.onClick += OnClick_BackFromnHowTo;
		

		// 一番目を選択状態にする
		SetSelectTitleBtn(0, false);

		m_RunnnigMan.Init();

		SetState(EState.Title_Open);
	}
	public override void OnUpdate(float _dt)
	{
		base.OnUpdate(_dt);

		float dt = Time.deltaTime;
		Keyboard kb = Keyboard.current;

		m_PlayerWalkAnim_Timer.Run(dt);
		//m_RunnnigMan.localRotation = Quaternion.Lerp(Quaternion.Euler(m_PlayerWalk_Euler_In), Quaternion.Euler(m_PlayerWalk_Euler_Out), m_PlayerWalkAnim_Timer.progress);
		if (m_PlayerWalkAnim_Timer.complete == true) {
			m_PlayerWalkAnim_Timer.reverse = m_PlayerWalkAnim_Timer.reverse == false;
		}

		switch (m_State) {
			// --------------------
			//  タイトル
			// --------------------
			case EState.Title_Open:

				AudioManager.PlayBGN(EBGM.Title);
				SetState(EState.Title_Loop);
				break;
			case EState.Title_Loop:
				// キーボード操作も可能
				int idx = m_CurrentSelectedTitleBtnIdx;
				if (kb.wKey.wasPressedThisFrame == true) {
					// ↑
					idx--;
					if (idx < 0) { idx = m_Btn_Titles.Length - 1; }
					SetSelectTitleBtn(idx);
				}
				else if (kb.sKey.wasPressedThisFrame == true) {
					// ↓
					idx++;
					if (idx >= m_Btn_Titles.Length) { idx = 0; }
					SetSelectTitleBtn(idx);
				}
				else if (kb.spaceKey.wasPressedThisFrame == true) {
					// 決定
					OnClick(m_Btn_Titles[m_CurrentSelectedTitleBtnIdx], null);
				}
				break;

			// --------------------
			//  ステージセレクト
			// --------------------
			case EState.StageSelect_Open:
				if (m_StageSelect.isViewAnim == false) {
					SetState(EState.StageSelect_Loop);
				}
				break;
			case EState.StageSelect_Loop:
				if (m_StageSelect.IsDecide() == true) {
					// BGM止める
					AudioManager.StopBGN(EBGM.Title);
					// シーン遷移
					GameSceneUtility.LoadSceneAsyncOnLoading("Main").Forget();
					//
					SetState(EState.ToMain);
					break;
				}
				if (kb.backspaceKey.wasPressedThisFrame == true) {
					Close_StageSelect();
				}
				break;
			case EState.StageSelect_Close:
				if (m_StageSelect.isViewAnim == false) {
					SetState(EState.Title_Loop);
				}
				break;

			// --------------------
			//  スタッフ
			// --------------------
			case EState.Credit_Open:
				if (m_Staff.isViewAnim == false) {
					SetState(EState.Credit_Loop);
				}
				break;

			case EState.Credit_Loop:
				if (kb.backspaceKey.wasPressedThisFrame == true) {
					Close_Staff();
				}
				break;

			case EState.Credit_Close:
				if (m_Staff.isViewAnim == false) {
					SetState(EState.Title_Loop);
				}
				break;

			// --------------------
			//  遊び方
			// --------------------
			case EState.HowTo_Open:
				if (m_HowTo.isViewAnim == false) {
					SetState(EState.HowTo_Loop);
				}
				break;

			case EState.HowTo_Loop:
				if (kb.backspaceKey.wasPressedThisFrame == true) {
					Close_HowTo();
				}
				break;

			case EState.HowTo_Close:
				if (m_HowTo.isViewAnim == false) {
					SetState(EState.Title_Loop);
				}
				break;

			// --------------------
			//  メインゲームへ
			// --------------------
			case EState.ToMain:

				break;

			case EState.None:
			default:
				break;

		}

	}

}
