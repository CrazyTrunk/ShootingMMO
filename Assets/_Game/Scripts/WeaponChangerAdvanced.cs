using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponChangerAdvanced : MonoBehaviour
{
    [SerializeField] private TwoBoneIKConstraint leftHand;
    [SerializeField] private TwoBoneIKConstraint rightHand;
    [SerializeField] private RigBuilder rig;
    [SerializeField] private Transform[] leftTargets;
    [SerializeField] private Transform[] rightTargers;
    [SerializeField] private GameObject[] weapons;
    private int weaponNumber = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
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
}
