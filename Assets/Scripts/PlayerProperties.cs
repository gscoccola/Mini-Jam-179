using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public Property _size;
    [SerializeField] float[] _playerSizes = new float[4] { 1f, 1.5f, 3f, 5f };
    float[] _playerSizeCosts = new float[4] { 5f, 10f, 20f, -1f};

    public Property _speed;
    [SerializeField] float[] _playerSpeeds = new float[4] { 10f, 15f, 25f, 35f };
    float[] _playerSpeedCosts = new float[4] { 5f, 10f, 20f, -1f };

    public Property _heat;
    [SerializeField] float[] _playerHeats = new float[4] { 1f, 1.5f, 2.5f, 5f };
    [SerializeField] Color[] _furnaceColors;
    float[] _playerHeatCosts = new float[4] { 5f, 10f, 20f, -1f };

    public Property _area;
    [SerializeField] public float[] _areas = new float[4] { 5f, 10f , 20f, 30f };
    float[] _areaCosts = new float[4] { 25f, 50f, 100f, -1f };

    public float _screenHeight;
    public float _screenWidth;

    BoxCollider2D _mBoxCollider;
    Camera _camera;
    SpriteRenderer _furnace;

    private void Awake()
    {
        _mBoxCollider = GetComponent<BoxCollider2D>();
        _camera = FindAnyObjectByType<Camera>();
        _furnace = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _size = new Property(_playerSizes, _playerSizeCosts);
        _speed = new Property(_playerSpeeds, _playerSpeedCosts);
        _heat = new Property(_playerHeats, _playerHeatCosts);
        _area = new Property(_areas, _areaCosts);

        UpdateProperties();
    }

    public void UpdateProperties()
    {
        transform.localScale = Vector3.one * _size._currentValue;
        _camera.orthographicSize = _area._currentValue;

        _screenHeight = _area._currentValue;
        _screenWidth = _screenHeight * _camera.aspect;

        _furnace.color = _furnaceColors[_heat._currentIndex];
    }
}
