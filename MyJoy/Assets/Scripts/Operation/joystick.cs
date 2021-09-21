using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading;
using System.Net.Sockets;

public class joystick : MonoBehaviour,IPointerDownHandler,IDragHandler,IPointerUpHandler
{
    public Global Option;
    public GameObject Img_BackGround;
    public GameObject Img_Stick;

    Vector2 normal = new Vector2();
    Vector2 begin = new Vector2();
    Vector2 last = new Vector2();

    float max = 0;

    public string Name;

    // Start is called before the first frame update
    void Start()
    {
        max = Img_Stick.GetComponent<Image>().rectTransform.offsetMin.y
            + Img_Stick.GetComponent<Image>().rectTransform.rect.width/2;
        normal = Img_BackGround.transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Img_BackGround.transform.position = eventData.position;
        begin = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.position - begin != last)
        {
            try
            {
                Vector2 v = max*(eventData.position - begin).normalized.magnitude < (eventData.position - begin).magnitude
                    ? (eventData.position - begin).normalized : (eventData.position - begin)/max;
                GetOption(v);
            }
            catch
            {

            }
            last = eventData.position;
        }
        if ((eventData.position - begin).magnitude <= max)
            Img_Stick.transform.position = eventData.position;
        else
            Img_Stick.transform.position = begin + (eventData.position - begin).normalized * max;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        try
        {
            Option.sever.Send(Encoding.UTF8.GetBytes("JOYSTICK|" + Name + "|0|0" + "+"));
        }
        catch
        {

        }
        Img_BackGround.transform.position = normal;
        Img_Stick.transform.localPosition = new Vector3();
    }

    void GetOption(Vector2 v)
    {
        if (Option.sever.Poll(1000, SelectMode.SelectRead))
             Option.sever.Send(Encoding.UTF8.GetBytes("JOYSTICK|" + Name + "|" + v.x.ToString() + "|" + v.y.ToString() + "+"));
    }

}
