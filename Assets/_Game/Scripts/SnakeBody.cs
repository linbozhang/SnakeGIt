using UnityEngine;
using System.Collections;
namespace SnakeOffline
{
    public class SnakeBody : MonoBehaviour
    {


        public SpriteRenderer body;
        public SpriteRenderer cover;
        public SpriteRenderer bgSp;
        public SpriteRenderer bgFlowSp;
        public SpriteRenderer bodyCover;
        public SpriteRenderer flowSp;

        public SpriteRenderer skinCover;
        public SpriteRenderer decorate;
        public SpriteRenderer arrow;

        int mDepth;

        int mBodyDepth;
        int mCoverDepth;
        int mShadowDepth;
        int mShadowFlowDepth;
        int mBodyCoverDepth;
        int mFlowDepth;
        int mSkinDepth;

        int mDecorateDepth;

        
        void Awake()
        {
            //Debug.Log("snakeBodyStart");
            mBodyDepth = body.sortingOrder;
            mBodyCoverDepth = bodyCover.sortingOrder;
            mShadowDepth = bgSp.sortingOrder;
            mShadowFlowDepth = bgFlowSp.sortingOrder;
            mCoverDepth = cover.sortingOrder;
            mFlowDepth = flowSp.sortingOrder;
            if (skinCover)
            {
                mSkinDepth = skinCover.sortingOrder;
            }
        }

        // Use this for initialization
        void Start()
        {
            
            
        }

        //public void InitBody(int depth)
        //{
        //    mDepth = depth;
        //    SetOrder();
        //}

        void SetOrder()
        {
            //Debug.Log("setOrder");
            bodyCover.sortingOrder = mBodyCoverDepth - mDepth * 4;
            body.sortingOrder = mBodyDepth - mDepth * 4;
            bgSp.sortingOrder = mShadowDepth - mDepth * 4;
            bgFlowSp.sortingOrder = mShadowFlowDepth - mDepth * 4;
            cover.sortingOrder = mCoverDepth - mDepth * 4;
            flowSp.sortingOrder = mFlowDepth - mDepth * 4;
            if(skinCover)
            {
                skinCover.sortingOrder = mSkinDepth - mDepth * 4;
            }
           

        }

       


        public TextMesh nameText;

        public bool isSpecial = false;

        public int depth;
        public bool isHead;

        public float serverSidePreAngle = 0;
        public float clientSidePreAngle = 0;


        Vector3 direction;
        public Color bodyColor;
        Color coverColor;
        public bool visible = true;
        public bool isSampleBody = false;
        public void Init(int depth, Vector3 direction, Color color, string name, bool _isSampleBody = false, bool isLocal = false)
        {
            
            this.depth = depth;
            this.direction = direction;
            this.bodyColor = color;
            this.bodyColor.a = 1;
            this.coverColor = Color.black;
            this.coverColor.a = color.a;
            //if (depth == 0)
            //{
            //    isHead = true;
            //    nameText.text = name;
            //    if (arrow != null && isLocal)
            //    {
            //        arrow.enabled = true;
            //        arrow.color = new Color(1, 1, 1, 0.8f);
            //        //TouchCanvas.current.bodyArrow = arrow;
            //    }
            //    _isSampleBody = false;
            //}
            this.isSampleBody = _isSampleBody;
            body.transform.localEulerAngles = new Vector3(0, 0, 0);

            body.color = bodyColor;
            bgSp.color = Color.black;
            cover.color = coverColor;
            bgFlowSp.enabled = false;
            flowSp.enabled = false;
            this.mDepth = depth;
            SetOrder();
        }



        float preEffectAlpha = 0;
        float preBGEffectAlpha = 0;
        bool isFirstAcc = true;




        public void Appear()
        {
            body.enabled = true;
            SetVis(gameObject, true);
        }

        public void Disappear()
        {
            visible = false;
            SetVis(gameObject, false);
        }

        void SetVis(GameObject go, bool vis)
        {
            foreach (var r in go.GetComponents<Renderer>())
            {
                r.enabled = vis;
            }
            for (int i = 0; i < go.transform.childCount; i++)
            {
                var t = go.transform.GetChild(i);
                SetVis(t.gameObject, vis);
            }
        }

    }
}

