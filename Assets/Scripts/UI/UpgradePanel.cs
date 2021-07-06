using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public delegate void UpgradeActivation();
    public event UpgradeActivation OnUpgradeActivation;

    [SerializeField] private UpgradeIcon[] _upgradeIcons;

    private void Start()
    {
        _upgradeIcons = GetComponentsInChildren<UpgradeIcon>();

        for (int i = 0; i < _upgradeIcons.Length; i++)
        {
            Upgrade randomUpgrade = PlayerUpgrades.Instance.RandomUpgrade;
            _upgradeIcons[i].GetComponent<Image>().sprite = randomUpgrade.SpriteImage;

            Text upgradeIconText = _upgradeIcons[i].GetComponentInChildren<Text>();
            upgradeIconText.text = randomUpgrade.Title.ToString();

            Button upgradeButton = _upgradeIcons[i].GetComponent<Button>();
            upgradeButton.onClick.AddListener(() =>
            {
                randomUpgrade.Activate(FindObjectOfType<Player>());

                OnUpgradeActivation?.Invoke();
            });
        }
    }
}