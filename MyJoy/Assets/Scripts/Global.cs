using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System;
using System.Threading;

public class Global : MonoBehaviour
{
    public CanvasGroup menu;
    public CanvasGroup link;
    public CanvasGroup joystick;

    public Button menu_Joystick;
    public Button menu_Link;

    public Button link_Link;
    public Button link_Back;
    public InputField link_IP;
    public InputField link_Name;
    public InputField link_Port;

    public List<Image> joystick_Home = new List<Image>();
    public List<Button> joysticks_Back = new List<Button>();
    public Button joystick_Back;
    public CanvasGroup joystick_Normal;
    public CanvasGroup joystick_Biped;
    public CanvasGroup joystick_Naruto;
    public Button joystick_normal;
    public Button joystick_biped;
    public Button joystick_naruto;

    public Socket sever = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    float i = 5;

    CanvasGroup Now;

    public delegate void LogCallback(string condition, string stackTrace, LogType type);
    // Start is called before the first frame update
    private void Awake()
    {
        Application.targetFrameRate = 30;
        try
        {
            if (!Directory.Exists(Application.persistentDataPath + @"/Data"))
                Directory.CreateDirectory(Application.persistentDataPath + @"/Data");
            if (!File.Exists(Application.persistentDataPath + @"/Data/connect.json"))
            {
                File.Create(Application.persistentDataPath + @"/Data/connect.json").Dispose();
                File.WriteAllText(Application.persistentDataPath + @"/Data/connect.json", JsonUtility.ToJson(new ConnectData()));
            }
            data a = JsonUtility.FromJson<data>
              (File.ReadAllText(Application.persistentDataPath + @"/Data/connect.json"));

            ConnectData.connected_ip = a.connected_ip;
            ConnectData.connected_port = a.connected_port;
            ConnectData.name = a.name;

            ConnectData.RAC = a.RAC;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    void Start()
    {
        try
        {
            if (!Directory.Exists(Application.persistentDataPath + @"/Data"))
                Directory.CreateDirectory(Application.persistentDataPath + @"/Data");
            if (!File.Exists(Application.persistentDataPath + @"/Data/connect.json"))
            {
                File.Create(Application.persistentDataPath + @"/Data/connect.json").Dispose();
                File.WriteAllText(Application.persistentDataPath + @"/Data/connect.json",JsonUtility.ToJson(new ConnectData()));
            }
            ConnectData a = JsonUtility.FromJson<ConnectData>
              (File.OpenText(Application.persistentDataPath + @"/Data/connect.json").ReadToEnd());


        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
            link_Name.text = ConnectData.name;
            link_IP.text = ConnectData.connected_ip;
            link_Port.text = ConnectData.connected_port;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Now = menu;

        menu_Link.onClick.AddListener(ToLink);
        menu_Joystick.onClick.AddListener(ToJoystick);

        link_Link.onClick.AddListener(OnLink);
        link_Back.onClick.AddListener(ToMenu);

        for (int i = 0; i < joysticks_Back.Count; i++)
            joysticks_Back[i].onClick.AddListener(ToJoystick);
        joystick_Back.onClick.AddListener(ToMenu);
        joystick_normal.onClick.AddListener(ToNormal);
        joystick_biped.onClick.AddListener(ToBiped);
        joystick_naruto.onClick.AddListener(ToNaruto);

    }
        

    // Update is called once per frame
    void Update()
    {
        i -= Time.deltaTime;
        if (i < 0)
        {
            i = 5;
            try
            {
                sever.Send(Encoding.UTF8.GetBytes("0+"));
            }
            catch
            {
                link_Link.interactable = true;
                for (int i = 0; i < joystick_Home.Count; i++)
                    joystick_Home[i].color = new Color(240f / 255f, 0, 0, 1);
                link_Link.image.color = new Color(240f / 255f, 0, 0, 1);
                sever = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
        }
    }



    void OnLink()
    {
        try
        {
            sever.Connect(new IPEndPoint(IPAddress.Parse(link_IP.text), int.Parse(link_Port.text)));
            sever.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
            link_IP.interactable = false;
            link_Name.interactable = false;
            link_Port.interactable = false;
            sever.Send(Encoding.UTF8.GetBytes(link_Name.text));
            byte[] buffer = new byte[1024];
            int i = sever.Receive(buffer);
            string[] s = Encoding.UTF8.GetString(buffer, 0, i).Split('|');
            if (s[0] == "SUCCEED")
            {
                link_Link.interactable = false;
                for (int j = 0; j < joystick_Home.Count; j++)
                    joystick_Home[j].color = new Color(102f / 255f, 204f / 255f, 1, 1);
                link_Link.image.color = new Color(102f / 255f, 204f / 255f, 1, 1);

                ConnectData.connected_ip = link_IP.text;
                ConnectData.connected_port = link_Port.text;
                ConnectData.name = link_Name.text;

                data connect = new data();
                connect.connected_ip = ConnectData.connected_ip;
                connect.connected_port = ConnectData.connected_port;
                connect.name = ConnectData.name;

                File.WriteAllText(Application.persistentDataPath + @"/Data/connect.json",JsonUtility.ToJson(connect),Encoding.UTF8);
            }
            else
            {
            }
        }
        catch
        {

        }
    }

    void ToMenu()
    {
        CanvaOn(menu);
        CanvaClose(Now);
        Now = menu;
    }

    void ToLink()
    {
        CanvaOn(link);
        CanvaClose(Now);
        Now = link;
    }

    void ToJoystick()
    {
        CanvaOn(joystick);
        CanvaClose(Now);
        Now = joystick;
    }
    void ToNormal()
    {
        CanvaOn(joystick_Normal);
        CanvaClose(Now);
        Now = joystick_Normal;
    }
    void ToBiped()
    {
        CanvaOn(joystick_Biped);
        CanvaClose(Now);
        Now = joystick_Biped;
    }
    void ToNaruto()
    {
        CanvaOn(joystick_Naruto);
        CanvaClose(Now);
        Now = joystick_Naruto;
    }

    void CanvaClose(CanvasGroup c)
    {
        c.alpha = 0;
        c.blocksRaycasts = false;
        c.interactable = false;
    }
    void CanvaOn(CanvasGroup c)
    {
        c.alpha = 1;
        c.blocksRaycasts = true;
        c.interactable = true;
    }

    /*
void GetIp()
{
   Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
}
*/
}
