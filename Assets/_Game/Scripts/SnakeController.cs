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

        private SnakeSkin skinData;
        private Color[] snakeBodyColors=new Color[4];
        private string[] snakeBodyPictures=new string[3];

        void Awake()
        {
            this.currentPath = base.GetComponent<Path>();
        }


        public void InitSnake(int skinId,Vector2 _dir,byte _playerId,int _length,bool _isPlayer)
        {
            Vector2 vector;
            this.currentPath.player = this;
            this.tempDirection = this._direction =  _dir;// Random.insideUnitCircle.normalized;
            this.playerID = _playerId;
            this.isPlayer = _isPlayer;
            vector = this.transform.position;
            this.length = _length;
            skinData = DataCache.skinMap[skinId];
            string scolor = "17";
            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        scolor = skinData.color_one;
                        break;
                    case 1:
                        scolor = skinData.color_two;
                        break;
                    case 2:
                        scolor = skinData.color_three;
                        break;
                }
                if (string.IsNullOrEmpty(scolor))
                {
                    snakeBodyColors[i] = Color.white;
                }
                else
                {
                    snakeBodyColors[i] = ColorPool.current.getBodyColorWithIndex(System.Int32.Parse(scolor));
                }
            }
            scolor = skinData.first_color;
            snakeBodyColors[3] = ColorPool.current.getBodyColorWithIndex(System.Int32.Parse(scolor));


            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        snakeBodyPictures[0] = skinData.picture_one;
                        break;
                    case 1:
                        snakeBodyPictures[1] = skinData.picture_two;
                        break;
                    case 2:
                        snakeBodyPictures[2] = skinData.picture_three;
                        break;
                }
            }

            if (this.isPlayer)
            {
                CameraController.current.followObject = this;
            }
           




            int bodyCount = this.length / 10;
            for (int i = 0; i < bodyCount; i++)
            {
                GameObject tempBody = ClientAddSnakeBody(i,vector);   //PoolManager.current.spawnBodyPart(vector);
                //tempBody.GetComponent<SnakeBody>().InitBody(bodyList.Count);
                this.bodyList.Add(tempBody);
                this.currentPath.addPointToHead(tempBody.transform.position);
            }
            if(!isPlayer)
            {
                gameObject.AddComponent<SnakeAIController>();
            }
        }

        // Use this for initialization
        void Start()
        {
          
        }

        // Update is called once per frame
        long count = 0;
        void Update()
        {


            //float realDeltaAngle = Quaternion.FromToRotation (this._direction,this.tempDirection).eulerAngles.z;
            //float deltaAngle = realDeltaAngle > 180 ? (360 - realDeltaAngle) : realDeltaAngle;
            //float rotateSpeed = 1.8f;
            //if (deltaAngle > Time.fixedDeltaTime*180*rotateSpeed) {
            //	float tempAngle = Mathf.Atan2 (this._direction.y, this._direction.x);
            //	tempAngle = tempAngle + ((realDeltaAngle > 180 ? -1 : 1) * Time.fixedDeltaTime * Mathf.PI*rotateSpeed);
            //	//Debug.Log ("z"+realDeltaAngle+"a"+deltaAngle+"t"+ tempAngle);
            //	this._direction = new Vector2 (Mathf.Cos (tempAngle), Mathf.Sin (tempAngle));
            //	//this._direction = (this._direction + (this.tempDirection * Time.deltaTime * 10)).normalized;	
            //} else {
            //	this._direction = this.tempDirection.normalized;
            //}

            count++;
            if(count%50==0)
            {
                Debug.Log(Time.realtimeSinceStartup);
            }
           
            Vector2 vector = this._direction + (this.tempDirection * Time.deltaTime * 10);
            this._direction = vector.normalized;
            this.size = 1f + (this.length / 1000f);
            transform.position +=  (Vector3) this.direction*Time.deltaTime * 50;

            float angle = Mathf.Atan2(this.direction.y, this.direction.x);
            this.transform.localEulerAngles = new Vector3(0, 0, angle / Mathf.PI * 180);

            this.bodyList[0].transform.position = transform.position;
            this.lastPosition = this.bodyList[0].transform.position;
            this.currentPath.addPointToHead(this.bodyList[0].transform.position);
            this.repositionBodyParts();
            if (count % 50 == 0)
            {
                Debug.Log(Time.realtimeSinceStartup);
            }
        }
        int aCount = 0;
        void repositionBodyParts()
        {
            aCount = 0;
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
                        aCount++;
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

                //Vector2 viewPos = CameraController.current.mCamera.WorldToViewportPoint(bodyList[i].transform.position);
                var snakeBody = bodyList[i].GetComponent<SnakeBody>();
                //if (viewPos.x < -0.1f || viewPos.x > 1.1f || viewPos.y < -0.1f || viewPos.y > 1.1f)
                //{
                //    snakeBody.Disappear();
                //}
                //else
                //{
                //    snakeBody.Appear();
                //}
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
			return new Vector2 (Random.Range(12.8f,GameConfig.MapRadius-12.8f),Random.Range(12.8f,GameConfig.MapRadius-12.8f) );
        }


        GameObject ClientAddSnakeBody(int depth, Vector2 pos)
        {
            GameObject prefab;

            //GameObject tempheadPrefab = headPrefab;
            //GameObject tempbodyPrefab = bodyPrefab;


            bool isSampleBody = true;
            if (!string.IsNullOrEmpty(skinData.picture_Body))
            {
                isSampleBody = false;
               // tempbodyPrefab = DataCache.loadPrefab("Skin/" + skinData.picture_Body);
            }
            


            Color color = Color.white;
            int index = depth % skinData.loopType.Length;
            int bodyIndex = skinData.loopType[index];

            if (!string.IsNullOrEmpty(snakeBodyPictures[bodyIndex - 1]))
            {
                isSampleBody = false;
              //  tempbodyPrefab = DataCache.loadPrefab("Skin/" + snakeBodyPictures[bodyIndex - 1]);
            }
            else
            {
                color = snakeBodyColors[bodyIndex - 1];
            }

            GameObject snake = PoolManager.current.spawnBodyPart(pos);

            //snake.transform.SetParent(snakeBodyHandler.transform);
            //SetOrder(snake, depth);
            float a = (depth % (skinData.shadow_interval * 2) * 1.0f / skinData.shadow_interval);

            if (a > 1)
            {
                a = 2 - a;
            }
            a *= 0.8f;
            color.a = a;

            snake.transform.localScale = new Vector3(size, size, 1);
            snake.GetComponent<SnakeBody>().Init(depth, Vector2.zero, color, "", isSampleBody, isPlayer);
            return snake.gameObject;
        }

    }
}

