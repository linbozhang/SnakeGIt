using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace SnakeOffline
{
    public class SnakeController : MonoBehaviour
    {


        private Vector2 _direction = Vector2.right;
        [HideInInspector]
        public List<GameObject> bodyList;
        private Path currentPath;
        [HideInInspector]
        public List<GameObject> eatableList;
        public GameObject head;
        public bool isPlayer = false;
        private Vector2 lastPosition = Vector2.zero;
        public int length;
        [HideInInspector]
        public bool onEject;

        private bool onScreen = true;
        [HideInInspector]
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
                tempBody.GetComponent<SnakeBody>().InitBody(bodyList.Count);
                this.bodyList.Add(tempBody);
                this.currentPath.addPointToHead(tempBody.transform.position);
            }
            


        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Vector2 vector = this._direction + (this.tempDirection * Time.deltaTime * 10);
            //this._direction = vector.normalized;
            this.size = 1f + (this.length / 1000f);
            transform.position +=  (Vector3) this.direction*Time.deltaTime * 4;

            float angle = Mathf.Atan2(this.direction.y, this.direction.x);
            this.transform.localEulerAngles = new Vector3(0, 0, angle / Mathf.PI * 180);

            this.bodyList[0].transform.position = transform.position;
            this.lastPosition = this.bodyList[0].transform.position;
            this.currentPath.addPointToHead(this.bodyList[0].transform.position);
            this.repositionBodyParts();

        }

        void repositionBodyParts()
        {
            float interval = GameConfig.SnakeBodyInterval*this.size;
            PathPoint toDelete = this.getHead();
            Vector2 zero = Vector2.zero;
            int count = this.bodyList.Count;
            bool flag = false;
            float scale = size * GameConfig.SnakeBaseScale;
            for (int i=0;i<count;i++)
            {
                
                this.bodyList[i].transform.localScale = new Vector3(scale, scale, 1);
                bool flag2 = false;
                if(i==0)
                {
                    //this.bodyList[i].transform.position += (Vector3)this._direction * Time.deltaTime * 3;//.Translate(this._direction*Time.deltaTime*3);

                }else
                {
                    float num4 = interval;
                    Vector2 vector2 = toDelete.position - zero;
                    while(vector2.sqrMagnitude<(num4*num4))
                    {
                        flag2 = true;
                        num4 -= vector2.magnitude;
                        this.bodyList[i].transform.position = toDelete.position;
                        if(toDelete.nextPoint!=null)
                        {
                            toDelete = toDelete.nextPoint;
                            vector2 = toDelete.position - (Vector2)this.bodyList[i].transform.position;
                        }else
                        {
                            flag = true;
                            break;
                        }
                    }
                    if(!flag)
                    {
                        if(flag2)
                        {
                            this.bodyList[i].transform.position += (Vector3)(vector2.normalized) * num4;
                        }else
                        {
                            this.bodyList[i].transform.position = zero + vector2.normalized * num4;
                        }
                    }else
                    {
                        this.bodyList[i].transform.position = zero;
                    }

                }
                if(i==(count-1))
                {
                    this.currentPath.deletePoint(toDelete);
                }
                if(i!=0)
                {
                    Vector2 tempDir = bodyList[i - 1].transform.position - bodyList[i].transform.position;
                    float angle = Mathf.Atan2(tempDir.y, tempDir.x);
                     bodyList[i].transform.localEulerAngles = new Vector3(0, 0, angle / Mathf.PI * 180);
                }else
                {
                    bodyList[0].transform.localEulerAngles = transform.localEulerAngles;
                }
               

                zero = this.bodyList[i].transform.position;
            }

        }
        public PathPoint getHead()
        {
            return this.currentPath.head;
        }

        public Vector2 direction
        {
            get
            {
                return this._direction;
            }
            set
            {
                this.tempDirection = value.normalized;
            }
        }
        Vector2 getRandomSnakePos()
        {
            return (Random.insideUnitCircle.normalized * GameConfig.MapRadius) + (Vector2.one * GameConfig.MapRadius);
        }
    }
}

