using UnityEngine;
using System.Collections;
namespace SnakeOffline
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController current;

        void Awake()
        {
            current = this;
        }

        public SnakeController followObject;
        

        void Update()
        {
            if(followObject!=null)
            {
                Vector3 pos = followObject.transform.position;
                pos.z = -10;
                transform.position = pos;
            }
        }
        
    }
}

