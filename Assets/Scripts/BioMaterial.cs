using UnityEngine;

public class BioMaterial : MonoBehaviour
{
    [SerializeField] float InitialYield;
    [SerializeField] float BurnRate;
    float _yield;
    bool _isBeingBurnt;
    SpriteRenderer _spriteRenderer;
    GameManager _gameManager;
    bool _isBurnt;
    Color InitialColor;

    float _playerHeat;

    private void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _yield = InitialYield;
        InitialColor = _spriteRenderer.color;
    }

    void Update()
    {
        if (_isBeingBurnt && _yield > 0)
        {
            _yield -= BurnRate * Time.deltaTime * _playerHeat;
            _gameManager._money += BurnRate * Time.deltaTime * _playerHeat;
            float ratio = _yield / InitialYield;
            _spriteRenderer.color = new Color(InitialColor.r * ratio, InitialColor.g * ratio, InitialColor.b * ratio);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _isBeingBurnt = true;
            _playerHeat = collider.GetComponent<PlayerProperties>()._heat._currentValue;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _isBeingBurnt = false;
            _playerHeat = 0;
        }
    }
}
