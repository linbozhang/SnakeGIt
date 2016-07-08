using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class LoadResource : MonoBehaviour {

    private bool isResLoaded = false;
    private string[] loadFiles = new string[2] { "res.db", "resl.db"};
    private static LoadResource instance;
    void Awake()
    {
        Debug.Log("awake");
        instance = this;
    }

    //[RuntimeInitializeOnLoadMethod]
    // static void loadRes()
    //{

    //    Debug.Log("loadRes");
    //    instance.StartCoroutine(instance.LoadFile());
    //    //this.StartCoroutine(LoadFile());
    //}
    System.Action loadOverCallback;
    public void LoadRes(System.Action overCallback)
    {
        loadOverCallback = overCallback;
        StartCoroutine(LoadFile());
    }        
    IEnumerator LoadFile()
    {
        string rootPath = Application.streamingAssetsPath;

        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            rootPath = "file://" + rootPath;
        }
        for (int i = 0; i < loadFiles.Length; i++)
        {
            string filePath = rootPath + "/" + loadFiles[i];

            if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
            {
                File.Copy(filePath, Application.persistentDataPath + "/" + loadFiles[i], true);
            }
            else
            {
                WWW www = new WWW(filePath);
                yield return www;
                FileStream sw = new FileStream(Application.persistentDataPath + "/" + loadFiles[i], FileMode.Create);
                sw.Write(www.bytes, 0, www.bytes.Length);
                //while (!www.isDone)
                //{
                //    yield return new WaitForSeconds(0.05f);
                //}
                sw.Close();
            }

        }
        Dictionary<string, string> vfMap = ZipUtility.readAllVF();
        foreach (KeyValuePair<string, string> pair in vfMap)
        {
            string[] arr = pair.Key.Split('/');
            DataCache.LoadData(arr[arr.Length - 1].Split('.')[0], pair.Value);
        }
        LocalData.getInstance();
        if(loadOverCallback!=null)
        {
            loadOverCallback();
        }
        isResLoaded = true;
        
        yield return null;
    }
}
