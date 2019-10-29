using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeManager : MonoBehaviour
{
    private int incomeInventoryAmount;
    private int incomeInventoryAmountPerSecond;

    // Start is called before the first frame update
    void Start()
    {
        incomeInventoryAmountPerSecond = 1;
    }

    // Update is called once per frame
    void Update()
    {
        StartCouroutine (task());
    }

    IEnumerator task()
    {
        yield return new WaitForSeconds(1);
        incomeInventoryAmount += incomeInventoryAmountPerSecond;
        Income.addIncomeAmount(incomeInventoryAmount);
        Debug.Log("INCOME: " + Income.GetIncomeAmount());
        incomeInventoryAmount = 0;
    }
}
