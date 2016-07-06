using UnityEngine;
using System.Collections;
namespace SnakeOffline
{
    public class SnakeBody : MonoBehaviour
    {
        public SpriteRenderer mBody;
        public SpriteRenderer mBodyCover;
        public SpriteRenderer mShadow;

        int mDepth;

        int mBodyDepth;
        int mBodyCoverDepth;
        int mShadowDepth;

        // Use this for initialization
        void Start()
        {
            mBodyDepth = mBody.sortingOrder;
            mBodyCoverDepth = mBodyCover.sortingOrder;
            mShadowDepth = mShadow.sortingOrder;
        }

        public void InitBody(int depth)
        {
            mDepth = depth;
            SetOrder();
        }

        void SetOrder()
        {
            mBodyCover.sortingOrder = mBodyCoverDepth - mDepth * 4;
            mBody.sortingOrder = mBodyDepth - mDepth * 4;
            mShadow.sortingOrder = mShadowDepth - mDepth * 4;
        }
    }
}

