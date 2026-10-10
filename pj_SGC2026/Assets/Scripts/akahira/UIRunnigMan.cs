
using System;
using UnityEngine;

/// <summary>
/// UI：走る人
/// </summary>
public class UIRunnigMan : UIComponent
{
	[Serializable]
	public class WayData
	{

		public RectTransform target;
		/// <summary>
		/// 期間
		/// </summary>
		public float duration;
	}

	[SerializeField] protected WayData[] m_WayDatas;

	[SerializeField] protected int m_StartWayIdx;
	[SerializeField] protected int m_GoalWayIdx;

	protected WayData m_StartWayData;
	protected WayData m_GoalWayData;
	protected SimpleTimer m_WayPassTimer;

	protected SimpleTimer m_RotateTimer;
	[SerializeField] protected float m_RotateDuration;

	[SerializeField] protected RectTransform m_Ningen;
	protected SimpleTimer m_SwayTimer;
	[SerializeField] protected float m_SwayDuration;
	[SerializeField] protected Vector3 m_Start_Euler;
	[SerializeField] protected Vector3 m_Goal_Euler;

	protected override void Awake()
	{
		base.Awake();

		m_WayPassTimer = new SimpleTimer();
		m_WayPassTimer.loop = false;
		m_WayPassTimer.reverse = false;

		m_RotateTimer = new SimpleTimer();
		m_RotateTimer.loop = false;
		m_RotateTimer.reverse = false;
		m_RotateTimer.duration = m_RotateDuration;

		m_SwayTimer = new SimpleTimer();
		m_SwayTimer.loop = false;
		m_SwayTimer.reverse = false;
		m_SwayTimer.duration = m_SwayDuration;

		SetWayIdx(0);
	}
	protected override void EachUpdate(float _dt)
	{
		base.EachUpdate(_dt);

		// Y軸回転
		Quaternion rot = this.rectTransform.localRotation;
		m_RotateTimer.Run(_dt);
		this.rectTransform.localRotation = Quaternion.Lerp(m_StartWayData.target.localRotation, m_GoalWayData.target.localRotation, m_RotateTimer.progress);

		// 走る
		m_WayPassTimer.Run(_dt);
		this.rectTransform.anchoredPosition = Vector2.Lerp(
			m_StartWayData.target.anchoredPosition,
			m_GoalWayData.target.anchoredPosition,
			m_WayPassTimer.progress
		);
		if (m_WayPassTimer.IsElapsed() == true) {
			SetWayIdx(++m_StartWayIdx);
		}

		// ゆらゆら
		m_SwayTimer.Run(_dt);
		m_Ningen.localRotation = Quaternion.Lerp(Quaternion.Euler(m_Start_Euler), Quaternion.Euler(m_Goal_Euler), m_SwayTimer.progress);
		if (m_SwayTimer.IsElapsed() == true) {
			m_SwayTimer.reverse = m_SwayTimer.reverse == false;
		}

	}

	protected void SetWayIdx(int _idx)
	{
		m_StartWayIdx = LoopIdx(_idx);
		m_GoalWayIdx = LoopIdx(m_StartWayIdx + 1);

		m_StartWayData = m_WayDatas[m_StartWayIdx];
		m_GoalWayData = m_WayDatas[m_GoalWayIdx];

		m_WayPassTimer.duration = m_StartWayData.duration;
		m_WayPassTimer.Reset();


		m_RotateTimer.Reset();
	}

	protected int LoopIdx(int _idx)
	{
		int idx = _idx;
		if (idx >= m_WayDatas.Length) { idx = 0; }
		if (idx < 0) { idx = m_WayDatas.Length - 1; }
		return idx;
	}

}