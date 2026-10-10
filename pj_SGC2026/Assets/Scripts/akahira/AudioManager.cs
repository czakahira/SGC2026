
using UnityEngine;

public enum EBGM
{
	None = -1,

	Title,

	Main,
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

}