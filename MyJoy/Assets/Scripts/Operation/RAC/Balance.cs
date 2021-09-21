using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Balance : MonoBehaviour
{
    public bool IsOn;
    public Global Option;
    public Button Reset;
    public Slider balance;

    Gyroscope go;
    bool SupoGo;
    // Start is called before the first frame update
    void Start()
    {
        SupoGo = SystemInfo.supportsGyroscope;
        go = Input.gyro;
        go.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOn && SupoGo)
        {
            float sign = Mathf.Sign(go.gravity.x);
            balance.value = (float)Math.Round(sign * Mathf.Min(2 * Mathf.Abs(go.gravity.x), 1),2);
            if (Option.sever.Poll(1000, SelectMode.SelectRead))
                Option.sever.Send(Encoding.UTF8.GetBytes("JOYSTICK|LEFT|" + balance.value.ToString() + "|0+"));
        }
    }
}
