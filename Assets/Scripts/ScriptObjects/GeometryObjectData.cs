using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GeometryObjectData", fileName = "GeometryObjectData")]
public class GeometryObjectData : ScriptableObject
{
 public List<ClickColorData> ClicksData=new List<ClickColorData>();
}

//Модель фигуры (GeometryObjectModel):
[System.Serializable]
public class GeometryObjectModel
{
 public int ClickCount;
 public Color CubeColor;

}
[System.Serializable]
public class ClickColorData
{ 
 public string ObjectType;//тип создаваемого объекта (куб, сфера, капсула)
 public int MinClicksCount;
 public int MaxClicksCount;
 public Color Color;
}
[System.Serializable]
public class itemName
{
    public List<string> name= new List<string>();
 
} 