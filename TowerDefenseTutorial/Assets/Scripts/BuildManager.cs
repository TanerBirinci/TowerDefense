using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager intance;

    public void Awake()
    {
        if (intance != null)
        {
            Debug.LogError("more than one BuildManager in scene!");
            return;
        }
        intance = this;
    }

    public GameObject standartTurretPrefab;
    public GameObject MisseleLauncherPrefab;

    public NodeUI nodeUI; 

    private TurretBluePrint turretToBuild;
    private Node selectedNode;
    
    public bool CanBuild { get { return turretToBuild != null; } } 
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }
    

    public void SelectNode(Node node)
    {
        if (selectedNode==node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }
    
}
