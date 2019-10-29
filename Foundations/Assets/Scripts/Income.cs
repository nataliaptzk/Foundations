using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Income : MonoBehaviour
{
    public static event EventHandler OnIncomeAmountChanged;

    private static int incomeAmount;

    public static void addIncomeAmount(int amount)
    {
        incomeAmount += amount;
        if (OnIncomeAmountChanged != null)
            OnIncomeAmountChanged(null, EventArgs.Empty);
    }

    public static float GetIncomeAmount()
    {
        return incomeAmount;
    }
}
