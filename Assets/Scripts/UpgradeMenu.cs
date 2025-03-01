using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] Button _sizeUpgradeButton;
    [SerializeField] Button _speedUpgradeButton;
    [SerializeField] Button _heatUpgradeButton;
    [SerializeField] Button _areaUpgradeButton;
    [SerializeField] TextMeshProUGUI _sizeUpgradeButtonText;
    [SerializeField] TextMeshProUGUI _speedUpgradeButtonText;
    [SerializeField] TextMeshProUGUI _heatUpgradeButtonText;
    [SerializeField] TextMeshProUGUI _areaUpgradeButtonText;

    PlayerProperties _playerProperties;
    GameManager _gameManager;

    private void Awake()
    {
        _playerProperties = FindAnyObjectByType<PlayerProperties>();
        _gameManager = FindAnyObjectByType<GameManager>();

        _sizeUpgradeButton.onClick.AddListener(delegate { TryUpgradeProperty(_playerProperties._size); }); ;
        _speedUpgradeButton.onClick.AddListener(delegate { TryUpgradeProperty(_playerProperties._speed); }); ;
        _areaUpgradeButton.onClick.AddListener(delegate { TryUpgradeProperty(_playerProperties._area); }); ;
        _heatUpgradeButton.onClick.AddListener(delegate { TryUpgradeProperty(_playerProperties._heat); }); ;
    }

    void TryUpgradeProperty(Property property)
    {
        if (_gameManager._money < property._currentCost) { return; }
        if (property.TryUpgradeProperty())
        {
            _gameManager._money -= property._currentCost;
            _playerProperties.UpdateProperties();
        }
    }
}
