using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ABXY : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Global Option;
    public string Name;
    public GameObject Img_ALL;

    Vector2 normal = new Vector3();
    Vector2 begin = new Vector3();

    Dictionary<string, string> d = new Dictionary<string, string>();
    string[] keys = new string[4];
    // Start is called before the first frame update
    void Start()
    {
        d.Add("|A", "|False");
        d.Add("|B", "|False");
        d.Add("|X", "|False");
        d.Add("|Y", "|False");
        keys[0] = "|A";
        keys[1] = "|B";
        keys[2] = "|X";
        keys[3] = "|Y";
        normal = Img_ALL.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Img_ALL.transform.position = eventData.position;
        begin = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        GetOption((eventData.position - begin).normalized);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        Img_ALL.transform.position = normal;
        GetOption(new Vector2());
    }

    void GetOption(Vector2 v)
    {
        try
        {
            d["|A"] = "|False";
            d["|B"] = "|False";
            d["|X"] = "|False";
            d["|Y"] = "|False";
            if (v != new Vector2())
                if (v.x < -0.5f)
                {
                    d["|A"] = "|True;";
                }
                else if (v.x > 0.5f)
                {
                    d["|X"] = "|True;";
                }
                else
                {
                    if (v.y > 0)
                    {
                        d["|Y"] = "|True;";
                    }
                    else
                    {
                        d["|B"] = "|True;";
                    }
                }
            string s = Name;
            for (int i = 0; i < d.Count; i++)
            {
                s += keys[i] + d[keys[i]];
            }
            Option.sever.Send(Encoding.UTF8.GetBytes(s + "+"));
        }
        catch
        {

        }
    }
}
