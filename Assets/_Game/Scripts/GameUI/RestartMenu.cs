using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DeerCat.SimpleTween;

namespace Game.UI
{
    public class RestartMenu : MenuScreen
    {

        public CanvasGroup newRecordScreen;
        public CanvasGroup normalScreen;
        public Text historyKill;
        public Text historyScore;
        
        
        
        protected override void Awake()
        {
            base.Awake();
        }

        public override void Show(float fade, TransitionDirection direction = TransitionDirection.Forward, Callback callback = null)
        {
            if(direction==TransitionDirection.Forward)
            {
                float score = 1000;
                float kill = 20;
                float recore = 1000;
                if(score==recore)
                {
                    normalScreen.alpha = 0.0f;
                    newRecordScreen.alpha = 1.0f;

                    SimpleTweener.AddTween(() => 0, x => historyScore.text = Mathf.CeilToInt(x).ToString() + "m", recore, 0.5f).Delay(0.2f).OnCompleted(() =>
                               {
                                   KickItem(historyScore.transform, 1.5f, 0.5f);
                               }
                    );
                }else
                {
                    normalScreen.alpha = 1.0f;
                    newRecordScreen.alpha = 0.0f;
                    SimpleTweener.AddTween(() => 0, x => historyScore.text = Mathf.CeilToInt(x).ToString() + "m", score, 0.5f).Delay(0.2f).OnCompleted(() =>
                               {
                                   KickItem(historyScore.transform, 1.5f, 0.3f);
                               });
                    SimpleTweener.AddTween(() => 0, x => historyKill.text = Mathf.CeilToInt(x).ToString() + "m", kill, 0.5f).Delay(0.2f).OnCompleted(() =>
                    {
                        KickItem(historyKill.transform, 1.5f, 0.3f);
                    });
                }
            }

            base.Show(fade, direction, callback);
        }

        private void KickItem(Transform item, float amount, float time)
        {
            // animate the scale of an item to give it a little 'kick'
            SimpleTweener.AddTween(() => item.localScale, x => item.localScale = x, amount * item.localScale, time).Ease(Easing.EaseKick);
        }
    }
}


