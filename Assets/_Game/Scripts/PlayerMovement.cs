using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float rotateSpeed= 100f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        Vector3 rotateY = new Vector3(0, Input.GetAxis("Mouse X") * rotateSpeed * Time.fixedDeltaTime, 0);
        if(movement != Vector3.zero)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotateY));
        }
        rb.MovePosition(rb.position + Input.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime * transform.forward + Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime * transform.right);
        anim.SetFloat("BlendV", Input.GetAxis("Vertical"));
        anim.SetFloat("BlendH", Input.GetAxis("Horizontal"));

    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(1200 * Time.deltaTime * Vector3.up, ForceMode.VelocityChange);
        }
    }
}
