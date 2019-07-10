using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class VrmAllBoneController : MonoBehaviour
{
    Dictionary<Kinect.JointType, GameObject> j_g_pair;
    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        j_g_pair = new Dictionary<Kinect.JointType, GameObject>();
    }

    void Update()
    {
        setJointAndObject();
        setBoneRotations();
        
    }

    void setJointAndObject()
    {
        if (j_g_pair.Count==0)
        {
            foreach(var p in JointToBoneAdapter.j_b_pair)
            {
                if (GameObject.Find(p.Key.ToString()) == null)
                {
                    //j_g_pair = null;
                    return;
                }
                j_g_pair.Add(p.Key, GameObject.Find(p.Key.ToString()));
            }
        }
    }

    void setBoneRotations()
    {
        foreach(var p in JointToBoneAdapter.j_b_pair)
        {
            _animator.GetBoneTransform(p.Value).rotation = j_g_pair[p.Key].transform.rotation;
        }
    }
}
