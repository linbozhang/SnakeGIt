using UnityEngine;
using System.Collections;
namespace SnakeOffline
{
    public class GameConfig
    {
        public static Vector2 AnchorPos = Vector2.zero; //the anchor of map ,bottom-left

        public const float MapRadius = 1000;     //the radius of map
        public const float HalfMapRadisu = MapRadius / 2;

        public const float FoodRectSize= 0.1f;
        


        public const float SnakeBodyInterval = 1f;
        public const float SnakeBodySize = 0.61f;    //the size of Image for SnakeBody in pixel is 61*61
        public const float SnakeBaseScale = 0.5f;   //the initScale of Snake is (0.4f,0.4f,0.4f)

        public const int snakeInitLength = 700;



        //场景内食物提供的长度
        public const float foodLowE = 1; //每个低级食物提供的长度
        public const float foodMiddleE = 2.5f; //每个中级食物提供的长度
        public const float foodHightE = 5f; //每个高级食物提供的长度

        //场景内食物生成的概率
        public const float lowFoodPro = 0.68f;       //低级食物概率
        public const float middleFoodPro = 0.31f;   //中级食物概率
        public const float hightFoodPro = 0.01f;   //高级食物概率




        public static int getRandomNormalSkinID()
        {
            return (Random.Range(0, DataCache.skinNormalMap.Keys.Count)+1);
        }
        public static int getRandomRareSkinID()
        {
            int id = (Random.Range(0, (DataCache.skinMap.Keys.Count - DataCache.skinNormalMap.Count)) + 1) + DataCache.skinNormalMap.Count;
            return id;
        }

    }


}

