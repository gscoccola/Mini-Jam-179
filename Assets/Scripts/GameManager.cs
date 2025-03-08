using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] GameObject _worldPopulationPanel;
    [SerializeField] TextMeshProUGUI _worldPopulationText;
    PlayerProperties _playerProperties;
    Light2D _globalLight;
    AudioPlayer _audioPlayer;

    float _money = 0;
    float _totalGainedMoney = 0;
    public float _worldPopulation;

    [Header("Milestones")]
    [SerializeField] float[] _moneyMilestones = { 20, 50, 100, 200 , Mathf.Infinity};
    [SerializeField] Color[] _globalColors;
    int _milestoneIndex = 0;
    string[] _milestoneTexts = new string[20];

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

    [Header("Warning PopUp")]
    [SerializeField] GameObject _warningPanel;
    [SerializeField] Button _warningAcceptButton;

    private void Awake()
    {
        _audioPlayer = FindAnyObjectByType<AudioPlayer>();

        _globalLight = FindAnyObjectByType<Light2D>();
        _playerProperties = FindAnyObjectByType<PlayerProperties>();

        _helpButton.onClick.AddListener(ToggleHelpScreen);
        _helpBackButton.onClick.AddListener(ToggleHelpScreen);

        _resetButton.onClick.AddListener(ReloadGame);
        _acceptButton.onClick.AddListener(TogglePopUp);

        _warningAcceptButton.onClick.AddListener(CloseWarning);

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
        if (_worldPopulation > 0)
        {
            _worldPopulationText.text = "World Population:" + ((int)_worldPopulation).ToString();
        } 
        else
        {
            _worldPopulationText.text = "World Population: 0";
        }

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
            if (_worldPopulation >= 1000)
            {
                _worldPopulation -= amount;
            }
            else
            {
                _worldPopulation -= amount * 1000 / (_moneyMilestones[19] - _moneyMilestones[18]);
            }
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
        else if (milestoneIndex == 12)
        {
            _audioPlayer.SwitchTrack(1);
        }
        else if (milestoneIndex == 16)
        {
            _worldPopulationPanel.SetActive(true);
            _worldPopulation = _moneyMilestones[18] - _moneyMilestones[16] + 1000;
        }
        else if (milestoneIndex == 19)
        {
            _audioPlayer.StopTrack();
        }


        if (milestoneIndex < 7)
        {
            _audioPlayer.PlayMilestoneClip(0);
        }
        else if (_milestoneIndex < 12)
        {
            _audioPlayer.PlayMilestoneClip(1);
        }
        else
        {
            _audioPlayer.PlayMilestoneClip(2);
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

    void CloseWarning()
    {
        _warningPanel.SetActive(false);
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

        _milestoneTexts[2] = "You made quite a bit of money."
            +Environment.NewLine + Environment.NewLine +
            "It's time to think about expanding operations."
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
            "Correlation with the increased level of pollution remains uncertain";

        _milestoneTexts[8] = "Work becomes optional for the citizens of Green Hill"
            + Environment.NewLine + Environment.NewLine +
            "Everyone is free to follow their dreams";

        _milestoneTexts[9] = "A thick smog covers the town"
            + Environment.NewLine + Environment.NewLine +
            "Festivities and social gatherings are moved indoors";

        _milestoneTexts[10] = "Wealthy families begin moving out of town"
            + Environment.NewLine + Environment.NewLine +
            "Minor concerns over the gentrification of the surrounding areas";

        _milestoneTexts[11] = "First recorded Cancer diagnosis attributed to pollution"
            + Environment.NewLine + Environment.NewLine +
            "Patient starts civil suit against township";

        _milestoneTexts[12] = "Green Hill completly abandoned"
            + Environment.NewLine + Environment.NewLine +
            "Previous inhabitants become millionaries";

        _milestoneTexts[13] = "State bill to regulate carbon emissions gets stalled in senate"
            + Environment.NewLine + Environment.NewLine +
            "Green Hill lobbyist expands its influence to national level";

        _milestoneTexts[14] = "Environmental Activists warn of a possible catastrophe"
            + Environment.NewLine + Environment.NewLine +
            "Green Hill cancer patient dies at age 45";

        _milestoneTexts[15] = "Regional wildlife population loss at an estimated 90%"
            + Environment.NewLine + Environment.NewLine +
            "West Coast is nicknamed \"the World's Chimney\" ";

        _milestoneTexts[16] = "Climate disaster deemed irreversible"
            + Environment.NewLine + Environment.NewLine +
            "Billionaries start living in underground bunkers"
            + Environment.NewLine + Environment.NewLine +
            "\"At this point we might as well keep polluting\" says expert";

        _milestoneTexts[18] = "Humanity goes entirely underground"
            + Environment.NewLine + Environment.NewLine +
            "Supplies estimated to last for only a few years";

        _milestoneTexts[17] = "Mars mission fails spectalularly"
            + Environment.NewLine + Environment.NewLine +
            "Spaceship carrying 2000 colonists explodes in the atmosphere";

        _milestoneTexts[19] = "The last of humanity perishes in underground bunkers"
            + Environment.NewLine + Environment.NewLine +
            "Cockroach population at an all time high";
    }


}
