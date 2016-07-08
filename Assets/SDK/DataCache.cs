using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
public class DataCache  {

    public static Dictionary<int, SnakeSkin> skinMap = new Dictionary<int, SnakeSkin>();
    public static Dictionary<int, SnakeSkin> skinNormalMap = new Dictionary<int, SnakeSkin>();
    public static Dictionary<int, SnakeSkin> skinShopMap = new Dictionary<int, SnakeSkin>();
    private static List<SnakeSkin> skinList = new List<SnakeSkin>();



    private static List<DataName> nameList=new List<DataName>();
    public static List<string> nameFirst = new List<string> ();
	public static List<string > nameMiddle = new List<string > ();
	public static List<string > nameLast = new List<string> ();
    public static void LoadData(string name,string content){
        switch(name)
        {
            case "SnakeSkin":
                skinList = JsonConvert.DeserializeObject<List<SnakeSkin>>(content);
                foreach (var item in skinList)
                {
                    if (item.type == 1)
                    {
                        skinNormalMap.Add(item.skinid, item);
                    }
                    if (item.type == 2)
                    {
                        skinShopMap.Add(item.skinid, item);
                    }
                    skinMap.Add(item.skinid, item);

                }
                break;
            case "DataName":
                nameList = JsonConvert.DeserializeObject<List<DataName>>(content);
                foreach (var item in nameList)
                {
                    if (!string.IsNullOrEmpty(item.prefix))
                    {
                        nameFirst.Add(item.prefix);
                    }
                    if (!string.IsNullOrEmpty(item.middle))
                    {
                        nameMiddle.Add(item.middle);
                    }

                    if (!string.IsNullOrEmpty(item.suffix))
                    {
                        nameLast.Add(item.suffix);
                    }
                }
                break;
        }
    }



	public static string getRandomName()
	{
		string rtn = "";
		rtn += nameFirst [Random.Range (0, nameFirst.Count)];
		rtn += nameMiddle [Random.Range (0, nameMiddle.Count)];
		rtn += nameLast [Random.Range (0, nameLast.Count)];
		return rtn;
	}

    private static Dictionary<string, GameObject> prefabPool = new Dictionary<string, GameObject>();
    private static Dictionary<string, AudioClip> soundPool = new Dictionary<string, AudioClip>();

    private static Dictionary<string, Sprite> spritePool = new Dictionary<string, Sprite>();



    public static Sprite loadSprite(string name)
    {
        if (spritePool.ContainsKey(name))
        {
            return spritePool[name];
        }else
        {
          Sprite sprite= Resources.Load<Sprite>(name);
            if(sprite!=null)
            {
                spritePool.Add(name,sprite);
                return sprite;
            }
        }
        return null;
    }

    public static GameObject  loadPrefab(string prefabName)
    {
        if(prefabPool.ContainsKey(prefabName))
        {
            return prefabPool[prefabName];
        }else
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/"+prefabName);
            if(obj!=null)
            {
                prefabPool.Add(prefabName, obj);
                return obj;
            }
        }
        return null;
    }
    public static AudioClip  loadSound(string soundName)
    {
        if(soundPool.ContainsKey(soundName))
        {
            return soundPool[soundName];
        }
        else
        {
            AudioClip clip = Resources.Load<AudioClip>("Sound/"+soundName);
            if(clip!=null)
            {

                soundPool.Add(soundName,clip);
                return clip;
            }
        }
        return null;
    }
	// Use this for initialization
	void Start () {
				
	}
	

}
