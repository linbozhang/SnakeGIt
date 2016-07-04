using UnityEngine;
using System.Collections;
using DeerCat.SimpleTween;
using UnityEngine.UI;
namespace Game.UI
{
    public class HUD : MonoBehaviour
    {
       public Text txtStart;
        public Text txtScore;
        public RectTransform scorePanel;

        private CanvasGroup canvasGroup;

        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0.0f;
        }
        
        void Update()
        {
            txtScore.text = string.Format("{0:0.0}m", 100);
        }

        public void Show()
        {
            SimpleTweener.AddTween(() => new Vector2(-250, 0), x => scorePanel.anchoredPosition = x, Vector2.zero, 0.5f);
            RectTransform rectStart = txtStart.GetComponent<RectTransform>();
            SimpleTweener.AddTween(() => Vector2.zero, x => rectStart.anchoredPosition = x, new Vector2(0,500), 0.5f);
            SimpleTweener.AddTween(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1.0f, 0.5f);
        }
        public void Hide()
        {
            SimpleTweener.AddTween(() => Vector2.zero, x => scorePanel.anchoredPosition = x, new Vector2(-250,0), 0.5f).Ease(Easing.EaseIn);
            SimpleTweener.AddTween(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0.0f, 0.5f).Ease(Easing.EaseIn);
        }
                   
    }
}

