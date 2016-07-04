using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DeerCat.SimpleTween;
namespace Game.UI
{   
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuScreen : MonoBehaviour
    {
        [Tooltip("this item(if not null) will be set as the focused ui item when this menu screen opens")]
        public GameObject defaultUIItem;

        public enum TransitionDirection
        {
            Forward,
            Back
        }

        private CanvasGroup canvasGroup;
        private Transform panelXfm;

        private Tween alphaTween;
        private Tween rotationTween;

        public CanvasGroup CanvasGroup
        {
            get { return canvasGroup; }
        }
        
        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            panelXfm = transform.Find("Panel");
            canvasGroup.alpha = 0.0f;
        }

        public virtual void Show(float fade,TransitionDirection direction=TransitionDirection.Forward,Callback callback=null)
        {
            gameObject.SetActive(true);
            SimpleTweener.RemoveTween(alphaTween);
            SimpleTweener.RemoveTween(rotationTween);

            if(fade==0.0f)
            {
                canvasGroup.alpha = 1.0f;
                if (callback != null)
                    callback();
            }else
            {
                alphaTween = SimpleTweener.AddTween(() => CanvasGroup.alpha, x => CanvasGroup.alpha = x, 1.0f, fade).OnCompleted(callback).UseRealTime(true);
                if (panelXfm != null)
                    rotationTween = SimpleTweener.AddTween(() => new Vector3(90, 0, 0), x => panelXfm.localEulerAngles = x, Vector3.zero, fade).UseRealTime(true);
                EventSystem.current.SetSelectedGameObject(defaultUIItem);
            }

        }

        public virtual void Hide(float fade ,TransitionDirection direction=TransitionDirection.Back,Callback callback=null)
        {
            SimpleTweener.RemoveTween(alphaTween);
            SimpleTweener.RemoveTween(rotationTween);

            if(fade==0.0f)
            {
                canvasGroup.alpha = 0.0f;
                gameObject.SetActive(false);
                if (callback != null)
                    callback();
            }else
            {
                // fade out the canvas group, then disable the gameobject once it's invisible
                alphaTween = SimpleTweener.AddTween(() => CanvasGroup.alpha, x => CanvasGroup.alpha = x, 0.0f, fade).UseRealTime(true).OnCompleted(() =>
                {
                    gameObject.SetActive(false);
                    if (callback != null)
                        callback();
                });

                if (panelXfm != null)
                    rotationTween = SimpleTweener.AddTween(() => Vector3.zero, x => panelXfm.localEulerAngles = x, new Vector3(90, 0, 0), fade).UseRealTime(true);
            }
        }

    }
}

