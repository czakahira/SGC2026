using UnityEngine;
using UnityEngine.UI;

public class LifeImageDisplay : MonoBehaviour
{
    [SerializeField] private PlayerLife _playerLife;
    [SerializeField] private Image[] _digitImages;          // 左から右の順
    [SerializeField] private Sprite[] _digitSprites = new Sprite[10]; // 0～9

    private int _displayedLife = int.MinValue;

    private void Update()
    {
        if (_playerLife == null) return;

        int life = Mathf.Max(0, _playerLife.life);
        if (life == _displayedLife) return;
        _displayedLife = life;

        string digits = life.ToString();

        for (int i = 0; i < _digitImages.Length; i++)
        {
            bool visible = i < digits.Length;
            _digitImages[i].gameObject.SetActive(visible);

            if (visible)
            {
                int number = digits[i] - '0';
                _digitImages[i].sprite = _digitSprites[number];
            }
        }
    }
}