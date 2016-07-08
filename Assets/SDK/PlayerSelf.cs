using UnityEngine;
using System.Collections;

public class PlayerSelf{

    private static PlayerSelf _instance;
    public static PlayerSelf Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerSelf();
            }
            return _instance;
        }
    }

    public void CreateData()
    {

    }

}
