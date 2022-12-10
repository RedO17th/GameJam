using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //������ ������ ������
    [SerializeField]
    private GameObject _orderPanel;

    //������ ������ ������ � �������
    [SerializeField]
    private GameObject _scorePanel;

    //������ ������ ������������
    [SerializeField]
    private GameObject _abilitiesPanel;

    //������ �������� ���������
    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private TextMeshProUGUI _textDeathPanelScore;

    [SerializeField]
    private TextMeshProUGUI _textUIGoldAmount;

    [SerializeField]
    private TextMeshProUGUI _textUIHighScore;

    [SerializeField]
    private Image _imageMeleeAttack;

    [SerializeField]
    private Image _imageRangeAttack;

    [SerializeField]
    private Image _imageGranade;

    //���� �������
    private int _highScore;


    //��� ������
    public event Action<int> OnGoldChanged;
    public event Action<AbilityData> OnAbilityUsed;
    public event Action OnPlayerDeath;
    public event Action OnRestarted;
    private int _goldAmount = 0;


    private void Awake()
    {
        _highScore = 0;
    }


    void Start()
    {
        //������������� �� ��������� �����
        OnGoldChanged += ChangeGoldAmount;
        //������������� �����������
        OnAbilityUsed += UseAbility;
        //������������� �� ������
        OnPlayerDeath += ShowDeathPanel;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _goldAmount += 1;
            OnGoldChanged?.Invoke(_goldAmount);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _goldAmount -= 1;
            OnGoldChanged?.Invoke(_goldAmount);
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
    }

    private void ShowDeathPanel() 
    {
        _gameOverPanel.SetActive(true);
        _textDeathPanelScore.SetText(_highScore.ToString());

        _orderPanel.SetActive(false);
        _scorePanel.SetActive(false);
        _abilitiesPanel.SetActive(false);
    }


    private void ChangeGoldAmount(int newGold) 
    {
        _textUIGoldAmount.SetText(newGold.ToString());
        Debug.Log(newGold > _highScore);
        if (newGold > _highScore)
        {
            ChangeHighScore(newGold);
        }
    }


    private void ChangeHighScore(int newScore) 
    {
        _highScore = newScore;
        Debug.Log(_highScore);
        _textUIHighScore.SetText(_highScore.ToString());
    }

    
    private void UseAbility(AbilityData abilityData)
    {
        switch (abilityData.GetAbilityType())
        {
            case AbilityType.Range:
                StartCoroutine(SetAbilityToCooldown(_imageMeleeAttack, abilityData.GetCooldown()));
                ChangeFireType(AbilityType.Range);
                break;
            case AbilityType.Melee:
                StartCoroutine(SetAbilityToCooldown(_imageRangeAttack, abilityData.GetCooldown()));
                ChangeFireType(AbilityType.Melee);
                break;
            case AbilityType.Grenade:
                StartCoroutine(SetAbilityToCooldown(_imageGranade, abilityData.GetCooldown()));
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

    public void CallRestart()
    {
        OnRestarted?.Invoke();
        Debug.Log("Restarted");
    }
}
