using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using Kinect = Windows.Kinect;

using WebSocketSharp;
using WebSocketSharp.Net;

public class KinectJointControllerFixed : MonoBehaviour
{
    [SerializeField] GameObject m_jointObj;
    [SerializeField] GameObject bodysourcemanager;
    Kinect.Body _body;
    BodySourceManager _bodySourceManager;

    //WebSocket ws;

    GameObject Head;
    GameObject Neck;
    GameObject Spine_Shoulder;
    GameObject Spine_Mid;
    GameObject Spine_Base;

    GameObject Shoulder_Right;
    GameObject Shoulder_Left;

    GameObject Elbow_Right;
    GameObject Elbow_Left;

    GameObject Wrist_Right;
    GameObject Wrist_Left;

    GameObject Hip_Right;
    GameObject Hip_Left;

    GameObject Knee_Right;
    GameObject Knee_Left;

    GameObject Ankle_Right;
    GameObject Ankle_Left;

    GameObject Foot_Right;
    GameObject Foot_Left;

    // Start is called before the first frame update
    void Start()
    {
        _bodySourceManager = bodysourcemanager.GetComponent<BodySourceManager>();

        Head = Instantiate(m_jointObj);
        Neck = Instantiate(m_jointObj);
        Spine_Shoulder = Instantiate(m_jointObj);
        Spine_Mid = Instantiate(m_jointObj);
        Spine_Base = Instantiate(m_jointObj);
        Shoulder_Left = Instantiate(m_jointObj);
        Shoulder_Right = Instantiate(m_jointObj);
        Elbow_Left = Instantiate(m_jointObj);
        Elbow_Right = Instantiate(m_jointObj);
        Wrist_Left = Instantiate(m_jointObj);
        Wrist_Right = Instantiate(m_jointObj);
        Hip_Left = Instantiate(m_jointObj);
        Hip_Right = Instantiate(m_jointObj);
        Knee_Left = Instantiate(m_jointObj);
        Knee_Right = Instantiate(m_jointObj);
        Ankle_Left = Instantiate(m_jointObj);
        Ankle_Right = Instantiate(m_jointObj);
        Foot_Left = Instantiate(m_jointObj);
        Foot_Right = Instantiate(m_jointObj);

        //ws = new WebSocket("ws://drumath-unity-web-socket.herokuapp.com");
        //ws.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        _body = _bodySourceManager.GetData().FirstOrDefault(b => b.IsTracked);

        if (_body.IsTracked)
        {
            SetBoneTransform(Head, Kinect.JointType.Head);
            SetBoneTransform(Neck, Kinect.JointType.Neck);
            SetBoneTransform(Spine_Shoulder, Kinect.JointType.SpineShoulder);
            SetBoneTransform(Spine_Mid, Kinect.JointType.SpineMid);
            SetBoneTransform(Spine_Base, Kinect.JointType.SpineBase);

            SetBoneTransform(Shoulder_Left, Kinect.JointType.ShoulderLeft);
            SetBoneTransform(Shoulder_Right, Kinect.JointType.ShoulderRight);

            SetBoneTransform(Elbow_Left, Kinect.JointType.ElbowLeft);
            SetBoneTransform(Elbow_Right, Kinect.JointType.ElbowRight);

            SetBoneTransform(Wrist_Left, Kinect.JointType.WristLeft);
            SetBoneTransform(Wrist_Right, Kinect.JointType.WristRight);

            SetBoneTransform(Hip_Left, Kinect.JointType.HipLeft);
            SetBoneTransform(Hip_Right, Kinect.JointType.HipRight);

            SetBoneTransform(Knee_Left, Kinect.JointType.KneeLeft);
            SetBoneTransform(Knee_Right, Kinect.JointType.KneeRight);

            SetBoneTransform(Ankle_Left, Kinect.JointType.AnkleLeft);
            SetBoneTransform(Ankle_Right, Kinect.JointType.AnkleRight);

            SetBoneTransform(Foot_Left, Kinect.JointType.FootLeft);
            SetBoneTransform(Foot_Right, Kinect.JointType.FootRight);

            SetBonesParents();

            FixBonesLocalRotation();
        }
    }

    Vector3 kinectPos2Vec3(Kinect.JointType type)
    {
        return new Vector3(_body.Joints[type].Position.X, _body.Joints[type].Position.Y, _body.Joints[type].Position.Z) * 10f;
    }

    Quaternion Vec4toQuaternion(Kinect.JointType type)
    {
        return new Quaternion(_body.JointOrientations[type].Orientation.X,
                                _body.JointOrientations[type].Orientation.Y,
                                _body.JointOrientations[type].Orientation.Z,
                                _body.JointOrientations[type].Orientation.W
                            );
    }

    void SetBoneTransform(GameObject bone, Kinect.JointType type)
    {
        bone.transform.position = kinectPos2Vec3(type);
        bone.transform.localRotation = Vec4toQuaternion(type);

        bone.name = type.ToString();
    }

    void FixBonesLocalRotation()
    {
        Head.transform.localRotation *= Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
        Shoulder_Left.transform.localRotation *= Quaternion.AngleAxis(90, new Vector3(0, 0, 1));
        Shoulder_Right.transform.localRotation *= Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));

        Hip_Right.transform.localRotation *= Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
        Hip_Left.transform.localRotation *= Quaternion.AngleAxis(90, new Vector3(0, 0, 1));

        Elbow_Right.transform.localRotation *= Quaternion.AngleAxis(90, new Vector3(0, 0, 1)) * Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
        Elbow_Left.transform.localRotation *= Quaternion.AngleAxis(-90, new Vector3(0, 0, 1)) * Quaternion.AngleAxis(180, new Vector3(0, 1, 0));

        Knee_Right.transform.localRotation *= Quaternion.AngleAxis(-90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(180, new Vector3(0, 0, 1));
        Knee_Left.transform.localRotation *= Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(180, new Vector3(0, 0, 1));

        Ankle_Right.transform.localRotation *= Quaternion.AngleAxis(-90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(180, new Vector3(0, 0, 1));
        Ankle_Left.transform.localRotation *= Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) * Quaternion.AngleAxis(180, new Vector3(0, 0, 1));

        Foot_Left.transform.localRotation *= Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
        Foot_Right.transform.localRotation *= Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
    }

    void SetBonesParents()
    {
        //Wrist_Right.transform.parent = Elbow_Right.transform;
        //Elbow_Right.transform.parent = Shoulder_Right.transform;

        //Ankle_Right.transform.parent = Knee_Right.transform;
        //Knee_Right.transform.parent = Hip_Right.transform;
    }
}
