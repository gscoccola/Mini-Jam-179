using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{

    [Header("Music")]
    [SerializeField] AudioSource _music;
    [SerializeField] AudioResource[] songs;

    [Header("Fire Ignition")]
    [SerializeField] AudioClip _fireIgnition;
    [SerializeField][Range(0f, 1f)] float _ignitionVolume = 1f;

    [Header("Buy Sound")]
    [SerializeField] AudioClip _buyClip;
    [SerializeField][Range(0f, 1f)] float _buyVolume = 1f;

    [Header("No Money Sound")]
    [SerializeField] AudioClip _noMoneyClip;
    [SerializeField][Range(0f, 1f)] float _noMoneyVolume = 1f;

    [Header("Fire loop")]
    [SerializeField] AudioSource _fireLoop;

    [Header("Move Sound")]
    [SerializeField] AudioSource _moveSound;
    Rigidbody2D _playerRigidBody;
    PlayerMovement _playerMovement;

    private void Awake()
    {
        _fireLoop = GetComponent<AudioSource>();

        _playerRigidBody = FindAnyObjectByType<Rigidbody2D>();
        _playerMovement = FindAnyObjectByType<PlayerMovement>();
        _fireLoop.volume = 0f;
    }

    private void Update()
    {
        _moveSound.volume = _playerRigidBody.linearVelocity.magnitude / _playerMovement._currentSpeed;
    }

    public void UpdateBurningSound(int amount)
    {
        switch (amount)
        {
            case 0:
                _fireLoop.volume = 0f;
                break;
            case 1:
                _fireLoop.volume = 1f;
                break;
            case 2:
                _fireLoop.volume = 1.4f;
                break;
            case 3:
                _fireLoop.volume = 1.6f;
                break;
            case 4:
                _fireLoop.volume = 1.7f;
                break;
        }
    }

    public void PlayIgnitionClip()
    {
        PlayClipAtPoint(_fireIgnition, Vector3.zero, _ignitionVolume);
    }

    public void PlayBuyClip()
    {
        PlayClipAtPoint(_buyClip, Vector3.zero, _buyVolume);
    }

    public void PlayNoMoneyClip()
    {
        PlayClipAtPoint(_noMoneyClip, Vector3.zero, _noMoneyVolume);
    }

    public void PauseMusic()
    {
        _music.Pause();
    }

    public void ResumeMusic()
    {
        _music.Play();
    }

    public void SwitchTrack(int index)
    {
        _music.resource = songs[index];
    }


    public static AudioSource PlayClipAtPoint(AudioClip clip, Vector3 pos, float volume = 1, float pitch = 1, bool dontDestroyOnLoad = false)
    {
        GameObject tempGO = new GameObject("TempAudio"); // create the temp object
        tempGO.transform.position = pos; // set its position
        AudioSource tempASource = tempGO.AddComponent<AudioSource>(); // add an audio source
        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(tempASource);
        }
        tempASource.clip = clip;
        tempASource.pitch = pitch;
        tempASource.volume = volume;
        // set other aSource properties here, if desired
        tempASource.Play(); // start the sound
        MonoBehaviour.Destroy(tempGO, tempASource.clip.length); // destroy object after clip duration (this will not account for whether it is set to loop)
        return tempASource; // return the AudioSource reference
    }
}
