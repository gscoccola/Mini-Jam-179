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
    AudioPlayer _audioPlayer;
    PlayerInteraction _playInteraction;
    bool _isSpent;
    float _playerHeat;

    private void Awake()
    {
        _playInteraction = FindAnyObjectByType<PlayerInteraction>();
        _audioPlayer = FindAnyObjectByType<AudioPlayer>();
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
            _gameManager.ChangeMoney(BurnRate * Time.deltaTime * _playerHeat);
            float ratio = _yield / InitialYield;
            _spriteRenderer.color = new Color(InitialColor.r * ratio, InitialColor.g * ratio, InitialColor.b * ratio);
        }
        if (_yield <= Mathf.Epsilon && !_isSpent)
        {
            _playInteraction.UpdateCurrentBurningObjects(-1);
            _isSpent = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && _yield > 0f)
        {
            _isBeingBurnt = true;
            _audioPlayer.PlayIgnitionClip();
            _playInteraction.UpdateCurrentBurningObjects(+1);
            _playerHeat = collider.GetComponent<PlayerProperties>()._heat._currentValue;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player" && _yield > 0f)
        {
            _isBeingBurnt = false;
            _playInteraction.UpdateCurrentBurningObjects(-1);
            _playerHeat = 0;
        }
    }
}
