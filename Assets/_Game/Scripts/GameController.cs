using UnityEngine;
using System.Collections;
using Game.UI;
using DeerCat.SimpleTween;
namespace SnakeOffline
{
    public class GameController : MonoBehaviour
    {

        public enum GameState
        {
            InMenus,
            Playing,
            Paused,
            Dead
        }

        private static GameController current;

        public MenuSystem menuSystem;

        private MenuScreen startMenu;
        private MenuScreen restartMenu;
        private CanvasGroup screenFade;
        private DeerCat.SimpleTween.Tween fadeTween;

        private GameState state;

        public static MenuSystem MenuSystem
        {
            get { return current ? current.menuSystem : null; }
        }

        public static GameState CurrentState
        {
            get { return current.state; }
        }

        void Awake()
        {
            current = this;
            menuSystem.enabled = true;
        }

        // Use this for initialization
        void Start()
        {
            menuSystem = GameObject.FindObjectOfType<MenuSystem>();
            startMenu = menuSystem.getScreen("StartMenu");
            restartMenu = menuSystem.getScreen("RestartMenu");
            screenFade = GameObject.Find("ScreenFade").GetComponent<CanvasGroup>();
            OnEnterStateMenu();
        }

        private void OnEnterStateMenu()
        {
            Debug.Log("OnEnterStateMenu");
            menuSystem.ShowScreen(startMenu,0.5f);
            state = GameState.InMenus;
            Game.SoundManager.PlayMusic("IntroLoop");
            ScreenFaderFadeOut(1.0f, null);
        }



        private void OnEnterStateGame()
        {
            Debug.Log("OnEnterStateGame");
            menuSystem.CurrentScreen.Hide(0.5f);
            menuSystem.HUD.Show();
            state = GameState.Playing;
            Game.SoundManager.PlayMusic("GameLoop");
            ScreenFaderFadeOut(0.4f);
        }

        private void OnEnterStateDeath()
        {

        }

        private void ScreenFaderFadeIn(float time,Callback onCompleteCB=null)
        {
            if(fadeTween!=null)
            {
                SimpleTweener.RemoveTween(fadeTween);
            }
            screenFade.gameObject.SetActive(true);
            screenFade.alpha = 0.0f;
            fadeTween = SimpleTweener.AddTween(() => screenFade.alpha, x => screenFade.alpha = x, 1.0f, time).OnCompleted(onCompleteCB);
        }

        private void ScreenFaderFadeOut(float time ,Callback onCompleteCB=null)
        {
            if (screenFade.gameObject.activeSelf)
                fadeTween = SimpleTweener.AddTween(() => screenFade.alpha, x => screenFade.alpha = x, 0.0f, time).OnCompleted(() => screenFade.gameObject.SetActive(false));
        }
        public static void FlashScreen(float fadeTime=0.5f)
        {
            current.screenFade.gameObject.SetActive(true);
            current.screenFade.alpha = 1.0f;
            current.ScreenFaderFadeOut(fadeTime,null);
        }
        

        public void EnterStateGame()
        {
            OnEnterStateGame();
        }
        


        // Update is called once per frame
        void Update()
        {

        }
    }
}

