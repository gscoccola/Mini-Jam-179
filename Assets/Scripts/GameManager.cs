using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _moneyText;
    PlayerProperties _playerProperties;

    public float _money = 99;

    private void Awake()
    {
        _playerProperties = FindAnyObjectByType<PlayerProperties>();
    }

    private void Update()
    {
        _moneyText.text= "Money: $" + ((int)_money).ToString();
    }
}
