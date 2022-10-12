using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standartTurret;
    public TurretBluePrint missileLauncher;
    public TurretBluePrint laserBeamer;
    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager=BuildManager.intance;
        
    }

    public void SelectStandardTurret()
    {
        _buildManager.SelectTurretToBuild(standartTurret);
    }
    public void SecektMisseleLauncher()
    {
        _buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        _buildManager.SelectTurretToBuild(laserBeamer);
    }
    
}
