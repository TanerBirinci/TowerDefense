using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    private Enemy targetEnemy;
    
    [Header("General")]
    public float range = 15f;
    
    [Header("Use Bullets(default)")]
    public GameObject bulletPrefab;
    private float fireCountDown=0f;
    public float fireRate=1f;

    [Header("Use Laser(default)")] 
    public float slowPct = 0.5f;
    public bool uselaser = false;
    public int damageOverTime = 30;
    
    public LineRenderer lineRenderer;
    public ParticleSystem laserImpactEffect;
    
        
    
    [Header("Unity Setup Fields")]
    
    public float turnSpeed = 10f;
    public string enemyTag = "Enemy";
    public Transform partToRotate;

    
    public Transform firePoint;
    
    

    private void Start()
    {
        InvokeRepeating("UpdateTarget",0f,0.5f);
    }

    private void Update()
    {
        if (target == null)
        {
            if (uselaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserImpactEffect.Stop();
                }
            }
            return;
        }
            
        LockOnTarget();

        if (uselaser)
        {
            Laser();
        }
        else
        {
            if (fireCountDown<=0f)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }

            fireCountDown -= Time.deltaTime;  
        }

        
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime*turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime*Time.deltaTime);
        targetEnemy.Slow(slowPct);
        
        
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserImpactEffect.Play();
        }
        lineRenderer.SetPosition(0,firePoint.position);
        lineRenderer.SetPosition(1,target.position);

        Vector3 dir = firePoint.position - target.position;
        
        laserImpactEffect.transform.position = target.position+dir.normalized;
        
        laserImpactEffect.transform.rotation=Quaternion.LookRotation(dir);
        
        
    }
    
    void Shoot()
    {
        GameObject bulletGO=(GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seek(target);
    }


    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        foreach (GameObject enemy in enemies)
        {
            float distanceEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceEnemy<shortestDistance)
            {
                shortestDistance = distanceEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy !=null && shortestDistance<=range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
