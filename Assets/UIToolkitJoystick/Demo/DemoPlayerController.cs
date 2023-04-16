using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayerController : MonoBehaviour
{
    Rigidbody rb;
    private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * JoystickController.input.y + Vector3.right * JoystickController.input.x;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        rb.rotation = targetRotation;
    }
}
