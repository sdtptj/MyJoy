using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text;
using System.Net.Sockets;
using System;

public class Gas : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Global Option;

    public  Transform Up,Down;
    public GameObject Img_BackGround;
    public GameObject Img_Stick;

    Vector2 begin;
    Vector2 last;
    Vector2 normal;

    float length;
    // Start is called before the first frame update
    void Start()
    {
        normal = transform.position;
        length = Mathf.Abs(Up.position.y - Down.position.y) / 2;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Img_BackGround.transform.position = eventData.position;
        begin = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        string name = "L";
        if (eventData.position - begin != last)
        {
            try
            {
                if (Mathf.Abs(eventData.position.y - begin.y) <= length)
                    Img_Stick.transform.position = begin + (eventData.position.y - begin.y) * new Vector2(0, 1);
                else
                    Img_Stick.transform.position = begin + length * Mathf.Sign(eventData.position.y - begin.y) * new Vector2(0, 1);
                if (eventData.position.y - begin.y > 0)
                {
                    name = "R";
                }
                Option.sever.Send(Encoding.UTF8.GetBytes("Trigger|"+name+"|"+
                    Math.Abs(Math.Round((Img_Stick.transform.position.y - begin.y)/length,3)).ToString()
                    +"+Trigger|"+(name == "L"?"R":"L")+"|0|0"));
            }
            catch
            {

            }
            last = eventData.position;
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        try
        {
            Option.sever.Send(Encoding.UTF8.GetBytes("Trigger|L|0|0+Trigger|R|0|0"));
        }
        catch
        {

        }
        Img_BackGround.transform.position = normal;
        Img_Stick.transform.localPosition = new Vector3();
    }
}
