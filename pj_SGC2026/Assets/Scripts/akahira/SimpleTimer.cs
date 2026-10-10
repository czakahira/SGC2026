
using System;
using UnityEngine;

/// <summary>
/// 簡易タイマー
/// </summary>
[Serializable]
public class SimpleTimer
{
	// ========================================
	//  Member
	// ========================================

	/// <summary>
	/// 計測期間 [s]
	/// </summary>
	[SerializeField] protected float m_Duration;


	/// <summary>
	/// 経過期間 [s]
	/// </summary>
	[SerializeField] protected float m_ElapedTime;
	/// <summary>
	/// フラグ：カウントダウン
	/// </summary>
	[SerializeField] protected bool m_Reverse;
	/// <summary>
	/// フラグ：ループ
	/// </summary>
	[SerializeField] protected bool m_Loop;

	protected bool m_IsElapsed = false;

	// ========================================
	//  Property
	// ========================================
	public float duration
	{
		get { return m_Duration; }
		set { m_Duration = value; }
	}
	public bool reverse
	{
		get { return m_Reverse; }
		set { m_Reverse = value; }
	}
	public bool loop
	{
		get { return m_Loop; }
		set { m_Loop = value; }
	}
	/// <summary>
	/// 進捗率
	/// </summary>
	public float progress => (m_ElapedTime / m_Duration);

	

	// ========================================
	//  Method
	// ========================================
	public void Run(float _dt)
	{
		m_IsElapsed = false;

		m_ElapedTime += _dt * (m_Reverse == true ? -1f : 1f);
		m_ElapedTime = Mathf.Clamp(m_ElapedTime, 0, m_Duration);

		bool isElapsedWasFrame = m_Reverse == true ? m_ElapedTime <= 0 : m_ElapedTime >= m_Duration;
		if (isElapsedWasFrame == true) {
			m_IsElapsed = true;

			if (loop == true) {
				Reset();
			}
		}


	}
	public bool IsElapsed()
	{
		return m_IsElapsed;
	}
	public bool Alrm(float _dt)
	{
		Run(_dt);
		return IsElapsed();
	}

	public void SetElapsed(float _value)
	{
		m_ElapedTime = _value;
	}
	/// <summary>
	/// リセット
	/// </summary>
	public void Reset()
	{
		SetElapsed( (m_Reverse == true) ? m_Duration : 0f );
	}
	

}