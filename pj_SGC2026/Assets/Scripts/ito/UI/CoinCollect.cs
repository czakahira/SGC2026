using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    [SerializeField] private int _scoreValue = 100;

    
    
    private void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(_scoreValue);
            Destroy(gameObject);
        }
    }
}
