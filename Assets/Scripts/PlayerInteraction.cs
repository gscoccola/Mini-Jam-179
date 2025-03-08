using System;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerInteraction : MonoBehaviour
{
    int currentBurningObjects;
    AudioPlayer _audioPlayer;
    ParticleSystem _particleSystem;

    private void Awake()
    {
        _audioPlayer = FindAnyObjectByType<AudioPlayer>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void UpdateCurrentBurningObjects(int amount)
    {
        currentBurningObjects += amount;
        _audioPlayer.UpdateBurningSound(currentBurningObjects);
        ParticleSystem.EmissionModule emissionModule = _particleSystem.emission;
        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(currentBurningObjects * 50f
            * transform.localScale.x * transform.localScale.x);

    }

    public void UpdateParticleSize()
    {
        ParticleSystem.MainModule mainModule = _particleSystem.main;
        mainModule.startSize = new ParticleSystem.MinMaxCurve(0.05f / transform.localScale.x, 0.15f / transform.localScale.x);
        mainModule.startSpeed = new ParticleSystem.MinMaxCurve(1.5f / transform.localScale.x, 2.5f / transform.localScale.x);
    }
}
