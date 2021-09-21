using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TORAC : MonoBehaviour, IPointerDownHandler
{
    public CanvasGroup[] RACs;

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponentInParent<CanvasGroup>().alpha = 0;
        GetComponentInParent<CanvasGroup>().interactable = false;
        GetComponentInParent<CanvasGroup>().blocksRaycasts = false;

        RACs[ConnectData.RAC].alpha = 1;
        RACs[ConnectData.RAC].interactable = true;
        RACs[ConnectData.RAC].blocksRaycasts = true;

        RACs[ConnectData.RAC].GetComponent<RAC>().Father = GetComponentInParent<CanvasGroup>();
        RACs[ConnectData.RAC].GetComponent<RAC>().balance.IsOn = true;
    }
}
