using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class RandomColor : MonoBehaviour
{
    public GeometryObjectModel geometryObjectModel;
    public int myIndex;
    Material mat;
    int timeInterval;

    // Start is called before the first frame update
    void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
        geometryObjectModel = new GeometryObjectModel();
    }
    public void StartUpdate(int _time, Color _color,int _index)
    {
        myIndex = _index;
        geometryObjectModel.cubeColor = _color;
       
        mat.color = _color;

        timeInterval = _time;
        Observable.Timer(System.TimeSpan.FromSeconds(timeInterval))
            .Repeat()
            .Subscribe(_ =>
            {
                mat.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                // Debug.Log(mat.color);
            }).AddTo(this);
    }
    public void ChangeColor()
    {
        geometryObjectModel.clickCount++;
        mat.color = geometryObjectModel.cubeColor;
    }
    public void AddCLick(ClickColorData itemData)
    {
        geometryObjectModel.clickCount++;
 

        if (geometryObjectModel.clickCount >= itemData.minClicksCount && geometryObjectModel.clickCount <= itemData.maxClicksCount)
        {
            ChangeColor();
        }
        if (geometryObjectModel.clickCount >= itemData.maxClicksCount)
        {
            geometryObjectModel.clickCount = 0;
        }
    }
}
