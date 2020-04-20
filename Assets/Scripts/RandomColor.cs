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
        geometryObjectModel.CubeColor = _color;
       
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
        geometryObjectModel.ClickCount++;
        mat.color = geometryObjectModel.CubeColor;
    }
    public void AddCLick(ClickColorData itemData)
    {
        geometryObjectModel.ClickCount++;
 

        if (geometryObjectModel.ClickCount >= itemData.MinClicksCount && geometryObjectModel.ClickCount <= itemData.MaxClicksCount)
        {
            ChangeColor();
        }
        if (geometryObjectModel.ClickCount >= itemData.MaxClicksCount)
        {
            geometryObjectModel.ClickCount = 0;
        }
    }
}
