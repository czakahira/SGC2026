
using UnityEngine;
using UnityEngine.UI;

namespace SGC2026
{
	/// <summary>
	/// UI：ローディング
	/// </summary>
	public class UILoading : UIComponent
	{
		public enum EState
		{
			None = -1,

			RunBorder_Horizontal = 0,
			RunBorder_Vertical,
			Loop,
		}

		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected RectTransform m_Bar;
		/// <summary>
		/// コビト
		/// </summary>
		[SerializeField] protected UIFlipAnim m_Ningen;

		[SerializeField] protected EState m_State;

		[SerializeField] protected Vector2 m_Size_Horizontal;
		[SerializeField] protected Vector2 m_Size_Vertical;

		protected SimpleTimer m_Timer;

		[SerializeField] protected float m_Duration_Horizontal;
		[SerializeField] protected float m_Duration_Vertical;

		protected bool m_Reverse;

		public bool isloading => IsState(EState.Loop) == false;


		protected override void Awake()
		{
			base.Awake();

			m_Timer = new SimpleTimer();

			m_Ningen.Init();

			SetState(EState.Loop);
		}
		protected void SetState(EState _state)
		{
			switch (m_State) {
				case EState.RunBorder_Horizontal:
					if (m_Reverse == true) {
						SetVisible(false);
						SetInteract(false);
					}
					break;
				case EState.RunBorder_Vertical:
					break;
				case EState.Loop:
					break;

				case EState.None:
				default:
					break;
			}
			m_State = _state;
			switch (m_State) {
				case EState.RunBorder_Horizontal:
					m_Timer.duration = m_Duration_Horizontal;
					m_Timer.SetElapsed(m_Reverse ? m_Timer.duration : 0f);
					break;
				case EState.RunBorder_Vertical:
					m_Timer.duration = m_Duration_Vertical;
					m_Timer.SetElapsed(m_Reverse ? m_Timer.duration : 0f);
					break;
				case EState.Loop:
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

		public override void OnUpdate(float _dt)
		{
			base.OnUpdate(_dt);

			float rate = 0f;

			m_Ningen.UpdateAnim(_dt);

			switch (m_State) {
				case EState.RunBorder_Horizontal:
					m_Timer.Run(_dt);
					rate = m_Timer.progress;
					m_Bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(m_Size_Horizontal.x, m_Size_Horizontal.y, rate));
					if (m_Timer.complete == true) {
						SetState(m_Reverse ? EState.Loop : EState.RunBorder_Vertical);
					}
					break;
				case EState.RunBorder_Vertical:
					m_Timer.Run(_dt);
					rate = m_Timer.progress;
					m_Bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(m_Size_Vertical.x, m_Size_Vertical.y, rate));
					if (m_Timer.complete == true) {
						SetState(m_Reverse ? EState.RunBorder_Horizontal : EState.Loop);
					}
					break;
				case EState.Loop:
					break;

				case EState.None:
				default:
					break;
			}

		}


		public void Open()
		{
			SetVisible(true);
			SetInteract(true);
			m_Reverse = false;
			m_Timer.reverse = m_Reverse;
			m_Bar.SetSizeWithCurrentAnchors(new Vector2(m_Size_Horizontal.x, m_Size_Vertical.x));
			SetState(EState.RunBorder_Horizontal);
		}
		public void Close()
		{
			SetVisible(true);
			SetInteract(true);
			m_Reverse = true;
			m_Timer.reverse = m_Reverse;
			m_Bar.SetSizeWithCurrentAnchors(new Vector2(m_Size_Horizontal.y, m_Size_Vertical.y));
			SetState(EState.RunBorder_Vertical);
		}

	}
}