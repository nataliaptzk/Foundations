using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncomeManager : MonoBehaviour
{
    public float incomeInventoryAmount;
    public float incomeInventoryAmountPerSecond;
    public Text money;
    public int paymentAmount;

    // Start is called before the first frame update
    void Start()
    {
        incomeInventoryAmount = 0f;
        incomeInventoryAmountPerSecond = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        money.text = (int)incomeInventoryAmount + "";
        incomeInventoryAmount += incomeInventoryAmountPerSecond * Time.deltaTime;
        Income.addIncomeAmount(incomeInventoryAmount);
        //incomeInventoryAmount = 0f;
    }

    public void Payment(int paymentAmount)
    {
        if (incomeInventoryAmount > paymentAmount) {
            incomeInventoryAmount = incomeInventoryAmount - paymentAmount;
            Income.payIncomeAmount(incomeInventoryAmount);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
}
