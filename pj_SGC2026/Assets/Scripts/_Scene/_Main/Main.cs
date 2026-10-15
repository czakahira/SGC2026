
using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;

namespace SGC2026
{
	/// <summary>
	/// シーン：メイン
	/// </summary>
	public partial class Main : GameScene
	{
		public enum EState
		{
			None = -1,

			// --------------------
			//  シーンロード待ち
			// --------------------
			Load_Wait,

			// --------------------
			//  初期化待ち
			// --------------------
			Init,

			// --------------------
			//  すたーと
			// --------------------
			Start,

			// --------------------
			//  メインループ
			// --------------------
			Loop,

			// --------------------
			//  死亡
			// --------------------
			Death,

			// --------------------
			//  リスポーン
			// --------------------
			Respawn,

			// --------------------
			//  リザルト
			// --------------------
			Result,
		}

		// ========================================
		//  Member
		// ========================================
		/// <summary>
		/// エフェクト：スコア
		/// </summary>
		[SerializeField] protected GameObject m_Effect_Score;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected Camera m_Camera;
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] protected CameraFollow m_CameraFollow;
		/// <summary>
		/// シーン上に存在する動的オブジェクト一覧
		/// </summary>
		[SerializeField] private List<DynamicObject> m_Objects;
		/// <summary>
		/// 一度当たった障害物
		/// <para> 通過直後に色が変わってあたったことにならないようにするための保険 </para>
		/// </summary>
		[SerializeField] private List<ColoredObject> m_TriggeredObstales;
		/// <summary>
		/// プレイヤー
		/// </summary>
		[SerializeField] protected PlayerController m_Player;
		/// <summary>
		/// クリア
		/// </summary>
		protected ClearObject m_Clear;

		/// <summary>
		/// UI：リザルト
		/// </summary>
		[SerializeField] protected ResultPanel m_Result;

		/// <summary>
		/// 
		/// </summary>
		protected EState m_State;

		// ========================================
		//  Method
		// ========================================
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

			ClearObject clear = _obj as ClearObject;
			if (clear != null) {
				m_Clear = clear;
				m_Clear.collision.onTrigger += OnTriggerEnter_Clear;
			}

			CheckPointObject checkPoint = _obj as CheckPointObject;
			if (checkPoint != null) {
				checkPoint.onCheckPoint += OnTriggerEnter_CheckPoint;
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
		/// <summary>
		/// ステート変更
		/// </summary>
		protected void SetState(EState _state)
		{
			// ▼ 退室
			switch (m_State) {
				case EState.Load_Wait:
					break;

				case EState.Init:
					break;

				case EState.Start:
					break;

				case EState.Loop:
					break;

				case EState.Death:
					IsProcessingDeath = false;
					break;

				case EState.Respawn:
					break;

				case EState.Result:
					break;

				case EState.None:
				default:
					break;
			}
			// ▼ 入室
			Debug.LogError($"{m_State} >> {_state}");
			m_State = _state;
			switch (m_State) {
				case EState.Load_Wait:
					break;

				case EState.Init:
					// 残機初期化
					SetLife(3);
					// カメラ追従
					m_CameraFollow.SetTarget(m_Player.transform);
					// 操作ガイド
					UIManager.instance.guide.Open(4);
					break;

				case EState.Start:
					Ready_GameStart();
					break;

				case EState.Loop:
					m_TriggeredObstales.Clear();

					// 走り出す
					m_Player.SetState(PlayerController.EState.Run);
					break;

				case EState.Death:
					break;

				case EState.Respawn:
					break;

				case EState.Result:
					// 操作ガイド
					UIManager.instance.guide.Close(4);
					// 
					m_Player.SetState(PlayerController.EState.Idle);
					break;

				case EState.None:
				default:
					break;
			}
		}
		protected bool IsState(EState _state)
		{
			return m_State == _state;
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
				if (IsState(EState.Loop) == false) { return; }
				if (m_TriggeredObstales.Contains(_obj) == true) { return; }

				StartDeath();
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
				// スコア加算
				ScoreManager.instance.AddScore(100);
				// エフェクト
				Vector3 pos = _obj.transform.position;
				pos += Vector3.up * 6f;
				GameObject.Instantiate(m_Effect_Score, pos, Quaternion.identity);
				// 保管
				m_TriggeredObstales.Add(_obj);
			}
		}
		/// <summary>
		/// ゴールにタッチした
		/// ClearObject より
		/// </summary>
		protected void OnTriggerEnter_Clear(ECollisionEvent _event, Collider _other)
		{
			if (_event != ECollisionEvent.TriggerEnter) { return; }

			if (IsState(EState.Result) == true) { return; }

			if (_other.CompareTag("Player") == false) { return; }

			GameClear();
		}
		/// <summary>
		/// チェックポイント
		/// </summary>
		protected void OnTriggerEnter_CheckPoint(CheckPointObject _obj, Collider _other)
		{
			if (_other.CompareTag("Player") == false) { return; }

			// チェックポイントを更新
			Vector3 pos = _obj.transform.position;
			pos.y += _obj.respawnHeightOffset; // ★ 高さを少し上げる
			SetCheckPoint(pos);
		}

		/// <summary>
		/// ボタンクリック：リザルトパネルの「タイトルへ」
		/// </summary>
		protected void OnClick_ResultToTitle(UIBehaviour _ui, PointerEventData _data)
		{
			// スコアリセット
			ScoreManager.instance.ResetScore();
			// タイトルへ戻る
			GameSceneUtility.LoadSceneAsyncOnLoading("Title").Forget();

			SetState(EState.None);
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

			m_DeathTimer = new SimpleTimer();

			m_TriggeredObstales = new List<ColoredObject>(10);

			m_CameraFollow.Initialize();
			m_BackgroundLoop.Initialize();

			m_Result.btnTItle.onClick += OnClick_ResultToTitle;
			m_Result.SetActive(false);

			Init_GameStart();

			// 立ち止まる
			m_Player.SetState(PlayerController.EState.Idle);
			
			m_StartPos = m_Player.transform.position;
			SetCheckPoint(m_StartPos);

			SetState(EState.Load_Wait);
		}
		public override void OnExit()
		{
			base.OnExit();

			m_DeathTimer = null;

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
			m_BackgroundLoop.OnUpdate(_dt);

			switch (m_State) {
				case EState.Load_Wait:
					// ロード完了を待つ
					if (UIManager.instance.loading.isloading == false) {
						SetState(EState.Init);
					}
					break;

				case EState.Init:
					SetState(EState.Start);
					break;

				case EState.Start:
					Update_GameStart(_dt);
					break;

				case EState.Loop:
					// 落下４チェック
					Check_FallDeath();
					break;

				case EState.Death:
					m_DeathTimer.Run(_dt);
					if (m_DeathTimer.complete == true) {
						// 残機が残っているなら
						if (IsAlive() == true) {
							// リスポーン
							Respawn();
						} else {
							// ゲームオーバー
							GameOver();
						}
					}
					break;

				case EState.Respawn:
					SetState(EState.Loop);
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

			if (IsState(EState.Loop) == true) {
				m_CameraFollow.UpdateFollow();
			}

		}


	}
}