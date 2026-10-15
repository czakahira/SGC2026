
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SGC2026
{
	/// <summary>
	/// UI：タイトル用ボタン
	/// </summary>
	public class UITitleButton : UIButton
	{
		[Flags]
		public enum EState
		{
			None = 0,

			Selected = 1 << 0,

			/// <summary>
			/// 決定
			/// </summary>
			Decide = 1 << 1,
			Decided = 1 << 2,
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
		[SerializeField] protected Image[] m_Texts;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected float m_HoverAnim_Duration;
		[SerializeField] protected SimpleTimer m_SelectAnim_Timer;

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


		/// <summary>
		/// 文字テクスチャの色を一括で変更
		/// </summary>
		/// <param name="_color"></param>
		protected void SetTextColor(Color _color)
		{
			foreach (var txt in m_Texts) {
				txt.color = _color;
			}
		}

		protected void AddState(EState _state)
		{
			if (Utility_Enum.AddMissinFlag(ref m_State, _state) == true) {
				if (_state.HasFlag(EState.Selected) == true) {
					m_Triangle_Alpha.alpha = 1f;
					m_SelectAnim_Timer.reverse = false;
				}
				if (_state.HasFlag(EState.Decide) == true) {
					m_Triangle_Alpha.alpha = 0f;
					m_Circle_Yellow_Alpha.alpha = 1f;
					SetTextColor(m_TextColor_Decide);
				}
				if (_state.HasFlag(EState.Decided) == true) {

				}
			}
		}
		protected void DelState(EState _state)
		{
			if (Utility_Enum.DelPresentFlag(ref m_State, _state) == true) {
				if (_state.HasFlag(EState.Selected) == true) {
					m_Triangle_Alpha.alpha = 0f;
					m_SelectAnim_Timer.reverse = true;
				}
				if (_state.HasFlag(EState.Decide) == true) {

				}
				if (_state.HasFlag(EState.Decided) == true) {

				}
			}
		}


		/// <summary>
		/// 決定された
		/// </summary>
		/// <returns></returns>
		protected bool IsDecide()
		{
			return m_State.HasFlag(EState.Decide) == true && m_State.HasFlag(EState.Decided) == false;
		}
		/// <summary>
		/// 決定済み
		/// </summary>
		public bool IsDecided()
		{
			return m_State.HasFlag(EState.Decided) == true && m_State.HasFlag(EState.Decided) == true;
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
			SetTextColor(m_TextColor_Wait);
			// タイマ－
			m_SelectAnim_Timer.Reset();
			m_SelectAnim_Timer.reverse = true;
			// スケール
			rectTransform.localScale = m_Scale_Out;

			m_State = EState.None;
		}

		protected override void Awake()
		{
			base.Awake();

			m_SelectAnim_Timer = new SimpleTimer();
			m_SelectAnim_Timer.duration = m_HoverAnim_Duration;

			Init();
		}

		public override void OnUpdate(float _dt)
		{
			base.OnUpdate(_dt);

			float rate = 0f;

			if (m_State.HasFlag(EState.Decide) == true && m_State.HasFlag(EState.Decided) == false) {

			}
			else {
				m_SelectAnim_Timer.Run(_dt);
				rate = m_SelectAnim_Timer.progress;
				rectTransform.localScale = Vector3.Lerp(m_Scale_Out, m_Scale_In, rate);
				if (m_SelectAnim_Timer.complete == false) {
				}
			}

		}


		/// <summary>
		/// 選択状態にする
		/// </summary>
		public void SetSelected(bool _value)
		{
			if (IsDecide() == true) { return; }

			if (_value == true) {
				AddState(EState.Selected);
			}
			else {
				DelState(EState.Selected);
			}
		}
		public void SetDecide(bool _value)
		{
			if (_value == true) {
				AddState(EState.Decide);
			}
			else {
				DelState(EState.Decide);
			}
		}

		public override void OnPointerEnter(PointerEventData _data)
		{
			base.OnPointerEnter(_data);

			//		SetSelected(true);
		}
		public override void OnPointerExit(PointerEventData _data)
		{
			base.OnPointerExit(_data);

			//		SetSelected(false);
		}
		public override void OnPointerClick(PointerEventData _data)
		{
			base.OnPointerClick(_data);

			//		AddState(EState.Decide);
		}

	}
}