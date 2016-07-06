using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace SnakeOffline
{
    public class SnakeController : MonoBehaviour
    {


        private Vector2 _direction = Vector2.right;
        public GameObject body;
        public List<GameObject> bodyList;
        public GameObject bodyPart;
        private Path currentPath;
        public List<GameObject> eatableList;
        public GameObject head;
        public bool isPlayer = true;
        private Vector2 lastPosition = Vector2.zero;
        public int length;
        public bool onEject;
        private bool onScreen = true;
        public Color playerColor;
        public byte playerID = 1;
        public string playerName;
        private float size = 1f;
        private Vector2 tempDirection = Vector2.right;
        private Vector2 velocity = Vector2.zero;



        void Awake()
        {
            this.currentPath = base.GetComponent<Path>();
        }


        // Use this for initialization
        void Start()
        {
            Vector2 vector;
            this.currentPath.player = this;
            this.tempDirection = this._direction = Random.insideUnitCircle.normalized;
            this.playerID = PoolManager.current.GetPlayerID();
            vector = getRandomSnakePos();
            this.transform.position = vector;
            this.length = GameConfig.snakeInitLength;
            if (this.isPlayer)
            {
                CameraController.current.followObject = this;
            }else
            {
                this.length = GameConfig.snakeInitLength*Random.Range(1,5);    
            }
            int bodyCount = this.length / 10;
            for(int i=0;i<bodyCount;i++)
            {
                GameObject tempBody = PoolManager.current.spawnBodyPart(vector);
                this.bodyList.Add(tempBody);
                this.currentPath.addPointToHead(tempBody.transform.position);
            }
            


        }

        // Update is called once per frame
        void Update()
        {

        }

    

        Vector2 getRandomSnakePos()
        {
            return (Random.insideUnitCircle.normalized * GameConfig.MapRadius) + (Vector2.one * GameConfig.MapRadius);
        }
    }
}

