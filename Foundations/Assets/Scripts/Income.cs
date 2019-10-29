using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Income
{
    public static event EventHandler OnIncomeAmountChanged;

    private static float incomeAmount;

    public static void addIncomeAmount(float amount)
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
