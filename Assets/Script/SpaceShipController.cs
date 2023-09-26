using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    float activeFowardSpeed, activeStrafeSpeed, activeHoverSpeed;
    float fowardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    public float lookRateSpeed = 90f;
    Vector2 lookInput, screenCenter, mouseDistance;

    float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

        activeFowardSpeed = Input.GetAxisRaw("Vertical") * forwardSpeed;
        activeStrafeSpeed = Input.GetAxisRaw("Horizontal") * strafeSpeed;
        activeHoverSpeed = Input.GetAxisRaw("Hover") * hoverSpeed;

        transform.position += transform.forward * activeFowardSpeed * Time.deltaTime;
        transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime)+(transform.up * activeHoverSpeed * Time.deltaTime);
    }
}
