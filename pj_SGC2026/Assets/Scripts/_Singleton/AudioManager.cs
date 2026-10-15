
using UnityEngine;

namespace SGC2026
{
	public enum EBGM
	{
		None = -1,

		Title,

		Main,

		Goal,

		GameOver,
	}

	public enum ESE
	{
		None = -1,

		Decide_12,
		Decide_22,
		Decide_26,
		Decide_31,
		Decide_39,
		Decide_42,
		Decide_45,
		Decide_53,
	}

	/// <summary>
	/// マネージャー：サウンド
	/// </summary>
	public class AudioManager : SingletonBehaviour<AudioManager>
	{
		/// <summary>
		/// 音源：BGN
		/// </summary>
		[SerializeField] protected AudioSource[] m_Source_Bgms;
		/// <summary>
		/// 音源：SE
		/// </summary>
		[SerializeField] protected AudioSource[] m_Source_Ses;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected AudioClip[] m_Clip_Ses;

		/// <summary>
		/// 
		/// </summary>
		protected static int s_CurrentIdx;

		/// <summary>
		/// BGM再生
		/// </summary>
		public static void PlayBGN(EBGM _bgm)
		{
			AudioSource audio = instance.m_Source_Bgms[(int)_bgm];
			if (audio.clip == null) { return; }

			audio.time = 0f;
			audio.Play();
		}
		/// <summary>
		/// BGM停止
		/// </summary>
		public static void StopBGN(EBGM _bgm)
		{
			AudioSource audio = instance.m_Source_Bgms[(int)_bgm];
			if (audio.clip == null) { return; }

			audio.Stop();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_se"></param>
		/// <param name="_idx"></param>
		public static void PlaySE(ESE _se, int _idx)
		{
			AudioClip clip = instance.m_Clip_Ses[(int)_se];
			AudioSource audio = instance.m_Source_Ses[_idx];
			audio.clip = clip;
			audio.time = 0f;
			audio.Play();
		}
		public static void PlaySE(ESE _se)
		{
			if (s_CurrentIdx >= instance.m_Source_Ses.Length) {
				s_CurrentIdx = 0;
			}

			PlaySE(_se, s_CurrentIdx);
			s_CurrentIdx++;
		}

	}
}