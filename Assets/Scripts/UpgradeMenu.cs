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

    PlayerProperties _playerProperties;
    GameManager _gameManager;
    AudioPlayer _audioPlayer;
    PlayerInteraction _playerInteraction;

    private void Awake()
    {
        _playerInteraction = FindAnyObjectByType<PlayerInteraction>();
        _playerProperties = FindAnyObjectByType<PlayerProperties>();
        _gameManager = FindAnyObjectByType<GameManager>();
        _audioPlayer = FindAnyObjectByType<AudioPlayer>();

        _sizeUpgradeButton.onClick.AddListener(delegate { TryUpgradeProperty(_playerProperties._size); }); ;
        _speedUpgradeButton.onClick.AddListener(delegate { TryUpgradeProperty(_playerProperties._speed); }); ;
        _areaUpgradeButton.onClick.AddListener(delegate { TryUpgradeProperty(_playerProperties._area); }); ;
        _heatUpgradeButton.onClick.AddListener(delegate { TryUpgradeProperty(_playerProperties._heat); }); ;
    }

    private void Start()
    {
        Invoke("UpdateButtons", 0.1f);
    }

    void TryUpgradeProperty(Property property)
    {
        float currentCost = property._currentCost;
        if (_gameManager.GetMoney() < currentCost)
        {
            _audioPlayer.PlayNoMoneyClip();
            return; 
        }
        if (property.TryUpgradeProperty())
        {
            _audioPlayer.PlayBuyClip();
            _gameManager.ChangeMoney(-currentCost);
            _playerProperties.UpdateProperties();
            _playerInteraction.UpdateParticleSize();
            UpdateButtons();
        }
        else
        {
            _audioPlayer.PlayNoMoneyClip();
        }
    }

    void UpdateButtons()
    {
        if (_playerProperties._size._currentCost  != -1)
        {
            _sizeUpgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Upgrade Size:  $" + _playerProperties._size._currentCost.ToString();
        }
        else
        {
            _sizeUpgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "MAX Size";
        }
        

        if (_playerProperties._speed._currentCost != -1)
        {
            _speedUpgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Upgrade Speed:  $" + _playerProperties._speed._currentCost.ToString();
        }
        else
        {
            _speedUpgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "MAX Speed";
        }

        if (_playerProperties._area._currentCost != -1)
        {
            _areaUpgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Upgrade Area:  $" + _playerProperties._area._currentCost.ToString();
        }
        else
        {
            _areaUpgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "MAX Area";
        }

        if (_playerProperties._heat._currentCost != -1)
        {
            _heatUpgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Upgrade Heat:  $" + _playerProperties._heat._currentCost.ToString();
        }
        else
        {
            _heatUpgradeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "MAX Heat";
        }


        
    }
}
