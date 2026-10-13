
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
	[SerializeField] protected float m_ElapsedTime;
	/// <summary>
	/// フラグ：カウントダウン
	/// </summary>
	[SerializeField] protected bool m_Reverse;
	/// <summary>
	/// フラグ：ループ
	/// </summary>
	[SerializeField] protected bool m_Loop;
	/// <summary>
	/// 
	/// </summary>
	protected bool m_Complete = false;

	// ========================================
	//  Property
	// ========================================
	public float duration
	{
		get { return m_Duration; }
		set { m_Duration = value; }
	}
	public float elapsedTime => m_ElapsedTime;
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
	public float progress => (m_ElapsedTime / m_Duration);
	public bool complete => m_Complete;

	

	// ========================================
	//  Method
	// ========================================
	public void Run(float _dt)
	{
		m_Complete = false;

		m_ElapsedTime += _dt * (m_Reverse == true ? -1f : 1f);
		m_ElapsedTime = Mathf.Clamp(m_ElapsedTime, 0, m_Duration);

		bool isElapsedWasFrame = m_Reverse == true ? m_ElapsedTime <= 0 : IsElapsed(m_Duration);
		if (isElapsedWasFrame == true) {
			m_Complete = true;

			if (loop == true) {
				Reset();
			}
		}


	}
	/// <summary>
	/// 指定期間経過したか
	/// </summary>
	public bool IsElapsed(float _value)
	{
		return m_ElapsedTime >= _value;
	}
	public bool Alrm(float _dt)
	{
		Run(_dt);
		return complete;
	}

	public void SetElapsed(float _value)
	{
		m_ElapsedTime = _value;
	}
	/// <summary>
	/// リセット
	/// </summary>
	public void Reset()
	{
		SetElapsed( (m_Reverse == true) ? m_Duration : 0f );
	}
	

}