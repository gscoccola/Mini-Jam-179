using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public Property _size;
    float[] _playerSizes = new float[7] { 1f, 1.5f, 3f, 5f, 8f, 11f, 15f };
    float[] _playerSizeCosts = new float[7] { 5f, 25f, 75f, 250f, 1000f, 4000f, -1f };

    public Property _speed;
    float[] _playerSpeeds = new float[7] { 3f, 4.5f, 6.5f, 9.5f , 12f, 14f, 16f};
    float[] _playerSpeedCosts = new float[7] { 5f, 15f, 50f, 150f, 500f, 1000f, -1f };

    public Property _heat;
    float[] _playerHeats = new float[7] { 1f, 1.5f, 2.5f, 5f, 8f, 10f , 20f};
    [SerializeField] Color[] _furnaceColors;
    float[] _playerHeatCosts = new float[7] { 5f, 25f, 75f, 250f, 1000f, 4000f, -1f };

    public Property _area;
    public float[] _areas = new float[5] { 8f, 16f , 32f, 50f, 70f };
    float[] _areaCosts = new float[5] { 25f, 50f, 250f, 1000f, -1f };

    public float _screenHeight;
    public float _screenWidth;

    BoxCollider2D _mBoxCollider;
    Camera _camera;
    SpriteRenderer _furnace;
    ParticleSystem _furnaceSparks;

    private void Awake()
    {
        _mBoxCollider = GetComponent<BoxCollider2D>();
        _camera = FindAnyObjectByType<Camera>();
        _furnace = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _furnaceSparks = _furnace.GetComponent<ParticleSystem>();
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
        _furnaceSparks.startColor = _furnaceColors[_heat._currentIndex];
    }
}
