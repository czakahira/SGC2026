
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/// <summary>
/// UI：ステージセレクト
/// </summary>
public class UITitleStageSelectWindow : UITitleSubWindow
{
	public enum EState
	{
		None = -1,

		Wait_Open,
		Wait_Select,

		Wait_Main,
	}

	/// <summary>
	/// ボタン軍：ステージセレクト
	/// </summary>
	[SerializeField] protected UITitleButton[] m_Btns_StageSelect;

	[SerializeField] protected EState m_State = EState.None;
	/// <summary>
	/// 選択中のインデックス
	/// </summary>
	[SerializeField] protected int m_CurrentSelectStageIdx;
	/// <summary>
	/// 決定されたインデックス
	/// </summary>
	[SerializeField] protected int m_DecideStageIdx = -1;

	protected void SetState(EState _state)
	{
		m_State = _state;
	}
	protected bool IsState(EState _state)
	{
		return m_State == _state;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="_idx"></param>
	public void SetSelect(int _idx)
	{
		m_CurrentSelectStageIdx = Mathf.Clamp(_idx, 0, m_Btns_StageSelect.Length - 1);

		for (int i = 0; i < m_Btns_StageSelect.Length; i++) {
			bool isSelected = i == m_CurrentSelectStageIdx;
			if (isSelected == true) {
				AudioManager.PlaySE(ESE.Decide_12, 0);
			}
			m_Btns_StageSelect[i].SetSelected(isSelected == true);
		}
	}

	/// <summary>
	/// 決定処理
	/// </summary>
	protected void SetDecide(int _idx)
	{

		// 決定
		m_DecideStageIdx = _idx;
		m_Btns_StageSelect[m_CurrentSelectStageIdx].SetDecide(true);

		//
		AudioManager.PlaySE(ESE.Decide_31, 0);
		// 
		SetState(EState.Wait_Main);
	}
	/// <summary>
	/// ステージが決定された
	/// </summary>
	public bool IsDecide()
	{
		return m_DecideStageIdx >= 0;
	}

	public override bool Open()
	{
		base.Open();

		m_DecideStageIdx = -1;
		SetSelect(m_CurrentSelectStageIdx);
		SetState(EState.Wait_Open);
		return true;
	}
	public override bool Close()
	{
		base.Close();

		m_DecideStageIdx = -1;
		SetState(EState.None);
		return true;
	}


	protected void OnEnner(UIBehaviour _ui, PointerEventData _data)
	{
		for (int i = 0; i < m_Btns_StageSelect.Length; i++) {
			if (m_Btns_StageSelect[i] == _ui) {
				SetSelect(i);
				break;
			}
		}
	}
	protected void OnClick(UIBehaviour _ui, PointerEventData _data)
	{
		for (int i = 0; i < m_Btns_StageSelect.Length; i++) {
			if (m_Btns_StageSelect[i] == _ui) {
				SetDecide(i);
				break;
			}
		}
	}

	protected override void Awake()
	{
		base.Awake();

		foreach (var btn in m_Btns_StageSelect) {
			btn.Init();
			btn.onEnter += OnEnner;
			btn.onClick += OnClick;
		}
		SetSelect(0);

		SetState(EState.None);
	}
	public override void OnUpdate(float _dt)
	{
		base.OnUpdate(_dt);

		Keyboard kb = Keyboard.current;

		switch (m_State) {
			case EState.Wait_Open:
				if (isViewAnim == false) {
					SetState(EState.Wait_Select);
				}
				break;

			case EState.Wait_Select:
				int idx = m_CurrentSelectStageIdx;

				if (kb.spaceKey.wasPressedThisFrame == true) {
					SetDecide(m_CurrentSelectStageIdx);
				}
				else if (kb.wKey.wasPressedThisFrame == true) {
					// ↑
					idx--;
					if (idx < 0) { idx = (m_Btns_StageSelect.Length - 1); }
					SetSelect(idx);
				}
				else if (kb.sKey.wasPressedThisFrame == true) {
					// ↓
					idx++;
					if (idx >= m_Btns_StageSelect.Length) { idx = 0; }
					SetSelect(idx);
				}

				break;

			case EState.Wait_Main:

				break;

			case EState.None:
			default:
				break;
		}
	}
	

}