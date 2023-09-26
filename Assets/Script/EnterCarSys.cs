using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCarSys : MonoBehaviour
{
    [SerializeField]
    CarDriving carDriving;
    [SerializeField]
    private Camera carCam;
    public GameObject playerCam;
    public Transform Player;
    public Transform car;
    bool canDrive;
    //bool driving;
    // Start is called before the first frame update
    void Start()
    {
        carDriving.enabled = false;
        carCam.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && canDrive)
        {
            //driving = true;
            carDriving.enabled = true;
            Player.transform.SetParent(car);
            Player.gameObject.SetActive(false);

            playerCam.gameObject.SetActive(false);
            carCam.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            //driving = false;
            carDriving.enabled = false;
            Player.transform.SetParent(null);
            Player.gameObject.SetActive(true);

            playerCam.gameObject.SetActive(true);
            carCam.gameObject.SetActive(false);
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
