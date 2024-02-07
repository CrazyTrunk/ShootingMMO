using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnCounter : MonoBehaviour
{
    [SerializeField] private Text spawnTime;
    [SerializeField] private int startTime = 3;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(SpawnCounter());
    }

    IEnumerator SpawnCounter()
    {
        int time = startTime;

        while (time > 0)
        {
            spawnTime.text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;
        }

        spawnTime.text = "Go!";
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
