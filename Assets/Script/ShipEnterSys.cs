using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEnterSys : MonoBehaviour
{
    [SerializeField]
    SpaceShipController spaceShip;
    [SerializeField]
    private Camera shipCam;
    public GameObject playerCam;
    public Transform Player;
    public Transform ship;
    bool canDrive;
    //bool driving;
    // Start is called before the first frame update
    void Start()
    {
        spaceShip.enabled = false;
        shipCam.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && canDrive)
        {
            //driving = true;
            spaceShip.enabled = true;
            Player.transform.SetParent(ship);
            Player.gameObject.SetActive(false);

            playerCam.gameObject.SetActive(false);
            shipCam.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            //driving = false;
            spaceShip.enabled = false;
            Player.transform.SetParent(null);
            Player.gameObject.SetActive(true);

            playerCam.gameObject.SetActive(true);
            shipCam.gameObject.SetActive(false);
        }
    }


    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            canDrive = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            canDrive = false;
        }
    }
}
