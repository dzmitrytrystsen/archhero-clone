using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    private Text _levelNumberText;
    private Button _levelButton;

    private void Awake()
    {
        _levelNumberText = GetComponentInChildren<Text>();
        _levelButton = GetComponent<Button>();

        DeactivateLevel();
    }

    public void SetLevelNumber(int levelNumberText)
    {
        _levelNumberText.text = levelNumberText.ToString();
    }

    public void ActivateLevel()
    {
        _levelButton.interactable = true;

        Color tempColor = _levelNumberText.color;
        tempColor.a = 1f;
        _levelNumberText.color = tempColor;
    }

    private void DeactivateLevel()
    {
        _levelButton.interactable = false;

        Color tempColor = _levelNumberText.color;
        tempColor.a = 0.3f;
        _levelNumberText.color = tempColor;
    }
}
