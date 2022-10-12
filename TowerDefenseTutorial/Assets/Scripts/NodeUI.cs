using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    public Text upgradeCost;
    public Button upgradeButton;
    public Text sellCost;
    
    
    
    private Node target;

    public void SetTarget(Node _target)
    {
       target = _target;

       transform.position = target.GetBuildPosition();
       if (!target.isUpgraded)
       {
           upgradeCost.text ="$"+ target.turretBluePrint.upgradeCost.ToString();
           upgradeButton.interactable = true;
       }
       else
       {
           upgradeCost.text = "MAX";
           upgradeButton.interactable = false;
       }

       sellCost.text = "$" + target.turretBluePrint.GetSellAmount();
       ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.intance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.intance.DeselectNode();
    }
}
