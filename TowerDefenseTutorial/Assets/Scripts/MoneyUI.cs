﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public Text moneyText;

    private void Update()
    {
        moneyText.text ="$"+PlayerStats.Money.ToString();   
    }
}
