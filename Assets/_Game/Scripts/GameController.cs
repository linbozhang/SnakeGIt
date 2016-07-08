using UnityEngine;
using System.Collections;
using Game.UI;
using DeerCat.SimpleTween;
using System.Collections.Generic;
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

        public static GameController current;

		public SnakeController player;

        public MenuSystem menuSystem;
		public MapGenerator mapGenerator;
        public FoodsController foodControl;

        private MenuScreen startMenu;
        private MenuScreen restartMenu;
        private CanvasGroup screenFade;
        private DeerCat.SimpleTween.Tween fadeTween;

        private GameState state;
        private List<SnakeController> players=new List<SnakeController>();

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
            GetComponent<LoadResource>().LoadRes(OnResLoadOver);
            //OnEnterStateMenu();
        }

        void OnResLoadOver()
        {
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

			//for (int i = 0; i < 50; i++) {
			//	CreatePlayer (false);
			//}

            CreatePlayer(true);
            Game.SoundManager.PlayMusic("GameLoop");
			mapGenerator.GeneratorMap ();
            ScreenFaderFadeOut(0.4f);
            foodControl.InitFoods();
        }

        private void OnEnterStateDeath()
        {
            menuSystem.HUD.Hide();
            foodControl.ResetFood();
            PoolManager.current.HideAll();
            for (int i = 0; i < players.Count; i++)
            {
                Destroy(players[i].gameObject);
            }


        }

		public Vector2 playerDirection
		{
			set {
				if (player != null) {
                   // Debug.Log("setDir:"+value);
					player.direction = value;
				}
			}
		}
		
		

		void CreatePlayer(bool isPlayer)
        {
			Vector2 pos = new Vector2 (Random.Range (12.8f, GameConfig.MapRadius - 12.8f), Random.Range (12.8f, GameConfig.MapRadius - 12.8f));
            GameObject playerGo = PoolManager.current.poolSnake(pos);
            SnakeController snake = playerGo.GetComponent<SnakeController>();
            snake.InitSnake(GameConfig.getRandomNormalSkinID(),
                Random.insideUnitCircle.normalized,
                PoolManager.current.GetPlayerID(),
                GameConfig.snakeInitLength*3500,
                isPlayer);
            //playerGo.GetComponent<SnakeController>().isPlayer = isPlayer;
            players.Add(snake);

			if (isPlayer) {
				player = playerGo.GetComponent<SnakeController> ();
			}
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
        
        public void EnterStateDeath()
        {
            OnEnterStateDeath();
           
                                    
        }

        // Update is called once per frame
        void Update()
        {
            if(state==GameState.Playing)
            {
                EatfoodCheck();
            }
        }

        void EatfoodCheck()
        {
            //Debug.Log("eatCheck");
            for(int i=0;i<players.Count;i++)
            {

                SnakeController eater = players[i];
                foodControl.EatCheck(eater);

            }

        }



    }
}

