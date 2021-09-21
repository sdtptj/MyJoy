using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RAC : MonoBehaviour
{
    public CanvasGroup Father;
    public Balance balance;

    public Button back;

    private void Start()
    {
        back.onClick.AddListener(Back);
    }

    void Back()
    {
        balance.IsOn = false;

        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        Father.alpha = 1;
        Father.interactable = true;
        Father.blocksRaycasts = true;
    }
}
