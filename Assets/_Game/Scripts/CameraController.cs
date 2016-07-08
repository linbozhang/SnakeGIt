using UnityEngine;
using System.Collections;
namespace SnakeOffline
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController current;
		Camera mCamera;
		float startSize;
        void Awake()
        {
            current = this;
        }
		void Start()
		{
			mCamera = GetComponent<Camera> ();
			startSize = mCamera.orthographicSize;
		}

        public SnakeController followObject;
        

        void Update()
        {
            if(followObject!=null)
            {
                Vector3 pos = followObject.transform.position;
                pos.z = -10;
                transform.position =Vector3.Lerp(transform.position, pos,Time.deltaTime*2);
				mCamera.orthographicSize= startSize*(1+(followObject.length/2000f));   //Mathf.Lerp(mCamera.orthographicSize,startSize*(followObject.length/1000+1),Time.deltaTime*2);
            }
        }
        
    }
}

