using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Description : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject dropdown;

    public void OnPointerEnter(PointerEventData eventData)
    {
        dropdown.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dropdown.SetActive(false);
    }
    private void Start()
    {
        dropdown.SetActive(false);
    }
}
