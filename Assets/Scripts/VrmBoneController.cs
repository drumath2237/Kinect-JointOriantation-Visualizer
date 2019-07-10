using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrmBoneController : MonoBehaviour
{
    GameObject Head;
    GameObject Neck;
    GameObject Shoulder;

    GameObject LeftShoulder, RightShoulder;
    GameObject LeftElbow, RightElbow;
    GameObject LeftWrist, RightWrist;

    GameObject Chest;
    GameObject Spine;
    GameObject Hip;

    GameObject LeftKnee, RightKnee;
    GameObject LeftAnkle, RightAnkle;
    GameObject LeftFoot, RightFoot;


    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(LeftShoulder==null)
            CheckObjects();

        _animator.GetBoneTransform(HumanBodyBones.LeftLowerArm).rotation = LeftElbow.transform.rotation;
        _animator.GetBoneTransform(HumanBodyBones.LeftShoulder).rotation = LeftShoulder.transform.rotation;

        _animator.GetBoneTransform(HumanBodyBones.RightShoulder).rotation = RightShoulder.transform.rotation;
        _animator.GetBoneTransform(HumanBodyBones.RightLowerArm).rotation = RightElbow.transform.rotation;

        _animator.GetBoneTransform(HumanBodyBones.Spine).rotation = Spine.transform.rotation;
    }

    void CheckObjects()
    {
        LeftShoulder = GameObject.Find("ShoulderRight");
        LeftElbow = GameObject.Find("ElbowRight");

        RightShoulder = GameObject.Find("ShoulderLeft");
        RightElbow = GameObject.Find("ElbowLeft");

        Spine = GameObject.Find("SpineBase");
    }
}
