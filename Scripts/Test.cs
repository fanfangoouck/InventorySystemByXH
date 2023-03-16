using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    #region basic property
    private int basicStrength = 10;
    private int basicIntellect = 10;
    private int basicAgility = 10;
    private int basicStamina = 10;
    private int basicDamage = 10;

    public int BasicStrength
    {
        get
        {
            return basicStrength;
        }
    }
    public int BasicIntellect
    {
        get
        {
            return basicIntellect;
        }
    }
    public int BasicAgility
    {
        get
        {
            return basicAgility;
        }
    }
    public int BasicStamina
    {
        get
        {
            return basicStamina;
        }
    }
    public int BasicDamage
    {
        get
        {
            return basicDamage;
        }
    }
    #endregion

    private int coinAmount = 100;
    private Text coinText;

    public int CoinAmount
    {
        get
        {
            return coinAmount;
        }
        set
        {
            coinAmount = value; // ??
            coinText.text = coinAmount.ToString();
        }
    }


    void Start()
    {
        coinText = GameObject.Find("Coin").GetComponentInChildren<Text>(); //获得coin下孩子的text组件
        coinText.text = coinAmount.ToString();//永远记得这一步
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            int id = Random.Range(1,18);
            Knapsack.Instance.StoreItem(id);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Knapsack.Instance.DisplaySwitch();
        }

         if (Input.GetKeyDown(KeyCode.C))
        {
            Chest.Instance.DisplaySwitch();
         }

          if (Input.GetKeyDown(KeyCode.E))
         {
            EquipmentPanel.Instance.DisplaySwitch();
         }

          if (Input.GetKeyDown(KeyCode.V))
          {
            Vendor.Instance.DisplaySwitch();
          }

           if (Input.GetKeyDown(KeyCode.F))
          {
            Forge.Instance.DisplaySwitch();
          }
    }

    //消费金币
    public bool ConsumeCoin(int amount)
    {
        if(coinAmount >= amount)
        {
            coinAmount -= amount;
            coinText.text = coinAmount.ToString();
            return true;
        }
        return false;
    }

    //赚取金币
    public void EarnCoin(int amount)
    {
        coinAmount += amount;
        coinText.text = coinAmount.ToString();
    }

}
