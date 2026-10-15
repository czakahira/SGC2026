
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SGC2026
{
	public partial class Main
	{
		[SerializeField] private UIGameStart m_GameStart;
		[SerializeField] private SimpleTimer m_GameStartTimer;
		private int m_GameStartCount;

		[SerializeField] private float _countSeconds = 1f;
		[SerializeField] private float _startSeconds = 0.8f;
		[SerializeField] private float _entranceSeconds = 1.2f;


		protected void Init_GameStart()
		{
			m_GameStartTimer = new SimpleTimer();

			// ★ カウントダウン中のカメラ位置を固定 
			Vector3 camPos = new Vector3(
				-15.5f,
				5f,								// Y = 5
				m_Camera.transform.position.z	// Z はそのまま
			);
			m_Camera.transform.position = camPos;
		}
		protected void Ready_GameStart()
		{
			m_GameStartTimer.duration = _countSeconds;
			m_GameStartCount= 4;
			m_GameStart.SetImage(m_GameStartCount);
			m_GameStart.SetActive(true);
		}
		protected void Update_GameStart(float _dt)
		{
			m_GameStartTimer.Run(_dt);
			if (m_GameStartTimer.complete == true) {
				if (m_GameStartCount <= 0) {
					m_GameStart.SetActive(false);
					SetState(EState.Loop);
				} else {
					m_GameStartCount--;
					m_GameStart.SetImage(m_GameStartCount);
					m_GameStartTimer.Reset();
				}
			}
		}

	}
}