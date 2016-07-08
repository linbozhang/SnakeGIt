using UnityEngine;
using System.Collections;
namespace SnakeOffline
{
    public class FoodBody : MonoBehaviour
    {
        public SpriteRenderer mPoint;



        public void InitFoodBody( FoodType type )
        {

            mPoint.color = ColorPool.current.getRandomFoodColor();

            float scale = 0.2f;
            switch(type)
            {
                case FoodType.Low:
                    scale = 0.2f;
                    break;
                case FoodType.Middle:
                    scale = 0.3f;
                    break;
                case FoodType.High:
                    scale = 0.4f;
                    break;
            }

            mPoint.transform.localScale = Vector3.one * scale;

        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

