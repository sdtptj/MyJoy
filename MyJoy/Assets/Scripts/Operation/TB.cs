using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class TB : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Global Option;
    public string Name;

    public void OnPointerDown(PointerEventData eventData)
    {
        GetOption("|True");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetOption("|False");
    }

    void GetOption(string b)
    { try
        {
            Option.sever.Send(Encoding.UTF8.GetBytes("OTHER|" + Name + b + "+"));
        }
        catch 
        {
            
        }
    }
}
