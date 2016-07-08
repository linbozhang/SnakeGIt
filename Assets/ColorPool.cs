using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
namespace SnakeOffline
{
    public class ColorPool : MonoBehaviour
    {

        public static ColorPool current;
        void Awake()
        {
            current = this;
        }

        public Color getRandomFoodColor()
        {
            return foodColors[ Random.Range(0, foodColors.Length)];
        }

        public Color getBodyColorWithIndex(int index)
        {
            if(index>=0&&index<bodyColors.Length)
            {
                return bodyColors[index];
            }
            return Color.white;
            
        }

        public Color[] foodColors;
        public Color[] bodyColors;
        

        [ContextMenu("fileColorToPrefab")]
        public void FileToPrefab()
        {
            bodyColors = new Color[colorPool.Length];
            for(int i=0;i<bodyColors.Length;i++)
            {
                bodyColors[i] = colorPool[i];
            }
            foodColors = new Color[colorPool.Length];
            for (int i = 0; i < foodColors.Length; i++)
            {
                foodColors[i] = colorPool[i];
            }
        }


        [ContextMenu("prefabColorToFood")]
        public void PrefabToFile()
        {
        }

        public static Color[] colorPool = new Color[] {
            new Color(200/255.0f, 32/255.0f, 32/255.0f),//0
            new Color(255/255.0f,114/255.0f,  0/255.0f),//1
            new Color(255/255.0f,226/255.0f, 49/255.0f),//2
            new Color( 34/255.0f,145/255.0f,107/255.0f),//3
            new Color( 25/255.0f,183/255.0f,235/255.0f),//4
            new Color( 60/255.0f, 95/255.0f,240/255.0f),//5
            new Color( 86/255.0f, 37/255.0f,203/255.0f),//6
            new Color(200/255.0f, 70/255.0f, 70/255.0f),//7
            new Color( 54/255.0f, 52/255.0f, 52/255.0f),//8
            new Color( 60/255.0f, 95/255.0f,240/255.0f),//9
            new Color(230/255.0f,230/255.0f,230/255.0f),//10
            new Color( 25/255.0f,183/255.0f,235/255.0f),//11
            new Color(200/255.0f, 70/255.0f, 70/255.0f),//12
            new Color( 86/255.0f, 37/255.0f,203/255.0f),//13
            new Color( 60/255.0f, 95/255.0f,240/255.0f),//14
            new Color(206/255.0f,207/255.0f,206/255.0f),//15
            new Color(255/255.0f,226/255.0f, 49/255.0f),//16
            new Color(255/255.0f,255/255.0f,255/255.0f),//17
            new Color(245/255.0f, 40/255.0f, 40/255.0f),//18
            new Color( 18/255.0f, 97/255.0f,154/255.0f),//19
            new Color(245/255.0f,255/255.0f,  0/255.0f),//20
            new Color(234/255.0f,234/255.0f,234/255.0f),//21
            new Color(211/255.0f,  0/255.0f,  0/255.0f),//22

            new Color(255/255.0f,0,0.0f,1),
            new Color(0xfe/255.0f,0x57/255.0f,0x09/255.0f,1) ,
            new Color(0xff/255.0f,0xe8/255.0f,0x46/255.0f,1) ,
            new Color(0x22/255.0f,0x91/255.0f,0x69/255.0f,1) ,
            new Color(0x66/255.0f,0xca/255.0f,0xeb/255.0f,1)
        };


    }
}

