using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponChange : MonoBehaviour
{
    [SerializeField] private TwoBoneIKConstraint leftHand;
    [SerializeField] private TwoBoneIKConstraint rightHand;
    [SerializeField] private RigBuilder rig;
    [SerializeField] private Transform leftTargetWeapon1;
    [SerializeField] private Transform rightTargetWeapon1;
    [SerializeField] private Transform leftTargetWeapon2;
    [SerializeField] private Transform rightTargetWeapon2;
    [SerializeField] private Transform leftTargetWeapon3;
    [SerializeField] private Transform rightTargetWeapon3;
    [SerializeField] private GameObject weapon1;
    [SerializeField] private GameObject weapon2;
    [SerializeField] private GameObject weapon3;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
            leftHand.data.target = leftTargetWeapon1;
            rightHand.data.target = rightTargetWeapon1;
            rig.Build();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);
            weapon3.SetActive(false);
            leftHand.data.target = leftTargetWeapon2;
            rightHand.data.target = rightTargetWeapon2;
            rig.Build();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            weapon1.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(true);
            leftHand.data.target = leftTargetWeapon3;
            rightHand.data.target = rightTargetWeapon3;
            rig.Build();
        }
    }
}
