
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



			return true;
		}

		protected void SetState(EState _state)
		{
			m_State = _state;
		}


		/// <summary>
		/// 接触イベント：障害物がプレイヤーに当たった
		/// </summary>
		protected void OnTriggerEnter_ObstacleToPlayer(ECollisionEvent _event, DynamicObject _obj, Collider _col)
		{
			// 相手はプレイヤーではない
			if (_col.gameObject != m_Player.gameObject) { return; }

			ColoredObject colored = _obj as ColoredObject;
			if (colored == null) { return; }

			// 同じ色
			if (colored.color == m_Player.color) {
				ScoreManager.instance.AddScore(100); // スコア
				Vector3 pos = _obj.transform.position;
				pos += Vector3.up * 2f;
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

			SetState(EState.Load_Wait);
		}
		public override void OnExit()
		{
			base.OnExit();

			foreach (var obj in m_Objects) {
				obj.Dispose();
			}
			m_Objects.Clear();
			m_Objects = null;
		}
		public override void OnUpdate(float _dt)
		{
			base.OnUpdate(_dt);

			switch (m_State) {
				case EState.Load_Wait:
					if (UIManager.instance.loading.isloading == false) {
						SetState(EState.Wait_Start);
					}
					break;

				case EState.Wait_Start:
					SetState(EState.Loop);
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

			if (m_Objects != null) {
				foreach (var obj in m_Objects) {
					obj.OnUpdate(_dt);
				}
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
		}


	}
}