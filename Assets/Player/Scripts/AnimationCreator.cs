using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AnimationCreator : MonoBehaviour
{
    private Animator animator = null;
    private JsonData jsonTemp = new JsonData();
    private float[] attackPara = new float[6];
    private float[] GuardPara = new float[6];
    private float[] attack2Para = new float[6];

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        loadMotion();
        CreateAnime("attack");
        CreateAnime("Guard");
        CreateAnime("attack2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateAnime(string para)
    {
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        AnimationClip animationClip = null;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name.Equals(para))
            {
                animationClip = ac.animationClips[i];
                break;
            }
        }
        animationClip.ClearCurves();

        string[] relativePath = { "Skeleton/rootBone/leftArm"
                , "Skeleton/rootBone/leftArm/leftArm2"
                , "Skeleton/rootBone/rightArm"
                , "Skeleton/rootBone/rightArm/rightArm2" 
                , "Skeleton/rootBone" 
                , "Skeleton/rootBone/head"
                };
        string[] propertyName = { "localEulerAngles.z", "localEulerAngles.z"
                , "localEulerAngles.z", "localEulerAngles.z"
                , "localEulerAngles.z", "localEulerAngles.z" };
        float[] la = { -152.28f, -152.28f, -152.28f, -152.28f };
        float[] la2 = { -157.26f, -157.26f, -157.26f, -157.26f };
        float[] ra = { 167.66f, 96.043f, 96.043f, 167.66f };
        float[] ra2 = { -145.5f, -11.204f, -11.204f, -145.5f };
        float[] rb = { -270.12f, -270.12f, -270.12f, -270.12f };
        float[] h = { -0.712f, -0.712f, -0.712f, -0.712f };

        if (para.Equals("attack"))
        {
            la[1] = attackPara[0]; la[2] = attackPara[0];
            la2[1] = attackPara[1]; la2[2] = attackPara[1];
            ra[1] = attackPara[2]; ra[2] = attackPara[2];
            ra2[1] = attackPara[3]; ra2[2] = attackPara[3];
            rb[1] = attackPara[4]; rb[2] = attackPara[4];
            h[1] = attackPara[5]; h[2] = attackPara[5];
        }
        if (para.Equals("Guard"))
        {
            la[1] = GuardPara[0]; la[2] = GuardPara[0];
            la2[1] = GuardPara[1]; la2[2] = GuardPara[1];
            ra[1] = GuardPara[2]; ra[2] = GuardPara[2];
            ra2[1] = GuardPara[3]; ra2[2] = GuardPara[3];
            rb[1] = GuardPara[4]; rb[2] = GuardPara[4];
            h[1] = GuardPara[5]; h[2] = GuardPara[5];
        }
        if (para.Equals("attack2"))
        {
            la[1] = attack2Para[0]; la[2] = attack2Para[0];
            la2[1] = attack2Para[1]; la2[2] = attack2Para[1];
            ra[1] = attack2Para[2]; ra[2] = attack2Para[2];
            ra2[1] = attack2Para[3]; ra2[2] = attack2Para[3];
            rb[1] = attack2Para[4]; rb[2] = attack2Para[4];
            h[1] = attack2Para[5]; h[2] = attack2Para[5];
        }

        float[][] property = new float[6][];
        property[0] = la; property[1] = la2; property[2] = ra;
        property[3] = ra2; property[4] = rb; property[5] = h;

        for (int i = 0; i < 6; i++)
        {
            Keyframe[] keys = new Keyframe[4];
            keys[0] = new Keyframe(0.0f, property[i][0]);
            keys[1] = new Keyframe(1.0f / 6.0f, property[i][1]);
            keys[2] = new Keyframe(2.0f / 6.0f, property[i][2]);
            keys[3] = new Keyframe(0.5f, property[i][3]);
            AnimationCurve curve = new AnimationCurve(keys);
            animationClip.SetCurve(relativePath[i], typeof(Transform), propertyName[i], curve);
        }
    }

    private void loadMotion()
    {
        string json = "";
        int index = -1;
        if(name.Equals("player1"))
        {
            index = 0;
        }
        if (name.Equals("player2"))
        {
            index = 1;
        }

        if (File.Exists(JsonPath(index)))
        {
            StreamReader sr = new StreamReader(JsonPath(index));
            json = sr.ReadToEnd();
            sr.Close();
        }
        if (!json.Equals(""))
        {
            jsonTemp = JsonUtility.FromJson<JsonData>(json);
        }
        for(int i=0;i<jsonTemp.MotionList.Count;i++)
        {
            if(jsonTemp.MotionList[i].motionIndex == 0)
            {
                int c1 = jsonTemp.MotionList[i].boneIndex;
                attackPara[c1] = jsonTemp.MotionList[i].value;
            }
            if (jsonTemp.MotionList[i].motionIndex == 1)
            {
                int c2 = jsonTemp.MotionList[i].boneIndex;
                GuardPara[c2] = jsonTemp.MotionList[i].value;
            }
            if (jsonTemp.MotionList[i].motionIndex == 2)
            {
                int c3 = jsonTemp.MotionList[i].boneIndex;
                attack2Para[c3] = jsonTemp.MotionList[i].value;
            }
        }
    }

    private string JsonPath(int index)
    {
        Debug.Log(Application.persistentDataPath + "/player" + (index + 1) + ".json");
        return Application.persistentDataPath + "/player" + (index + 1) + ".json";
    }
}
