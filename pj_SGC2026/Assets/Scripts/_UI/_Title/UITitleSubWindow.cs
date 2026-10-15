
using UnityEngine;

namespace SGC2026
{
	public class UITitleSubWindow : UIComponent
	{
		// ========================================
		//  Member
		// ========================================	

		/// <summary>
		/// 閉じるボタン
		/// </summary>
		[SerializeField] protected UIButton m_Btn_Close;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected float m_ViewAnimDuration;
		[SerializeField] protected AnimationCurve m_ViewAnimCurve;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected Vector2 m_ClosePosition;

		protected SimpleTimer m_ViewAnimTimer;
		/// <summary>
		/// アニメーション中か？
		/// </summary>
		protected bool m_IsViewAnim = false;

		// ========================================
		//  Property
		// ========================================
		public UIButton btnClose => m_Btn_Close;
		public bool isViewAnim => m_IsViewAnim;

		// ========================================
		//  Method
		// ========================================
		public void Init()
		{
			m_ViewAnimTimer = new SimpleTimer();
			m_ViewAnimTimer.duration = m_ViewAnimDuration;
			m_ViewAnimTimer.reverse = false;

			rectTransform.anchoredPosition = m_ClosePosition;

			m_IsViewAnim = false;
		}

		public override void OnUpdate(float _dt)
		{
			base.OnUpdate(_dt);

			if (m_IsViewAnim == true) {
				m_ViewAnimTimer.Run(_dt);
				float rate = m_ViewAnimCurve.Evaluate(m_ViewAnimTimer.progress);
				UpdateViewAnim(rate);
				if (m_ViewAnimTimer.complete == true) {
					m_IsViewAnim = false;
				}
			}

		}

		protected virtual void UpdateViewAnim(float _progress)
		{
			this.rectTransform.anchoredPosition = Vector2.Lerp(m_ClosePosition, Vector2.zero, _progress);
		}

		public virtual bool Open()
		{
			m_ViewAnimTimer.reverse = false;
			m_IsViewAnim = true;
			return true;
		}
		public virtual void OpenForce()
		{
			UpdateViewAnim(1f);
		}
		public virtual bool Close()
		{
			m_ViewAnimTimer.reverse = true;
			m_IsViewAnim = true;
			return true;
		}
		public virtual void CloseForce()
		{
			UpdateViewAnim(0f);
		}

	}
}