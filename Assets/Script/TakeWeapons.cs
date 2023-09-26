using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine;

public class TakeWeapons : MonoBehaviour
{
    public ShootSys shootSys;
    public Image image;
    public BoxCollider col;
    public Transform player, gunContainer, cam;
    public Animator anim;
    public float takeRange;
    public float dropArriereForce, dropAvantForce;

    public bool equiped;
    public static bool fullStock;
    // Start is called before the first frame update
    void Start()
    {
        if (!equiped)
        {
            shootSys.enabled = false;
            image.gameObject.SetActive(false);
            col.isTrigger = false;
        }

        if (equiped)
        {
            shootSys.enabled = true;
            image.gameObject.SetActive(true);
            col.isTrigger = true;
            fullStock = true;
            anim.SetBool("Weapons", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equiped && distanceToPlayer.magnitude <= takeRange && Input.GetKeyDown(KeyCode.T) && !fullStock)
            Take();
        if (equiped && Input.GetKeyDown(KeyCode.F))
            Drop();
    }

    void Take()
    {
        equiped = true;
        fullStock = true;
        image.gameObject.SetActive(true);

        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        col.isTrigger = true;
        anim.SetBool("Weapons", true);
        shootSys.enabled = true;
    }

    void Drop()
    {
        equiped = false;
        fullStock = false;
        image.gameObject.SetActive(false);

        transform.SetParent(null);

        col.isTrigger = false;

        shootSys.enabled = false;

        float random = Random.Range(-1f, 1f);
        anim.SetBool("Weapons", false);
    }
}
