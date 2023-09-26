using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Image image;
    IAlife alife;
    Transform camTransform;
    [SerializeField]
    bool focusCam = true;
    float lifeRate
    {
        get
        {
            return (float)alife.health / (float)alife.maxHealth;
        }
    }
    public void Init(IAlife alife)
    {
        camTransform = Camera.main.transform;
        this.alife = alife;
    }
    void LateUpdate()
    {
        if (!focusCam) return;
        transform.LookAt(camTransform);
    }
    //met à jour le sprite de la barre de vie
    public void UpdateHealth()
    {
        image.fillAmount = lifeRate;
    }
}
