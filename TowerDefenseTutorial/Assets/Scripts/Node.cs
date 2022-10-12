using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    [HideInInspector]
    public GameObject turret;

    [HideInInspector] 
    public TurretBluePrint turretBluePrint;

    [HideInInspector] 
    public bool isUpgraded = false;

    public Vector3 positionOffset;
    private Renderer rend;
    private Color startColor;

    private BuildManager _buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        _buildManager=BuildManager.intance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        
        if (turret!=null) 
        {
           _buildManager.SelectNode(this);
            return; 
        }
        if (!_buildManager.CanBuild)
            return;

        BuilTurret(_buildManager.GetTurretToBuild());
        
    }

    void BuilTurret(TurretBluePrint bluePrint)
    {
        if (PlayerStats.Money<bluePrint.cost)   
        {
            Debug.Log("Not Enough Money to Build That!");
            return;
        }

        PlayerStats.Money -= bluePrint.cost;
        
        GameObject _turret=(GameObject)Instantiate(bluePrint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBluePrint = bluePrint;

        Debug.Log("Turret Build!");
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money<turretBluePrint.upgradeCost)   
        {
            Debug.Log("Not Enough Money to Upgrade That!");
            return;
        }

        PlayerStats.Money -= turretBluePrint.upgradeCost;
        
        Destroy(turret);
        
        GameObject _turret=(GameObject)Instantiate(turretBluePrint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        isUpgraded = true;
        Debug.Log("Turret Upgraded!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBluePrint.GetSellAmount();
        Destroy(turret);
        turretBluePrint = null;
    }
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!_buildManager.CanBuild)
            return;

        if (_buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color=Color.red;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
