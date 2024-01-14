using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Vector3 worldPos;
    private Vector3 screenPos;
    // Update is called once per frame
    public GameObject crosshair;
    private void Start()
    {
        Cursor.visible = false;
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
