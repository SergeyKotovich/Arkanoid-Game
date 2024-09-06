using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreLabel;
    private IScore _scoreController;

    public void Initialize(IScore scoreController)
    {
        _scoreController = scoreController;
        _scoreController.ScoreChanged += UpdateScore;
    }

    private void UpdateScore(int points)
    {
        _scoreLabel.text = points.ToString();
    }

    private void OnDestroy()
    {
        _scoreController.ScoreChanged -= UpdateScore;
    }
}