using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SnakeOffline;
namespace Game.UI
{
    public class TouchCanvas : EventTrigger
    {


		int mTouchId=-2;

		Vector2 centerPos;
		Camera mCamera;
        const float screenWidth = 800;
        const float screenHeight = 600;
        float scaleX = Screen.width / screenWidth;
        float scaleY = Screen.height / screenHeight;
        Rect parentRect;


        void Start()
		{
			mCamera = GameObject.FindGameObjectWithTag ("UICamera").GetComponent<Camera> ();
            parentRect = GetComponent<RectTransform>().rect;
            //Debug.Log(parentRect);
            #if UNITY_ANDROID||UNITY_IOS
				
			#else
			centerPos=mCamera.ViewportToScreenPoint(Vector2.one*0.5f);
			#endif
		}

		public override void OnPointerEnter (PointerEventData eventData)
		{
			mTouchId = eventData.pointerId;
			//base.OnPointerEnter (eventData);
		}
		public override void OnPointerExit (PointerEventData eventData)
		{
			mTouchId = eventData.pointerId;
			//base.OnPointerExit (eventData);
		}
		void OnMove(Vector2 pos)
		{

           // Debug.Log("mousePos:"+pos);

            pos.x = pos.x / Screen.width * parentRect.width + parentRect.x;
            pos.y = pos.y / Screen.height * parentRect.height + parentRect.y;
           // Debug.Log("aftmousePos:" + pos);
            Vector2 dir = pos - Vector2.zero;
			dir = dir.normalized;
			GameController.current.playerDirection = dir;
		}
		void Update()
		{
            if(GameController.current.player)
            {
#if UNITY_ANDROID || UNITY_IOS
#else

                Vector2 pos = Input.mousePosition;
                OnMove(pos);
#endif
            }

        }

    }
}


