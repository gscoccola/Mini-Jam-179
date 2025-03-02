using UnityEngine;

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
        _particleSystem.emissionRate = currentBurningObjects * 30;
    }
}
