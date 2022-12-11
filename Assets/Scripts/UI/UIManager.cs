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
    private Image _imageOrder;

    [SerializeField]
    private Sprite[] _orderSprites;

    private AbilityType _activeFireType;

    private int _highScore;

    private int _totalEarned;

    private int _goldAmount;

    private int _currentDisplayedOrder;

    public static event Action OnRestarted;


    //Для тестов
    public event Action<int> OnGoldChanged;
    public event Action<AbilityData> OnAbilityUsed;
    public event Action OnPlayerDeath;
    public static event Action OnEnemyDeath;

    private void OnEnable()
    {
        EventManager.OnGameOver.AddListener(ShowDeathPanel);
        EventManager.OnGoldChanged.AddListener(ChangeScores);
        EventManager.OnAbilityChanged.AddListener(UseAbility);
    }

    private void Awake()
    {
        _highScore = 0;
        _totalEarned = 0;
        _goldAmount = 100;
        _currentDisplayedOrder = -1;
    }

    void Start()
    {
        OrderManager.OnOrderChanged += ChangeOrder;
        OrderManager.SetStartOrder();
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
    }

    private void ShowDeathPanel() 
    {
        PauseManager.Pause();
        _gameOverPanel.SetActive(true);
        _textDeathPanelScore.SetText(_highScore.ToString());
        SetUI(false);
    }


    private void ChangeScores(int goldChange) 
    {
        _goldAmount += goldChange;
        if(_goldAmount < 0)
        {
            _goldAmount = 0;
        }
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

    
    private void UseAbility(AbilityType type)
    {
        switch (type)
        {
            case AbilityType.MidasHand:
                if (_activeFireType != type)
                {
                    StartCoroutine(SetAbilityToCooldown(_imageMeleeAttack, 0.3f));
                }
                ChangeFireType(AbilityType.MidasHand);
                break;

            case AbilityType.CloseCombat:
                if (_activeFireType != type)
                {
                    StartCoroutine(SetAbilityToCooldown(_imageRangeAttack, 0.3f));
                }
                ChangeFireType(AbilityType.CloseCombat);
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

        if (abilityType == AbilityType.MidasHand)
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
        int newOrder = OrderManager.Order;
        if (newOrder != _currentDisplayedOrder)
        {
            _currentDisplayedOrder = newOrder;
            _imageOrder.sprite = _orderSprites[newOrder];
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
        SceneManager.LoadScene(0);
    }

    public void CallRestart()
    {
        PauseManager.Pause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
