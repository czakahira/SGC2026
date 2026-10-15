
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace SGC2026
{
	/// <summary>
	/// シーン：起動
	/// </summary>
	public class BootManager : GameScene
	{
		public enum EState
		{
			None = -1,

			Wait_Boot,
			Wait_Load,
		}

		private static bool s_Booted = false;
		[SerializeField] protected EState m_State = EState.None;

		private void Start()
		{
			bool isBoot = GameManager.TryInitialize() == true;
			isBoot &= GameSceneManager.TryInitialize() == true;
			isBoot &= UIManager.TryInitialize() == true;
			isBoot &= AudioManager.TryInitialize() == true;

			GameManager.instance.onDispose += AudioManager.Dispose;
			GameManager.instance.onDispose += UIManager.Dispose;
			GameManager.instance.onDispose += GameSceneManager.Dispose;

			s_Booted = isBoot;

			m_State = EState.Wait_Boot;
		}

		private void Update()
		{

			switch (m_State) {

				case EState.Wait_Boot:

					// 準備完了したらタイトルへ
					if (s_Booted == true) {
						//					GameSceneManager.instance.Load("Title");
						GameSceneUtility.LoadSceneAsyncOnFade("Title", _durationOut: 0f, _durationIn: 0.2f).Forget();
						m_State = EState.Wait_Load;
					}
					break;

				case EState.Wait_Load:
					break;

				case EState.None:
				default:
					break;
			}
		}

	}
}