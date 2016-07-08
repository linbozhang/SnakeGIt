using UnityEngine;
using System.Collections;
namespace SnakeOffline
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController current;
		public Camera mCamera;
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
        

        void LateUpdate()
        {
            if(followObject!=null)
            {
                Vector3 pos = followObject.transform.position;
                pos.z = -10;
                transform.position =Vector3.Lerp(transform.position, pos,Time.deltaTime*3);
				mCamera.orthographicSize= startSize*(1+(followObject.length/2000f));   //Mathf.Lerp(mCamera.orthographicSize,startSize*(followObject.length/1000+1),Time.deltaTime*2);
            }
        }
        
    }
}

