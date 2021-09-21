using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class POV : MonoBehaviour,IPointerDownHandler,IDragHandler, IPointerUpHandler
{
    public Global Option;
    public string Name;
    public GameObject Img_ALL;

    Vector2 normal = new Vector3();
    Vector2 begin = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        normal = Img_ALL.transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Img_ALL.transform.position = eventData.position;
        begin = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        try
        {
            GetOption((eventData.position - begin).normalized);
        }
        catch 
        {

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        Img_ALL.transform.position = normal;
        try
        {
            Option.sever.Send(Encoding.UTF8.GetBytes(Name + "|-1" + "+"));
        }
        catch { }
    }

    void GetOption(Vector2 v)
    {
        try { 
            string s = "";
            if (v.x < -0.5f)
            {
                s = "|3";
            }
            else if (v.x > 0.5f)
            {
                s = "|1";
            }
            else
            {
                if (v.y > 0)
                {
                    s = "|0";
                }
                else
                {
                    s = "|2";
                }
            }
            Option.sever.Send(Encoding.UTF8.GetBytes(Name + s + "+"));
        }
        catch
        {

        }
    }
}
