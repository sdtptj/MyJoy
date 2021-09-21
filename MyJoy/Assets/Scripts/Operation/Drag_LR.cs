using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag_LR : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IDragHandler
{
    public Global Option;
    public string Name;
    public string LName;
    public string RName;

    Vector2 begin;

    public void OnPointerDown(PointerEventData eventData)
    {
        begin = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        int i = (eventData.position - begin).x < 0 ? 0 : 1;
        GetOption(i);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Option.sever.Send(Encoding.UTF8.GetBytes(Name+"|"+LName + "|False" + "+"));
        Option.sever.Send(Encoding.UTF8.GetBytes(Name + "|" + RName + "|False" + "+"));
    }

    void GetOption(int i)
    {
        try
        {
            switch (i) {
                case 0:
                    Option.sever.Send(Encoding.UTF8.GetBytes(Name + "|" + LName + "|True" + "+"));
                    break;
                case 1:
                    Option.sever.Send(Encoding.UTF8.GetBytes(Name + "|" + RName + "|True" + "+"));
                    break;
            }

        }
        catch
        {

        }
    }
}
