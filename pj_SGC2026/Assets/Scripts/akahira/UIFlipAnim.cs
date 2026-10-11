
using UnityEngine;
using UnityEngine.UI;

public class UIFlipAnim : MonoBehaviour
{
	/// <summary>
	/// パラパラ
	/// </summary>
	[SerializeField] protected Sprite[] m_Flips;
	/// <summary>
	/// 
	/// </summary>
	[SerializeField] protected float m_Duration;
	[SerializeField] protected Image m_Image;
	/// <summary>
	/// 
	/// </summary>
	[SerializeField] protected int m_Idx;

	protected SimpleTimer m_Timer;

	public void Init()
	{
		m_Timer = new SimpleTimer();

		SetDuration(m_Duration);

		SetIdx(0);
	}

	public void UpdateAnim(float _dt)
	{
		if (m_Duration <= 0f) { return; }

		m_Timer.Run(_dt);
		if (m_Timer.IsElapsed() == true) {
			NextIdx();
		}
	}

	public void SetDuration(float _value)
	{
		m_Duration = _value;
		m_Timer.duration = m_Duration;
	}

	public void SetIdx(int _idx)
	{
		m_Idx = Mathf.Clamp(_idx, 0, m_Flips.Length - 1);

		m_Image.sprite = m_Flips[m_Idx];

		m_Timer.Reset();
	}
	protected void NextIdx()
	{
		int idx = m_Idx + 1;
		if (idx >= m_Flips.Length) {
			idx = 0;
		}
		SetIdx(idx);
	}

}
