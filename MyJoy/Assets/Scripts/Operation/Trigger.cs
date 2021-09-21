using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Global Option;
    public string Name; 

    public void OnPointerDown(PointerEventData eventData)
    {
        GetOption("|1");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetOption("|0");
    }

    void GetOption(string b)
    {
        try
        {
            Option.sever.Send(Encoding.UTF8.GetBytes("Trigger|" + Name + b + "+"));
        }
        catch
        {

        }
    }
}
