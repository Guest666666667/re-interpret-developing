using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class writeShow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sequence wShow(Sequence flash,int Num)
    {
        if (flash == null)
        {
            flash = DOTween.Sequence();
        }
        int i = 0;
        foreach (Image t in GetComponentsInChildren<Image>())
        {
            i++;
            t.material = new Material(Resources.Load<Material>("UI source/writeMaterial"));
            t.material.SetTexture("_SplineTex", Resources.Load<Texture>("UI source/" + Num + "/" + Num + "_" + i + "_" + "a"));
            flash.Append(DOTween.To(() => t.material.GetFloat("_CurrentAngle"), x => t.material.SetFloat("_CurrentAngle", x), 1f, 0.4f / GetComponentsInChildren<Image>().Length));
        }
        return flash;
    }
}
