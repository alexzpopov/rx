using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GeometryObjectData", fileName = "GeometryObjectData")]
public class GeometryObjectData : ScriptableObject
{
 public List<ClickColorData> clicksData=new List<ClickColorData>();
}

//model of (GeometryObjectModel):
[System.Serializable]
public class GeometryObjectModel
{
 public int clickCount;
 public Color cubeColor;

}
[System.Serializable]
public class ClickColorData
{ 
 public string objectType;//object type  (cube, sphere, capsule)
 public int minClicksCount;
 public int maxClicksCount;
 public Color color;
}
[System.Serializable]
public class ItemName
{
    public List<string> name= new List<string>();
 
} 