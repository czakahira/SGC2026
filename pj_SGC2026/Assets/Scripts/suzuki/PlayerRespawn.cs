using UnityEngine;

/// <summary>
/// 落下判定、死亡アニメーション、初期地点への復帰を管理します。
/// </summary>
public class PlayerRespawn : MonoBehaviour
{
    public static PlayerRespawn instance;

    private Vector3 _startPos;
    private Vector3 _checkpointPosition;
    public float fallThreshold = -5f;

    [Header("復活地点")]
    [Tooltip("OFF = ゲーム開始時の初期地点。ON = 最後に通過したチェックポイント")]
    [SerializeField] private bool _respawnAtCheckpoint = false;

    [Header("死亡・復活アニメーション")]
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private string _deathTriggerName = "Die";
    [SerializeField] private string _respawnTriggerName = "Respawn";

    [Header("死亡中に停止する移動スクリプト（任意）")]
    [SerializeField] private MonoBehaviour _movementScript;

    [Header("背景スクロール")]
    [SerializeField] private BackgroundLoop _backgroundLoop;

    private Rigidbody _rigidbody;
    private bool _isPlayingDeath;
    private bool _wasKinematic;
    private bool _wasMovementEnabled;

    private void Awake()
    {
        instance = this;
        _rigidbody = GetComponent<Rigidbody>();

        // Inspectorで未設定でも自動でシーン内の背景管理スクリプトを探す。
        if (_backgroundLoop == null)
        {
            _backgroundLoop = FindFirstObjectByType<BackgroundLoop>();
        }
    }

    private void Start()
    {
        _startPos = transform.position;
        _checkpointPosition = _startPos;
    }

    private void Update()
    {
        if (PlayerLife.instance == null || PlayerLife.instance.IsProcessingDeath)
        {
            return;
        }

        if (transform.position.y < fallThreshold)
        {
            // ここでは位置を変更しない。演出と待機の開始だけ。
            PlayerLife.instance.ReduceLife();
        }
    }

    /// <summary>
    /// 復活先チェックポイントを変更します。
    /// </summary>
    public void SetCheckpoint(Vector3 pos)
    {
        _checkpointPosition = pos;
        Debug.Log("チェックポイント更新: " + pos);
    }

    /// <summary>
    /// やられ演出を開始し、移動・落下を止めます。位置は移動しません。
    /// </summary>
    public void PlayDeathAnimation()
    {
        if (_isPlayingDeath)
        {
            return;
        }

        _isPlayingDeath = true;

        if (_movementScript != null)
        {
            _wasMovementEnabled = _movementScript.enabled;
            _movementScript.enabled = false;
        }

        if (_rigidbody != null)
        {
            _wasKinematic = _rigidbody.isKinematic;

            if (!_rigidbody.isKinematic)
            {
                _rigidbody.linearVelocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }

            _rigidbody.isKinematic = true;
        }

        if (_playerAnimator != null)
        {
            _playerAnimator.SetTrigger(_deathTriggerName);
        }
    }

    /// <summary>
    /// 外部からの即時復活要求。死亡演出の待機中は位置移動を禁止します。
    /// </summary>
    public void Respawn()
    {
        if (PlayerLife.instance != null && PlayerLife.instance.life <= 0)
        {
            return;
        }

        if (_isPlayingDeath || (PlayerLife.instance != null && PlayerLife.instance.IsProcessingDeath))
        {
            Debug.Log("[PlayerRespawn] やられ演出中の Respawn() 呼び出しを無視しました。");
            return;
        }

        MoveToRespawnPosition();
    }

    /// <summary>
    /// PlayerLife の待機終了時にだけ呼ぶ、死亡演出完了後の復活です。
    /// </summary>
    public void CompleteRespawnAfterDeath()
    {
        if (PlayerLife.instance != null && PlayerLife.instance.life <= 0)
        {
            return;
        }

        MoveToRespawnPosition();
    }

    private void MoveToRespawnPosition()
    {
        Vector3 position = _respawnAtCheckpoint ? _checkpointPosition : _startPos;
        transform.position = position;

        // プレイヤーが復活するとき、背景もゲーム開始時の位置に戻す。
        if (_backgroundLoop != null)
        {
            _backgroundLoop.ResetBackgroundPosition();
        }

        if (_rigidbody != null)
        {
            if (_isPlayingDeath)
            {
                _rigidbody.isKinematic = _wasKinematic;
            }

            if (!_rigidbody.isKinematic)
            {
                _rigidbody.linearVelocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
        }

        if (_isPlayingDeath)
        {
            if (_playerAnimator != null)
            {
                _playerAnimator.SetTrigger(_respawnTriggerName);
            }

            if (_movementScript != null)
            {
                _movementScript.enabled = _wasMovementEnabled;
            }
        }

        _isPlayingDeath = false;
        Debug.Log("[PlayerRespawn] 復活位置へ移動しました: " + position);
    }

    private void ResetCollisions()
    {
        Collider playerCol = GetComponent<Collider>();
        if (playerCol == null) return;

        var obstacles = Object.FindObjectsByType<ColorObstacles>(FindObjectsSortMode.None);

        foreach (var obstacle in obstacles)
        {
            Collider obstacleCol = obstacle.GetComponent<Collider>();
            if (obstacleCol != null)
            {
                Physics.IgnoreCollision(playerCol, obstacleCol, false);
            }
        }
    }
}
