using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DeerCat.SimpleTween;
namespace Game.UI
{
    public class MenuSystem : MonoBehaviour
    {

        private Dictionary<string, MenuScreen> screens = new Dictionary<string, MenuScreen>();

        private Stack<MenuScreen> screenHistory = new Stack<MenuScreen>();

        private HUD hud;


        public MenuScreen CurrentScreen
        {
            get { return screenHistory.Count == 0 ? null : screenHistory.Peek(); }
        }
        public HUD HUD
        {
            get { return hud; }
        }

        void Awake()
        {
            MenuScreen[] allScreen = GameObject.FindObjectsOfType<MenuScreen>();
            for(int i=0;i<allScreen.Length;i++)
            {
                screens[allScreen[i].name] = allScreen[i];
            }
            hud = GameObject.FindObjectOfType<HUD>();
        }


        // Use this for initialization
        void Start()
        {
            MenuScreen[] allScreen = GameObject.FindObjectsOfType<MenuScreen>();
            MenuScreen currentScreen = CurrentScreen;
            for (int i = 0; i < allScreen.Length; i++)
            {
                if(allScreen[i]!=currentScreen)
                {
                    allScreen[i].gameObject.SetActive(false);
                }
                
                //screens[allScreen[i].name] = allScreen[i];
            }
        }


        public MenuScreen getScreen(string name)
        {
            return screens[name];
        }


        public void ShowScreen(MenuScreen screen ,float fade=0.5f,DeerCat.SimpleTween.Callback callback=null)
        {
            if(screen==null)
            {
                Debug.LogWarning("");
                return;
            }
            MenuScreen currentScreen = CurrentScreen;
            screenHistory.Push(screen);

            if(currentScreen!=null)
            {
                currentScreen.Hide(fade, MenuScreen.TransitionDirection.Forward, () =>
                  {
                      screen.Show(fade, MenuScreen.TransitionDirection.Forward, callback);
                  });
                //currentScreen.hide -screen.show
            }else
            {
                screen.Show(fade, MenuScreen.TransitionDirection.Forward, callback);
                //screen.show
            }

        }


        public void GoBack(float fade=0.5f,DeerCat.SimpleTween.Callback callback=null)
        {
            if(screenHistory.Count==0)
            {
                return;
            }

            MenuScreen currentScreen = screenHistory.Pop();
            MenuScreen prevScreen = screenHistory.Count == 0 ? null : screenHistory.Peek();

            if(prevScreen!=null)
            {
                currentScreen.Hide(fade, MenuScreen.TransitionDirection.Back, () =>
                  {
                      prevScreen.Show(fade, MenuScreen.TransitionDirection.Back, callback);
                  });
                //current.hide-prevscreen.show
            }else
            {
                currentScreen.Hide(fade, MenuScreen.TransitionDirection.Back, callback);
                //hide
            }
        }
        

        public void ExitAll(float fade=0.5f,DeerCat.SimpleTween.Callback callback=null)
        {
            if(screenHistory.Count==0)
            {
                return;
            }

            MenuScreen currentScreen = screenHistory.Pop();
            //hide
            screenHistory.Clear();
        }
    }
}

