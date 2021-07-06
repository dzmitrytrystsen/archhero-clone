using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActivator : MonoBehaviour
{
    [SerializeField] private List<LevelButton> _levelButtons;

    private void Start()
    {
        int levelsCompleted = SaveSystem.LoadGameData().LevelsCompleted;

        ActivateCompletedLevels(levelsCompleted);
    }

    private void ActivateCompletedLevels(int levelsCompleted)
    {
        for (int i = 0; i < _levelButtons.Count; i++)
        {
            _levelButtons[i].SetLevelNumber(i + 1);

            if (i + 1 <= levelsCompleted + 1)
            {
                _levelButtons[i].ActivateLevel();
            }
        }
    }
}
