
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SGC2026
{
	/// <summary>
	/// シーン：メイン
	/// </summary>
	public class Main : GameScene
	{
		public enum EState
		{
			None = -1,

			Load_Wait,

			Wait_Start,
			Wait_Loop,
			Wait_End,

			Loop,

			Resawn_Start,
			Resawn_Loop,
			Resawn_End,

			Result,
		}


		/// <summary>
		/// エフェクト：スコア
		/// </summary>
		[SerializeField] protected GameObject m_Effect_Score;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected CameraFollow m_CameraFollow;
		/// <summary>
		/// 
		/// </summary>
		protected EState m_State;

		/// <summary>
		/// 
		/// </summary>
		[SerializeField] private List<DynamicObject> m_Objects;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected PlayerController m_Player;

		public bool Add(DynamicObject _obj)
		{
			if (m_Objects == null) { return false; }
			if (m_Objects.Contains(_obj) == true) { return false; }

			m_Objects.Add(_obj);
			_obj.Initialize();

			PlayerController player = _obj as PlayerController;
			if (player != null) {
				m_Player = player;
			}

			ColoredObject clrd = _obj as ColoredObject;
			if (clrd != null) {
				clrd.onFixedUpdateObjected += OnFixedUpdate_ColoredObject;
			}

			ColoredObstacles obstacle = _obj as ColoredObstacles;
			if (obstacle != null) {
				obstacle.onCollisionColored += OnCollisionEnter_ObstacleToPlayer;
				obstacle.onTriggerScore += OnTriggerEnter_ScoreObstacleToPlayer;
			}

			return true;
		}

		protected void SetState(EState _state)
		{
			m_State = _state;
			switch (m_State) {
				case EState.Load_Wait:
					break;

				case EState.Wait_Start:
					m_CameraFollow.SetTarget(m_Player.transform);
					break;
				case EState.Wait_Loop:
					break;
				case EState.Wait_End:
					break;

				case EState.Loop:
					break;

				case EState.Resawn_Start:
					break;
				case EState.Resawn_Loop:
					break;
				case EState.Resawn_End:
					break;

				case EState.Result:
					break;

				case EState.None:
				default:
					break;
			}
		}

		/// <summary>
		/// プレイヤーと色付きオブジェクトの色が一致している時は、すり抜ける
		/// </summary>
		protected void OnFixedUpdate_ColoredObject(float _dt, ColoredObject _obj)
		{
			if (m_Player == null) { return; }

			bool sameColor = (_obj.color == m_Player.color);

			// 壁本体との衝突だけを無効化
			Physics.IgnoreCollision(m_Player.collider, _obj.collider, sameColor);
		}
		/// <summary>
		/// 接触イベント：障害物がプレイヤーぶつかった
		/// </summary>
		protected void OnCollisionEnter_ObstacleToPlayer(ECollisionEvent _event, ColoredObject _obj, Collision _other)
		{
			if (_event != ECollisionEvent.CollisionEnter) { return; }

			// 相手はプレイヤーではない
			if (_other.gameObject != m_Player.gameObject) { return; }

			// 色が違う
			if (_obj.color != m_Player.color) {
				PlayerLife.instance.ReduceLife();

				var respawn = _other.gameObject.GetComponent<PlayerRespawn>();
				respawn?.Respawn();
			}
		}
		/// <summary>
		/// 接触イベント：障害物(スコア)がプレイヤーに当たった
		/// </summary>
		protected void OnTriggerEnter_ScoreObstacleToPlayer(ECollisionEvent _event, ColoredObject _obj, Collider _col)
		{
			if (_event != ECollisionEvent.TriggerEnter) { return; }

			// 相手はプレイヤーではない
			if (_col.gameObject != m_Player.gameObject) { return; }

			// 同じ色
			if (_obj.color == m_Player.color) {
				ScoreManager.instance.AddScore(100); // スコア
				Vector3 pos = _obj.transform.position;
				pos += Vector3.up * 6f;
				GameObject.Instantiate(m_Effect_Score, pos, Quaternion.identity);
			}
		}

		public override void OnEnter()
		{
			base.OnEnter();

			m_Objects = new List<DynamicObject>(50);

			// 既に配置されているオブジェクトを検索
			DynamicObject[] objectsOnScene = Component.FindObjectsByType<DynamicObject>(FindObjectsSortMode.None);
			foreach (var obj in objectsOnScene) {
				Add(obj);
			}

			m_CameraFollow.Initialize();

			SetState(EState.Load_Wait);
		}
		public override void OnExit()
		{
			base.OnExit();

			m_Player = null;

			foreach (var obj in m_Objects) {
				obj.Dispose();
			}
			m_Objects.Clear();
			m_Objects = null;
		}
		public override void OnUpdate(float _dt)
		{
			base.OnUpdate(_dt);

			// オブジェクト更新
			if (m_Objects != null) {
				foreach (var obj in m_Objects) {
					obj.OnUpdate(_dt);
				}
			}

			switch (m_State) {
				case EState.Load_Wait:
					if (UIManager.instance.loading.isloading == false) {
						SetState(EState.Wait_Start);
					}
					break;

				case EState.Wait_Start:
					SetState(EState.Wait_Loop);
					break;
				case EState.Wait_Loop:
					SetState(EState.Wait_End);
					break;
				case EState.Wait_End:
					SetState(EState.Loop);
					break;

				case EState.Loop:
					break;

				case EState.Resawn_Start:
					break;
				case EState.Resawn_Loop:
					break;
				case EState.Resawn_End:
					break;

				case EState.Result:
					break;

				case EState.None:
				default:
					break;
			}

		}
		public override void OnFixedUpdate(float _dt)
		{
			base.OnFixedUpdate(_dt);

			if (m_Objects != null) {
				foreach (var obj in m_Objects) {
					obj.OnFixedUpdate(_dt);
				}
			}
		}
		public override void OnLateUpdate(float _dt)
		{
			base.OnLateUpdate(_dt);

			if (m_Objects != null) {
				foreach (var obj in m_Objects) {
					obj.OnLateUpdate(_dt);
				}
			}

			m_CameraFollow.UpdateFollow();

		}


	}
}