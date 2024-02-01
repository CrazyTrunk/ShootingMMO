using Cinemachine;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class WeaponChangerAdvanced : MonoBehaviour
{
    [SerializeField] private TwoBoneIKConstraint leftHand;
    [SerializeField] private TwoBoneIKConstraint rightHand;
    [SerializeField] private RigBuilder rig;
    [SerializeField] private Transform[] leftTargets;
    [SerializeField] private Transform[] rightTargers;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private PhotonView photonView;

    [Header("UI for Weapon Switch")]
    [SerializeField] private Sprite[] weaponIcons;
    [SerializeField] private int[] ammoAmounts;

    private Image weaponIcon;
    private Text ammoAmountText;

    private int weaponNumber = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && photonView.IsMine)
        {
            //weaponNumber++;
            weaponIcon = GameObject.Find("WeaponUI").GetComponent<Image>();
            ammoAmountText = GameObject.Find("AmmoText").GetComponent<Text>();
            photonView.RPC(nameof(ChangeWeapon), RpcTarget.AllBuffered);
            //switch weapon when clicking mouse neu ma lon hon so luong weapon => reset ve 0
            if (weaponNumber > weapons.Length - 1)
            {
                weaponIcon.GetComponent<Image>().sprite = weaponIcons[0];
                ammoAmountText.text = ammoAmounts[0].ToString();
                weaponNumber = 0;
            }
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[weaponNumber].SetActive(true);
            weaponIcon.GetComponent<Image>().sprite = weaponIcons[weaponNumber];
            ammoAmountText.text = ammoAmounts[weaponNumber].ToString();
            leftHand.data.target = leftTargets[weaponNumber];
            rightHand.data.target = rightTargers[weaponNumber];
            rig.Build();
        }
    }
    [PunRPC]
    public void ChangeWeapon()
    {
        weaponNumber++;
        if (weaponNumber > weapons.Length - 1)
        {
            weaponNumber = 0;
        }
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        weapons[weaponNumber].SetActive(true);
        leftHand.data.target = leftTargets[weaponNumber];
        rightHand.data.target = rightTargers[weaponNumber];
        rig.Build();
    }
}
