using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAt : MonoBehaviour
{
    private Vector3 worldPos;
    private Vector3 screenPos;
    // Update is called once per frame
    [SerializeField]private GameObject crosshair;
    [SerializeField] private Text nicknameText;

    private void Start()
    {
        Cursor.visible = false;
        nicknameText.text = PhotonNetwork.LocalPlayer.NickName;
    }
    void FixedUpdate()
    {
        screenPos = Input.mousePosition;
        screenPos.z = 3f;
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        transform.position = worldPos;
        crosshair.transform.position = Input.mousePosition;
    }
}
