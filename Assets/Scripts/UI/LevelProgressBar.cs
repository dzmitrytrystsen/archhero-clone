using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    [SerializeField] private Image _progressBarFilledImage;
    [SerializeField] private Text _levelText;
    
    private static readonly string LEVEL_TEXT = "Lv. ";

    private void Start()
    {
        _progressBarFilledImage.fillAmount = 0f;
        _levelText.text = LEVEL_TEXT + 1;
    }

    public void UpdateLevelProgressBar(float XPfillPercent, int currentLevel)
    {
        _levelText.text = LEVEL_TEXT + currentLevel;
        _progressBarFilledImage.fillAmount = XPfillPercent;
    }
}
