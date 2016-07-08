using UnityEngine;
using System.Collections;
namespace SnakeOffline
{
    public enum FoodType
    {
        Low = 1,
        Middle = 2,
        High = 3,
    }

    public class FoodsController : MonoBehaviour
    {

        const float foodRectSize = 1f;
        const float halfFoddRectSize = 0.5f;
        const int foodC = 1000;
        GameObject[,] foods = new GameObject[foodC, foodC];
        ushort[,] foodsCount = new ushort[areaCount, areaCount];
        const int areaCount = 5;
        const int areaSize = 200;
        const int foodCountInArea = 50;




        public void InitFoods()
        {

            for (int i = 0; i < areaCount; i++)
            {
                for (int j = 0; j < areaCount; j++)
                {
                    int basex = i * areaSize;
                    int basey = j * areaSize;
                    for (int k = 0; k < foodCountInArea; k++)
                    {
                        int x = basex + Random.Range(0, areaSize);
                        int y = basey + Random.Range(0, areaSize);

                        Vector2 pos = new Vector2(x, y) * foodRectSize + (Vector2.one * halfFoddRectSize);
                                                
                        GameObject food = PoolManager.current.spawnFoodBody(pos);
                        food.GetComponent<FoodBody>().InitFoodBody((FoodType)getRandomFoodType());
                        foods[x, y] = food;

                    }
                }
            }
        }

        public void ResetFood()
        {
            for(int i=0;i<foods.GetLength(0);i++)
            {
                for(int j=0;j<foods.GetLength(1);j++)
                {
                    foods[i, j] = null;
                }
            }
            for (int i = 0; i < foodsCount.GetLength(0); i++)
            {
                for (int j = 0; j < foodsCount.GetLength(1); j++)
                {
                    foodsCount[i, j] = 0;
                }
            }
        }

        // Use this for initialization
        void Start()
        {
            //InitFoods();
        }


        // Update is called once per frame
        void Update()
        {

        }



        public byte getRandomFoodType()
        {
            return (byte)Random.Range(1, 4);
        }
    }
}

