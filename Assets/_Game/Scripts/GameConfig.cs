using UnityEngine;
using System.Collections;
namespace SnakeOffline
{
    public class GameConfig
    {
        public static Vector2 AnchorPos = Vector2.zero; //the anchor of map ,bottom-left

        public const float MapRadius = 100;     //the radius of map

        public const float FoodRectSize= 0.1f;
        


        public const float SnakeBodyInterval = 0.9f;
        public const float SnakeBodySize = 0.61f;    //the size of Image for SnakeBody in pixel is 61*61
        public const float SnakeBaseScale = 0.4f;   //the initScale of Snake is (0.4f,0.4f,0.4f)
        



        
    }


}

