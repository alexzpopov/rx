using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using System;
using System.Collections.Generic;


public class task1 : MonoBehaviour
{

    private GameData gameData;
    private GeometryObjectData geometryObjectData;

    private itemName items;
    private TextAsset jsonList;
    // Start is called before the first frame update


    IEnumerator AsynLoadData()
    {
        yield return Resources.LoadAsync<GeometryObjectData>("ScriptObjects/GeometryObjectData").AsAsyncOperationObservable()
            .Subscribe(xs=> { geometryObjectData = xs.asset as GeometryObjectData; });
    }
    IEnumerator AsyncLoadJson()
    {
        yield return Resources.LoadAsync<TextAsset>("Json/Objects").AsAsyncOperationObservable()
            .Subscribe(xs => { jsonList = xs.asset as TextAsset; });
    }

    void Start()
    {
        //loadresources
        gameData = Resources.Load<GameData>("ScriptObjects/GameData");

        Observable.WhenAll(
      Observable.FromCoroutine(AsynLoadData),
      Observable.FromCoroutine(AsyncLoadJson)
    ).Subscribe(xs => {
        items = JsonUtility.FromJson<itemName>((jsonList as TextAsset).text);
        waitForClick();
    }).AddTo(this);

        
    }

    void waitForClick()
    {
        var clickStream = Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0));
        clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(250)))
            .Subscribe(xs => {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Transform objectHit = hit.transform;
                    string _tag = objectHit.gameObject.tag;
                     //Debug.Log(tag);
                    if (_tag.CompareTo("Player") != 0)
                    {
                        CreateFigures();
                    }
                }
            }
            );
        
    }

    void CreateFigures()
    {
        int ind = UnityEngine.Random.Range(0, items.name.Count);
        string AssetBundlePath = Application.streamingAssetsPath + "/AssetBundles/";

        AssetBundle.LoadFromFileAsync(AssetBundlePath + items.name[ind]).AsAsyncOperationObservable()
            .Subscribe(xs =>
            {

                GameObject[] allObjectsBundle1Prefabs = xs.assetBundle.LoadAllAssets<GameObject>();
                //  Debug.Log(ind);

                GameObject obj = Instantiate(allObjectsBundle1Prefabs[0]);
                xs.assetBundle.Unload(false);

                RandomColor randomColor = obj.GetComponent<RandomColor>();
                randomColor.StartUpdate(gameData.ObservableTime, geometryObjectData.ClicksData[ind].Color, ind);
                onClickFigure();
            });
    }
    void onClickFigure()
    {
        var clickStream = Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0));
        clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(250)))
            .Subscribe(xs => { 
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Transform objectHit = hit.transform;
                    string _tag = objectHit.gameObject.tag;
                    
                    //Debug.Log(tag);
                    if (_tag.CompareTo("Player")==0)
                    {
                        RandomColor randomColor = objectHit.gameObject.GetComponent<RandomColor>();
                        int _index = randomColor.myIndex;
                        ClickColorData itemData = geometryObjectData.ClicksData[_index];
                        randomColor.AddCLick(itemData);
                    }
                }
            }
            );
    }
    void makeJson()
    {
        items.name.Add("cube");
        items.name.Add("sphere");
        items.name.Add("capsule");
        var ss = JsonUtility.ToJson(items, true);
        Debug.Log(ss);
    }
}
