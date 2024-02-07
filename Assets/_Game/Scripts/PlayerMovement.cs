using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float rotateSpeed= 100f;
    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    private bool canJump = true;
    private bool isDead = false;
    [Header("Spawn")]
    private Vector3 startPos;
    private bool isRespawned = false;
    [SerializeField] private DisplayColor displayColor;
    [SerializeField] private PhotonView photonViewObj;

    public bool IsDead { get => isDead; set => isDead = value; }
    private void Start()
    {
        startPos = transform.position;
    }
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
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                canJump = false;
                rb.AddForce(jumpForce * Vector3.up, ForceMode.VelocityChange);
                StartCoroutine(JumpAgain());
            }
        }
        if(isDead && !isRespawned)
        {
            isRespawned = true;
            StartCoroutine(RespawnWaitTime());
        }
    }

    IEnumerator RespawnWaitTime()
    {
        yield return new WaitForSeconds(3);
        isDead = false;
        isRespawned = false;
        transform.position = startPos;
        displayColor.Respawn(photonViewObj.Owner.NickName);
    }

    IEnumerator JumpAgain()
    {
        yield return new WaitForSeconds(1f);
        canJump = true;

    }
}
