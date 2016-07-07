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
		void Start()
		{
			mCamera = GameObject.FindGameObjectWithTag ("UICamera").GetComponent<Camera> ();
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
			Vector2 dir = pos - centerPos;
			dir = dir.normalized;
			GameController.current.playerDirection = dir;
		}
		void Update()
		{
			#if UNITY_ANDROID||UNITY_IOS
			#else

			Vector2 pos= Input.mousePosition;	
			OnMove(pos);
			#endif
		}

    }
}


