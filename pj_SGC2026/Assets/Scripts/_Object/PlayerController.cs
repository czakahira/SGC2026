
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SGC2026
{
	/// <summary>
	/// キャラクター：プレイヤー
	/// </summary>
	public class PlayerController : DynamicObject
	{
		public enum EState
		{
			None = 0,

			Idle = 1 << 0,
			Run = 1 << 1,
			Jump = 1 << 2,

			Death = 1 << 3,

			All = -1
		}

		// ========================================
		//  Member
		// ========================================
		#region open

		/// <summary>
		/// 入力
		/// </summary>
		[SerializeField] protected PlayerInput m_Input;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected CapsuleCollider m_CapsuleCollider;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected Rigidbody m_Rigidbody;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected Animator m_Anim;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected Renderer[] m_Renderers;
		/// <summary>
		/// 
		/// </summary>
		protected List<Material> m_Materials;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected EState m_State = EState.None;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected EColorType m_Color;
		/// <summary>
		/// 予約されたステート
		/// </summary>
		[SerializeField] protected EState m_StateReserve = EState.None;
		/// <summary>
		/// 自前の速度
		/// </summary>
		protected Vector3 m_Velocity;
		// ------------------------------
		//  接地
		// ------------------------------
		[SerializeField] protected float m_CheckDistance;
		[SerializeField] protected float m_CheckRadius;
		protected Vector3 m_CheckOrigin;
		protected Vector3 m_CheckDist;
		protected int m_HitCnt;
		protected RaycastHit[] m_Hits;
		protected bool m_Grounded;
		// ------------------------------
		//  移動
		// ------------------------------
		private bool m_CanMove = true;
		/// <summary>
		/// 最大速度
		/// </summary>
		[SerializeField] private float m_MaxSpeed = 5f;
		/// <summary>
		/// 加速度
		/// </summary>
		[SerializeField] private float m_Acceleration = 2f;
		/// <summary>
		/// 現行速度
		/// </summary>
		private float m_CurrentSpeed = 0f;
		// ------------------------------
		//  ジャンプ
		// ------------------------------
		[SerializeField] private float m_JumpForce = 7f;
		[SerializeField] private float m_FallMultiplier = 2.5f;
		[SerializeField] private int m_MaxJumpCount = 2;
		private int m_CurrentJumpCount;
		#endregion

		// ========================================
		//  Property
		// ========================================

		public EColorType color => m_Color;
		public Rigidbody rb => m_Rigidbody;
		public Collider collider => m_CapsuleCollider;
		public bool isGrounded => m_Grounded;


		// ========================================
		//  Method
		// ========================================
		#region open
		public void SetState(EState _state)
		{
			m_State = _state;
			switch (m_State) {

				case EState.Idle:
					m_Anim.Play("Run");
					break;

				case EState.Run:
					m_Anim.Play("Run");
					break;

				case EState.Jump:
					break;

				case EState.Death:
					m_Anim.Play("Damage");
					break;

				case EState.None:
				default:
					break;

			}
		}
		public bool IsState(EState _state)
		{
			return m_State == _state;
		}
		public void RejistState(EState _state)
		{
			m_StateReserve |= _state;
		}
		/// <summary>
		/// 色の変更
		/// </summary>
		public void SetColor(EColorType _color)
		{
			m_Color = _color;

			Color targetColor = Color.white;
			switch (_color) {
				case EColorType.Default:
					targetColor = Color.white;
					break;
				case EColorType.Red:
					targetColor = Color.red;
					break;
				case EColorType.Blue:
					targetColor = Color.blue;
					break;
				case EColorType.Green:
					targetColor = Color.green;
					break;
			}

			// ★ マテリアルが複数あるので全部変更する
			foreach (var mat in m_Materials) {
				// URP Lit シェーダー対応
				if (mat.HasProperty("_BaseColor")) {
					mat.SetColor("_BaseColor", targetColor);
				}
				else {
					mat.color = targetColor;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		protected void ChangeColorForInput()
		{
			EColorType clr = m_Color;

			if (m_Input.actions["Change_Red"].IsPressed() == true) {
				clr = EColorType.Red;
			}
			else if (m_Input.actions["Change_Blue"].IsPressed() == true) {
				clr = EColorType.Blue;
			}
			else if (m_Input.actions["Change_Green"].IsPressed() == true) {
				clr = EColorType.Green;
			}
			else {
				clr = EColorType.Default;
			}

			if (clr != m_Color) {
				SetColor(clr);
			}
		}

		/// <summary>
		/// 接地チェック
		/// </summary>
		protected void GroundCheck()
		{
			bool result = false;

			m_CheckOrigin = m_Rigidbody.position;
			Vector3 dir = this.transform.rotation * Vector3.down;
			m_CheckDist = m_CheckOrigin + (dir * m_CheckDistance);

			m_HitCnt = Physics.SphereCastNonAlloc(m_CheckOrigin, m_CheckRadius, dir, m_Hits, m_CheckDistance);
			if (m_HitCnt > 0) {
				for (int i = 0; i < m_HitCnt; i++) {
					if (m_Hits[i].collider.CompareTag("ground") == true) {
						result = true;
						break;
					}
				}
			}

			if (m_Grounded != result) {
				OnGround(result);
			}
			m_Grounded = result;
		}
		protected void OnGround(bool _grounded)
		{
			if (_grounded == true) {
				m_CurrentJumpCount = 0;
			}
		}

		// ------------------------------
		//  移動
		// ------------------------------
		/// <summary>
		/// 移動
		/// </summary>
		protected void Move(float _dt)
		{
			if (m_CanMove == false) { return; }

			// 徐々にスピードを増やす
			m_CurrentSpeed += m_Acceleration * _dt;

			// 最大速度制限
			m_CurrentSpeed = Mathf.Clamp(m_CurrentSpeed, 0f, m_MaxSpeed);

			// 速度を反映
			Vector3 vel = m_Velocity;
			vel.x = m_CurrentSpeed;
			m_Velocity = vel;
		}
		public void StopMove()
		{
//			m_CanMove = false;

			m_CurrentSpeed = 0f;

			m_Velocity = Vector3.zero;
			m_Rigidbody.linearVelocity = Vector3.zero;
			m_Rigidbody.angularVelocity = Vector3.zero;
		}
		public void ResetSpeed()
		{
			m_CurrentSpeed = 0f;
		}

		// ------------------------------
		//  ジャンプ
		// ------------------------------
		protected void TryJump()
		{
			if (m_CurrentJumpCount < m_MaxJumpCount) {
				m_Velocity.y = m_JumpForce;
				m_CurrentJumpCount++;
			}
		}

		// ------------------------------
		//  リスポーン
		// ------------------------------

		public void Respawn(Vector3 _pos)
		{
			StopMove();

//			m_Rigidbody.MovePosition(_pos);
			transform.position = _pos;
		}

		// ------------------------------
		//  etc
		// ------------------------------
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();

#if false
			// 入力
			if (m_Input != null) {
				// 色変更
				m_Input.actions["Change_Red"].started += ctx => SetColor(EColorType.Red);
				m_Input.actions["Change_Blue"].started += ctx => SetColor(EColorType.Blue);
				m_Input.actions["Change_Green"].started += ctx => SetColor(EColorType.Green);
				// 色戻す
				m_Input.actions["Change_Red"].canceled += ctx => SetColor(EColorType.Default);
				m_Input.actions["Change_Blue"].canceled += ctx => SetColor(EColorType.Default);
				m_Input.actions["Change_Green"].canceled += ctx => SetColor(EColorType.Default);
			}
#endif
			// マテリアルの複製をキャッシュ
			m_Renderers = this.GetComponentsInChildren<Renderer>();
			m_Materials = new List<Material>(10);
			foreach (var rdr in m_Renderers) {
				foreach (var mat in rdr.materials) {
					m_Materials.Add(mat);
				}
			}

			m_Hits = new RaycastHit[3];

			// 白から
			SetColor(EColorType.Default);

			m_StateReserve = EState.None;

			SetState(EState.Idle);
		}
		public override void Dispose()
		{
			base.Dispose();

			// マテリアルの複製を廃棄
			foreach (var mat in m_Materials) {
				Material.Destroy(mat);
			}
			m_Materials.Clear();
			m_Materials = null;

			m_Renderers = null;

			SetState(EState.None);
		}
		public override void OnUpdate(float _dt)
		{

			m_Anim.SetBool("IsGrounded", m_Grounded);

			switch (m_State) {

				case EState.Idle:

					break;

				case EState.Run:
					ChangeColorForInput();

					if (m_Input.actions["Jump"].WasPressedThisFrame() == true) {
						RejistState(EState.Jump);
					}
					break;

				case EState.Jump:
					ChangeColorForInput();

					if (m_Input.actions["Jump"].WasPressedThisFrame() == true) {
						RejistState(EState.Jump);
					}
					break;

				case EState.None:
				default:
					break;
			}

		}
		public override void OnFixedUpdate(float _dt)
		{
			// 速度をキャッシュ
			m_Velocity = m_Rigidbody.linearVelocity;

			// 接地判定
			GroundCheck();

			// ジャンプ
			if (m_StateReserve.HasFlag(EState.Jump) == true) {
				TryJump();
			}

			switch (m_State) {

				case EState.Idle:

					break;

				case EState.Run:
					// 移動
					Move(_dt);
					break;

				case EState.Jump:
					// 移動
					Move(_dt);
					break;

				case EState.Death:
					StopMove();
					break;

				case EState.None:
				default:
					break;
			}

			if (m_Velocity.y < 0) {
				m_Velocity += Vector3.up * Physics.gravity.y * (m_FallMultiplier - 1f) * _dt;
			}
			// 予約を削除
			m_StateReserve = EState.None;
			// 速度を反映
			m_Rigidbody.linearVelocity = m_Velocity;
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.lightGreen;
			Gizmos.DrawWireSphere(m_CheckDist, m_CheckRadius);
		}
		#endregion

	}
}