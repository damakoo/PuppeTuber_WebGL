using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NCMB;

public class AnimationLoader : MonoBehaviour
{
    private List<bool> animLoaded = new List<bool>();
    public static List<List<List<float>>> HumanPoseAnimList = new List<List<List<float>>>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Enum.GetNames(typeof(handState)).Length; i++)
        {
            animLoaded.Add(false);
            HumanPoseAnimList.Add(new List<List<float>>());
        }
        AnimationLoading("default", 0);
        AnimationLoading("bow", 6);
        AnimationLoading("nod", 4);
    }

    private void AnimationLoading(string animation,int animnumber)
    {
        if (!animLoaded[animnumber])
        {
            NCMBFile file = new NCMBFile("Humanpose_" + animation + ".txt");
            file.FetchAsync((byte[] fileData, NCMBException error) =>
            {
                if (error != null)
                {
                    // é∏îs
                    AnimationLoading(animation,animnumber);
                }
                else
                {
                    // ê¨å˜
                    DecodeAnimation(System.Text.Encoding.UTF8.GetString(fileData),animnumber);
                    Debug.Log("anim : " + animation + " loaded");
                    animLoaded[animnumber] = true;
                }
            });
        }
    }
    private void DecodeAnimation(string content,int animnumber)
    {
        HumanPoseAnimList[animnumber] = new List<List<float>>();
        string[] line = content.Trim().Split('\n');
        for (int j = 0; j < line.Length; j++)
        {
            string[] list = line[j].Trim().Split(',');
            List<float> nowHumanPose = new List<float>();
            for (int i = 0; i < list.Length; i++)
            {
                nowHumanPose.Add(float.Parse(list[i]));
            }
            HumanPoseAnimList[animnumber].Add(nowHumanPose);
        }
    }
}
