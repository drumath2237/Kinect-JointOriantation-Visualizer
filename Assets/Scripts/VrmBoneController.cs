using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrmBoneController : MonoBehaviour
{
    [SerializeField] GameObject LeftBone;
    [SerializeField] GameObject LeftElbow;

    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();


        LeftBone = GameObject.Find("ShoulderRight");
        LeftElbow = GameObject.Find("ElbowRight");

    }

    // Update is called once per frame
    void Update()
    {
        _animator.GetBoneTransform(HumanBodyBones.LeftLowerArm).rotation = LeftElbow.transform.rotation;
        _animator.GetBoneTransform(HumanBodyBones.LeftShoulder).rotation = LeftBone.transform.rotation;
    }
}
