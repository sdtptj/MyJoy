using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class Naruto_LR : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Global Option;
    public string Name;

    public void OnPointerDown(PointerEventData eventData)
    {
        GetOption();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Option.sever.Send(Encoding.UTF8.GetBytes("JOYSTICK|RIGHT|0|0+"));
    }

    void GetOption()
    {
        try
        {
            if (Name == "LEFT")
                Option.sever.Send(Encoding.UTF8.GetBytes("JOYSTICK|RIGHT|-1|0+"));
            else
                Option.sever.Send(Encoding.UTF8.GetBytes("JOYSTICK|RIGHT|1|0+"));

        }
        catch
        {

        }
    }
}
