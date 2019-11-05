using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IncomeManager : MonoBehaviour
{
    public static float incomeInventoryAmount;
    public float incomeInventoryAmountPerSecond;
    public TextMeshProUGUI money;
    public int paymentAmount;

    // Start is called before the first frame update
    void Start()
    {
        incomeInventoryAmount = 300f;
        incomeInventoryAmountPerSecond = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        money.text = "£ " + (int) incomeInventoryAmount + "";
        incomeInventoryAmount += incomeInventoryAmountPerSecond * Time.deltaTime;
        Income.addIncomeAmount(incomeInventoryAmount);
        //incomeInventoryAmount = 0f;
    }

    public static void Payment(int paymentAmount)
    {
        if (incomeInventoryAmount > paymentAmount)
        {
            incomeInventoryAmount = incomeInventoryAmount - paymentAmount;
            Income.payIncomeAmount(incomeInventoryAmount);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    //I know its not ideal but apparently buttons hate static functions
    public void PaymentWrapper(int paymentAmount)
    {
        Payment(paymentAmount);
    }
}