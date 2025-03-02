using UnityEngine;
using System;

public class BioMaterialSpawner : MonoBehaviour
{
    [SerializeField] int ProbabilityInverse;
    PlayerProperties _playerProperties;
    Camera _camera;
    [SerializeField] GameObject _prefabTree;
    [SerializeField] GameObject _prefabCar1;
    [SerializeField] GameObject _prefabCar2;
    [SerializeField] GameObject _prefabBuilding;
    System.Random _random;


    private void Awake()
    {
        _playerProperties = FindAnyObjectByType<PlayerProperties>();
        _camera = FindAnyObjectByType<Camera>();
        _random = new System.Random();
    }

    private void Start()
    {
        float[] areas = _playerProperties._areas;
        int[] tilemapHeights = { 0, 0, 0, 0, 0 };
        int[] tilemapWidths = { 0, 0, 0, 0, 0 };

        for (int index = 0; index < areas.Length; index++)
        {
            tilemapHeights[index + 1] = (int)Mathf.Ceil(areas[index] / 2);
            tilemapWidths[index + 1] = (int)Mathf.Ceil(areas[index] * _camera.aspect / 2);

            for (int i = -tilemapWidths[index+1]; i <= tilemapWidths[index + 1]; i++)
            {
                for (int j = -tilemapHeights[index + 1]; j <= tilemapHeights[index + 1]; j++)
                {
                    if (i >= -tilemapWidths[index] && i <= tilemapWidths[index] &&
                        j >= -tilemapHeights[index] && j <= tilemapHeights[index])
                    { continue; }
                    SpawnObject(index, i, j);

                }
            }
        }
    }

    private void SpawnObject(int index, int i, int j)
    {
        if (_random.Next(0, ProbabilityInverse + 1) != 0) { return; }
        switch (index)
        {
            case 0:
                Instantiate(_prefabTree, new Vector3(2 * i, 2 * j, 0), Quaternion.identity);
                break;
            case 1:
                Instantiate(_prefabCar1, new Vector3(2 * i, 2 * j, 0), Quaternion.identity);
                break;
            case 2:
                Instantiate(_prefabCar2, new Vector3(2 * i, 2 * j, 0), Quaternion.identity);
                break;
            case 3:
                Instantiate(_prefabBuilding, new Vector3(2 * i, 2 * j, 0), Quaternion.identity);
                break;
        }
    }
}
