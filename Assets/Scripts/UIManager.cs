using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _orderPanel;

    [SerializeField]
    private GameObject _scorePanel;

    [SerializeField]
    private GameObject _abilitiesPanel;

    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private GameObject _pausePanel;

    [SerializeField]
    private GameObject _rulesPanel;

    [SerializeField]
    private TextMeshProUGUI _textDeathPanelScore;

    [SerializeField]
    private TextMeshProUGUI _textUICurrentGold;

    [SerializeField]
    private TextMeshProUGUI _textUIEarned;

    [SerializeField]
    private TextMeshProUGUI _textUIHighScore;

    [SerializeField]
    private Image _imageMeleeAttack;

    [SerializeField]
    private Image _imageRangeAttack;

    [SerializeField]
    private Image _imageGrenade;

    [SerializeField]
    private List<Sprite> _orderSprites = new List<Sprite>();

    private AbilityType _activeFireType;

    private int _highScore;

    private int _totalEarned;

    private int _goldAmount;

    private int _currentDisplayedOrder;

    public event Action OnRestarted;

    public event Action OnPaused;


    //Для тестов
    public event Action<int> OnGoldChanged;
    public event Action<AbilityData> OnAbilityUsed;
    public event Action OnPlayerDeath;
    public event Action OnEnemyDeath;


    private void Awake()
    {
        _highScore = 0;
        _totalEarned = 0;
        _goldAmount = 100;
        _currentDisplayedOrder = -1;
    }


    void Start()
    {
        OnGoldChanged += ChangeScores;
        OnAbilityUsed += UseAbility;
        OnPlayerDeath += ShowDeathPanel;
        OnEnemyDeath += ChangeOrder;

        ChangeOrder();
        ChangeFireType(_activeFireType);
        ChangeScores(0);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        //Тесты
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnGoldChanged?.Invoke(10);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnGoldChanged?.Invoke(-10);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            OnAbilityUsed?.Invoke(new AbilityData(AbilityType.Range, 0.3f));
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnAbilityUsed?.Invoke(new AbilityData(AbilityType.Melee, 0.3f));
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnAbilityUsed?.Invoke(new AbilityData(AbilityType.Grenade, 1.0f));
        }
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            OnPlayerDeath?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnEnemyDeath?.Invoke();
        }
    }

    private void ShowDeathPanel() 
    {
        _gameOverPanel.SetActive(true);
        _textDeathPanelScore.SetText(_highScore.ToString());
        SetUI(false);
    }


    private void ChangeScores(int goldChange) 
    {
        _goldAmount += goldChange;
        if (goldChange > 0)
        {
            _totalEarned += Math.Abs(goldChange);
        }
        
        _textUICurrentGold.SetText(_goldAmount.ToString());
        _textUIEarned.SetText(_totalEarned.ToString());

        if (_goldAmount > _highScore)
        {
            ChangeHighScore(_goldAmount);
        }
    }


    private void ChangeHighScore(int newScore) 
    {
        _highScore = newScore;
        _textUIHighScore.SetText(_highScore.ToString());
    }

    
    private void UseAbility(AbilityData abilityData)
    {
        switch (abilityData.GetAbilityType())
        {
            case AbilityType.Range:
                if (_activeFireType != abilityData.GetAbilityType())
                {
                    StartCoroutine(SetAbilityToCooldown(_imageMeleeAttack, abilityData.GetCooldown()));
                }
                ChangeFireType(AbilityType.Range);
                break;

            case AbilityType.Melee:
                if (_activeFireType != abilityData.GetAbilityType())
                {
                    StartCoroutine(SetAbilityToCooldown(_imageRangeAttack, abilityData.GetCooldown()));
                }
                ChangeFireType(AbilityType.Melee);
                break;

            case AbilityType.Grenade:
                StartCoroutine(SetAbilityToCooldown(_imageGrenade, abilityData.GetCooldown()));
                break;
        }
    }


    private IEnumerator SetAbilityToCooldown(Image abilityImage, float cooldown) 
    {
        for (float t = 0; t < 1; t += Time.deltaTime / cooldown)
        {
            abilityImage.color = Color.Lerp(Color.gray, Color.white, t);
            yield return null;
        }
    }

    
    private void ChangeFireType(AbilityType abilityType)
    {
        _activeFireType = abilityType;

        if (abilityType == AbilityType.Range)
        {
            _imageRangeAttack.rectTransform.localScale = new Vector3(1, 1, 1);
            _imageMeleeAttack.rectTransform.localScale = new Vector3(0.8f, 0.8f, 1);
        }
        else
        {
            _imageMeleeAttack.rectTransform.localScale = new Vector3(1, 1, 1);
            _imageRangeAttack.rectTransform.localScale = new Vector3(0.8f, 0.8f, 1);
        }
    }


    private void ChangeOrder()
    {
        if (OrderManager.Order != _currentDisplayedOrder)
        {
            switch (OrderManager.Order)
            {
                case 0:
                    {
                        _imageOrder.sprite = ;
                        break;
                    }
                case 1:
                    {
                        _imageOrder.sprite = ;
                        break;
                    }

                case 2:
                    {
                        _imageOrder.sprite = ;
                        break;
                    }
            }
        }
    }


    private void SetUI(bool show)
    {
        _orderPanel.SetActive(show);
        _scorePanel.SetActive(show);
        _abilitiesPanel.SetActive(show);
    }

    public void Pause()
    {
        PauseManager.Pause();
        Debug.Log(PauseManager._pauseStatus);

        if (_rulesPanel.activeInHierarchy)
        {
            _rulesPanel.SetActive(!_rulesPanel.activeInHierarchy);
        }
        else
        {
            _pausePanel.SetActive(!_pausePanel.activeInHierarchy);
        }

        SetUI(!_pausePanel.activeInHierarchy);
    }

    public void ShowRules()
    {
        _rulesPanel.SetActive(!_rulesPanel.activeInHierarchy);
        _pausePanel.SetActive(!_pausePanel.activeInHierarchy);
    }

    public void ToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CallRestart()
    {
        OnRestarted?.Invoke();
        Debug.Log("Restarted");
    }
}
