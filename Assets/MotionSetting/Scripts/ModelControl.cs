using Anima2D;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class ModelInfo
{
    public int modelIndex;
    public string modelpath;
}
public class ModelData
{
    public List<ModelInfo> ModelList = new List<ModelInfo>();
}
public class ModelIndexMap
{
    public GameObject gameObject;
    public int index;
}

public class ModelControl : MonoBehaviour
{

    private List<ModelIndexMap> map = new List<ModelIndexMap>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 11; i++)
        {
            map.Add(new ModelIndexMap());
            map[i].index = i;
        }

        {
            map[0].gameObject = GameObject.Find(name + "/model/头");
            map[1].gameObject = GameObject.Find(name + "/model/身体");
            map[2].gameObject = GameObject.Find(name + "/model/身体下半");
            map[3].gameObject = GameObject.Find(name + "/model/左前臂");
            map[4].gameObject = GameObject.Find(name + "/model/左后臂");
            map[5].gameObject = GameObject.Find(name + "/model/左手");
            map[6].gameObject = GameObject.Find(name + "/model/右前臂");
            map[7].gameObject = GameObject.Find(name + "/model/右后臂");
            map[8].gameObject = GameObject.Find(name + "/model/右手");
            map[9].gameObject = GameObject.Find(name + "/model/左腿");
            map[10].gameObject = GameObject.Find(name + "/model/右腿");
        }

        LoadModel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadModel()
    {
        string json = "";
        ModelData modelData = new ModelData();
        if (File.Exists(JsonPath()))
        {
            StreamReader sr = new StreamReader(JsonPath());
            json = sr.ReadToEnd();
            sr.Close();
        }
        if (!json.Equals(""))
        {
            modelData = JsonUtility.FromJson<ModelData>(json);
        }
        for (int i = 0; i < modelData.ModelList.Count; i++)
        {
            int index = modelData.ModelList[i].modelIndex;

            /*map[index].gameObject.GetComponent<SpriteMeshInstance>().spriteMesh = 
                AssetDatabase.LoadAssetAtPath<SpriteMesh>
                (modelData.ModelList[i].modelpath);
            map[index].gameObject.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;*/
        }
    }

    private void SaveModelExample()
    {
        ModelData jsonTemp1 = new ModelData();
        for(int i=0;i<map.Count;i++)
        {
            ModelInfo modelInfo = new ModelInfo();
            modelInfo.modelIndex = i;
            modelInfo.modelpath = "path:"+i;
            jsonTemp1.ModelList.Add(modelInfo);
        }
        string result1 = JsonUtility.ToJson(jsonTemp1, true);
        StreamWriter sw1 = new StreamWriter(JsonPath(), false);
        sw1.Write(result1);
        sw1.Flush();
        sw1.Close();
    }

    private string JsonPath()
    {
        return Application.persistentDataPath + "/" + name + "_modelpath" + ".json";
    }
}
