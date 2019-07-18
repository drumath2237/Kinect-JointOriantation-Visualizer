using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WebSocketSharp;
using WebSocketSharp.Net;

using Kinect = Windows.Kinect;

public class JointDataSender : MonoBehaviour
{

    WebSocket ws;
    // Start is called before the first frame update
    void Start()
    {
        ws = new WebSocket("ws://drumath-unity-web-socket.herokuapp.com");
        ws.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var j in JointToBoneAdapter.j_b_pair)
        {
            SendJointData(j.Key);
        }
    }

    void SendJointData(Kinect.JointType joint)
    {
        var obj = GameObject.Find(joint.ToString());
        var data = new TrackedJointData();

        data.JointName = joint.ToString();

        data.Posx = obj.transform.position.x;
        data.PosY = obj.transform.position.y;
        data.PosZ = obj.transform.position.z;

        data.RotW = obj.transform.rotation.w;
        data.RotX = obj.transform.rotation.x;
        data.RotY = obj.transform.rotation.y;
        data.RotZ = obj.transform.rotation.z;

        ws.Send(JsonUtility.ToJson(data));
    }

    private void OnDestroy()
    {
        ws.Close();
    }
}
