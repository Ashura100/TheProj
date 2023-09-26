using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootSys : MonoBehaviour
{
    public Transform attackPoint;
    public int damage = 20;
    public float range = 100f;
    public LayerMask enemyLayer;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(attackPoint.position, attackPoint.forward, out hit, range, enemyLayer))
        {
            Debug.Log(hit.collider.name);
            LifeSys lifeSys = hit.transform.GetComponent<LifeSys>();

            if(lifeSys != null)
            {
                lifeSys.TakeDamage(damage);
            }
        }
    }
}