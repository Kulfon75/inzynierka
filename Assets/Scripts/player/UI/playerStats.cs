using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStats : MonoBehaviour
{
    public int money;
    private float HP;
    private float MaxHp;
    public int wave;

    public bool PlayWave;

    [SerializeField] private Image HpValue;
    [SerializeField] private Text MoneyValue;
    [SerializeField] private Text WaveWalue;

    private void Start()
    {
        money = 100;
        HP = 1000;
        MaxHp = 1000;
        wave = 1;
        UpdateHpBar();
        UpdateWaveBar();
        UpdateMoneyBar();
    }
    public void UpdateHp(float HpValue)
    {
        HP -= HpValue;
        UpdateHpBar();
    }

    public void UpdateMoney(int reward)
    {
        money += reward;
        UpdateMoneyBar();
    }

    public void UpdateWave()
    {
        wave++;
        UpdateWaveBar();
    }
    private void UpdateHpBar()
    {
        HpValue.fillAmount = HP / MaxHp;
    }

    private void UpdateMoneyBar()
    {
        MoneyValue.text = money.ToString();
    }

    private void UpdateWaveBar()
    {
        WaveWalue.text = wave.ToString();
    }
}
