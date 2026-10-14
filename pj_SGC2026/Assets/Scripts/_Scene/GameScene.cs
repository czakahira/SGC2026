
using UnityEngine;

/// <summary>
/// シーン：基底
/// </summary>
public class GameScene : MonoBehaviour
{
	/// <summary>
	/// 入室
	/// </summary>
	public virtual void OnEnter()
	{

	}
	/// <summary>
	/// 退室
	/// </summary>
	public virtual void OnExit()
	{


	}
	public virtual void OnUpdate(float _dt)
	{

	}
	public virtual void OnFixedUpdate(float _dt)
	{

	}
	public virtual void OnLateUpdate(float _dt)
	{

	}

}