
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// シーン：タイトル
/// </summary>
public class TitleManager : MonoBehaviour
{
	public enum EState
	{
		None,

		Wait,
		Decided,
	}

	/// <summary>
	/// ボタン：スタート
	/// </summary>
	[SerializeField] protected Button m_BtnStart;


	/// <summary>
	/// ステート
	/// </summary>
	protected EState m_State;


	protected void SetState(EState _state)
	{
		m_State = _state;
	}

	/// <summary>
	/// 開始
	/// </summary>
	protected void OnStart()
	{

	}

	void Awake()
	{


		SetState(EState.Wait);
	}
	void Start()
	{
		
	}
	void Update()
	{
		
	}
}
