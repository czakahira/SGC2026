using UnityEngine;
using UnityEngine.UI;

public class ScoreImageDisplay : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private Image[] _digitImages;
    [SerializeField] private Sprite[] _digitSprites = new Sprite[10];
    private int displayedScore = int.MinValue;

    private void Update()
    {
        if (_scoreManager == null) return;

        int score = Mathf.Max(0, _scoreManager.GetScore());
        if (score == displayedScore) return;
        displayedScore = score;

        string digits = score.ToString();

        for (int i = 0; i < _digitImages.Length; i++)
        {
            int characterIndex = i - (_digitImages.Length - digits.Length);
            bool visible = characterIndex >= 0 && characterIndex < digits.Length;

            _digitImages[i].gameObject.SetActive(visible);
            if (visible)
            {
                int number = digits[characterIndex] - '0';
                _digitImages[i].sprite = _digitSprites[number];
            }
        }
    }
}