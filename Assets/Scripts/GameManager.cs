using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _moneyText;
    PlayerProperties _playerProperties;
    Light2D _globalLight;
    AudioPlayer _audioPlayer;

    float _money = 0;
    float _totalGainedMoney = 0;

    [Header("Milestones")]
    [SerializeField] float[] _moneyMilestones = { 20, 50, 100, 200 , Mathf.Infinity};
    [SerializeField] Color[] _globalColors;
    int _milestoneIndex = 0;
    string[] _milestoneTexts = new string[10];

    [Header("Help Menu")]
    [SerializeField] GameObject _helpPanel;
    [SerializeField] Button _helpButton;
    [SerializeField] Button _helpBackButton;

    [Header("Reset Button")]
    [SerializeField] Button _resetButton;

    [Header("Milestone PopUp")]
    [SerializeField] GameObject _popUpPanel;
    [SerializeField] TextMeshProUGUI _popUpText;
    [SerializeField] Button _acceptButton;

    private void Awake()
    {
        _audioPlayer = FindAnyObjectByType<AudioPlayer>();

        _globalLight = FindAnyObjectByType<Light2D>();
        _playerProperties = FindAnyObjectByType<PlayerProperties>();

        _helpButton.onClick.AddListener(ToggleHelpScreen);
        _helpBackButton.onClick.AddListener(ToggleHelpScreen);

        _resetButton.onClick.AddListener(ReloadGame);
        _acceptButton.onClick.AddListener(TogglePopUp);

        SetMileStoneTexts();
    }

    private void Start()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    private void Update()
    {
        _moneyText.text= "Funds: $" + ((int)_money).ToString();
    }

    public float GetMoney()
    {
        return _money;
    }

    public void ChangeMoney(float amount)
    {
        _money += amount;
        if (amount >= 0f)
        {
            _totalGainedMoney += amount;
            TriggerPhaseChange();
        }
    }

    void TriggerPhaseChange()
    {
        if (_totalGainedMoney >= _moneyMilestones[_milestoneIndex])
        {
            TriggerEvent(_milestoneIndex);
            _milestoneIndex++;
        }
    }

    private void TriggerEvent(int milestoneIndex)
    {
        _globalLight.color = _globalColors[milestoneIndex];
        _popUpText.text = _milestoneTexts[milestoneIndex];
        TogglePopUp();
        if (milestoneIndex == 7)
        {
            _audioPlayer.SwitchTrack(0);
        }
        else if (milestoneIndex == 8)
        {
            _audioPlayer.SwitchTrack(1);
        }
    }

    void ToggleHelpScreen()
    {
        _helpPanel.SetActive(!_helpPanel.activeSelf);
    }

    void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }

    void TogglePopUp()
    {
        if (_popUpPanel.activeSelf)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            _audioPlayer.ResumeMusic();
        }
        else
        {
            Time.timeScale = 0;
            _audioPlayer.PauseMusic();
            AudioListener.pause = true;
        }
        
        _popUpPanel.SetActive(!_popUpPanel.activeSelf);
    }

    void SetMileStoneTexts()
    {
        _milestoneTexts[0] = "Great Job!" 
            + Environment.NewLine + Environment.NewLine +
            "A small percentage of the income you generated will be invested back into you." 
            + Environment.NewLine + Environment.NewLine +
            "Spend it on upgrades to Prometheus!";

        _milestoneTexts[1] = "The first water pump has been installed in Green Hill." 
            + Environment.NewLine + Environment.NewLine +
            "The townsfolk will finally enjoy running water!"
            + Environment.NewLine + Environment.NewLine +
            "Keep up the good work!";

        _milestoneTexts[2] = "It's time to think about expanding operations."
            + Environment.NewLine + Environment.NewLine +
            "Be careful of expending all your resources before you can afford mode land!";

        _milestoneTexts[3] = "Literacy in Green Hill reaches 60%."
            + Environment.NewLine + Environment.NewLine +
            "Townsfolk arrange a small shrine in your honor";

        _milestoneTexts[4] = "Child labour is slowly phased out in Green Hill."
            + Environment.NewLine + Environment.NewLine +
            "A yearly festivity is decreed to conmemorate the arrival of Prometheus";

        _milestoneTexts[5] = "A shopping mall is opened in the middle of town"
            + Environment.NewLine + Environment.NewLine +
            "Dissenters are quickly dispersed";

        _milestoneTexts[6] = "Poverty has been erradicated:"
            + Environment.NewLine + Environment.NewLine +
            "Every household in Green Hill is above the income threshold.";

        _milestoneTexts[7] = "Birth defects are twice the national average"
            + Environment.NewLine + Environment.NewLine +
            "Correlations with the increase in pollution are not being investigated";

        _milestoneTexts[8] = "Dire consequences"
            + Environment.NewLine + Environment.NewLine +
            "";

        _milestoneTexts[9] = "..."
            + Environment.NewLine + Environment.NewLine +
            "";
    }


}
