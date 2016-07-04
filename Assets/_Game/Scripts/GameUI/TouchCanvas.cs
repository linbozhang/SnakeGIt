using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Game;
namespace Old
{
    //public class TouchCanvas :MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler,
    //    IBeginDragHandler ,IDragHandler,IEndDragHandler{


    //    private float m_DoubleClickInterval = 0.3f;
    //    private bool m_HasLastClick = false;
    //    private bool m_IsClickDown = false;
    //    private float m_LastClickTimer = 0;

    //    const float screenWidth = 1280.0f;
    //    const float screenHeight = 720.0f;

    //    public RectTransform joyStickCenter;
    //    public RectTransform joyStickPanel;
    //    public RectTransform btnAcc;
    //    public RectTransform arrow;

    //    public SnakeController player;

    //    public Vector2 JoyStickDesignPos = Vector2.zero;
    //    public Vector2 btnAccDesignPos = Vector2.zero;

    //    public SpriteRenderer bodyArrow;

    //    float scaleX = 1;
    //    float scaleY = 1;

    //    Vector2 joySticBasePos;
    //    Vector2 btnAccBasePos;

    //    Rect parentRect;
    //    public static TouchCanvas current;
    //    void Awake()
    //    {
    //        current = this;
    //    }

    //    void Start()
    //    {
    //        scaleX = Screen.width / screenWidth;
    //        scaleY = Screen.height / screenHeight;

    //        Vector2 baseOffset = JoyStickDesignPos - new Vector2(-screenWidth / 2, -screenHeight / 2);

    //        Vector2 base2Offset = btnAccDesignPos - new Vector2(-screenWidth / 2, -screenHeight / 2);

    //        parentRect = GetComponent<RectTransform>().rect;
    //        joySticBasePos = new Vector2(parentRect.x, parentRect.y) + baseOffset;
    //        joyStickCenter.anchoredPosition = joySticBasePos;
    //        joyStickPanel.anchoredPosition = joySticBasePos;

    //        btnAccBasePos = new Vector2(parentRect.x, parentRect.y) + base2Offset;
    //        btnAcc.anchoredPosition = btnAccBasePos;

    //        arrow.gameObject.SetActive(false);
    //        if(bodyArrow)
    //        {
    //            bodyArrow.gameObject.SetActive(false);
    //        }



    //    }

    //    void OnEnable()
    //    {
    //        if (PlayerSelf.Instance.controlMode == 1)
    //        {
    //            btnAcc.gameObject.SetActive(true);
    //            joyStickCenter.gameObject.SetActive(true);
    //            joyStickPanel.gameObject.SetActive(true);
    //            EventTriggerListener.Get(btnAcc.gameObject).onDown = OnAccClick;
    //            EventTriggerListener.Get(btnAcc.gameObject).onUp = OnCancleAcc;

    //        }
    //        else
    //        {
    //            btnAcc.gameObject.SetActive(false);
    //            joyStickCenter.gameObject.SetActive(false);
    //            joyStickPanel.gameObject.SetActive(false);
    //        }
    //    }

    //    public void OnAccClick(GameObject go)
    //    {
    //        GameController.current.SetPlayerAcc(true);
    //    }
    //    public void OnCancleAcc(GameObject go )
    //    {
    //        GameController.current.SetPlayerAcc(false);
    //    }

    //    public void OnDoubleClick(PointerEventData data)
    //    {
    //        if(GameController.current.player.isAcc)
    //        {
    //            GameController.current.SetPlayerAcc(false);
    //        }else
    //        {
    //            GameController.current.SetPlayerAcc(true);
    //        }
    //    }

    //    public void OnClickDown(PointerEventData data)
    //    {
    //        Vector2 pos=data.position;
    //        pos.x=pos.x / Screen.width * parentRect.width+parentRect.x;
    //        pos.y = pos.y / Screen.height * parentRect.height+parentRect.y;

    //        SetJoyStickPos(pos,true);
    //    }
    //    public void OnClickUp(PointerEventData data)
    //    {
    //        ResetJoyStickPos();
    //    }

    //    public void OnMove(PointerEventData data)
    //    {
    //        if(m_IsClickDown)
    //        {
    //            Vector2 pos = data.position;
    //            pos.x = pos.x / Screen.width * parentRect.width + parentRect.x;
    //            pos.y = pos.y / Screen.height * parentRect.height + parentRect.y;
    //            SetJoyStickPos(pos,false);
    //        }

    //    }

    //    void SetJoyStickPos(Vector2 pos,bool isFirst)
    //    {
    //        if (PlayerSelf.Instance.controlMode == 1)
    //        {
    //            if (!bodyArrow.gameObject.activeInHierarchy)
    //            {
    //                bodyArrow.gameObject.SetActive(true);
    //            }
    //        }

    //        if (isFirst)
    //        {
    //            if(PlayerSelf.Instance.controlMode==2)
    //            {
    //                pos = Vector2.zero;
    //                joyStickCenter.anchoredPosition = pos;
    //                joyStickPanel.anchoredPosition = pos;
    //            }else
    //            {

    //                joyStickCenter.anchoredPosition = pos;

    //                if (player != null)
    //                {
    //                    Vector2 dir = player.localDirection;

    //                    joyStickPanel.anchoredPosition = pos - (dir.normalized * 70);
    //                }
    //                else
    //                {
    //                    joyStickPanel.anchoredPosition = pos;
    //                }
    //            }




    //        }else
    //        {
    //            Vector2 parentPos = joyStickPanel.anchoredPosition;
    //            Vector2 offset = pos - parentPos;
    //            float distance = offset.magnitude;

    //            if(bodyArrow)
    //            {
    //                float arrowDis = distance / 70.0f * 2f;
    //                if (arrowDis > 10)
    //                {
    //                    arrowDis = 10;
    //                }
    //                bodyArrow.color = new Color(1, 1, 1, ((11 - arrowDis) / 11.0f));
    //                bodyArrow.transform.localPosition = new Vector3(arrowDis, 0, 0);
    //            }

    //            //arrow.anchoredPosition = new Vector2(arrowDis, arrow.anchoredPosition.y);

    //            //if (player != null)
    //            //{
    //            //    arrow.parent.localEulerAngles = player.transform.localEulerAngles;
    //            //}
    //            Vector2 direction = offset.normalized;
    //            GameController.current.SetPlayerDirection(direction);
    //            if (distance>=70)
    //            {
    //                offset = direction * 70;
    //            }
    //            joyStickCenter.anchoredPosition = parentPos + offset;
    //        }
    //    }

    //    void ResetJoyStickPos()
    //    {
    //        joyStickCenter.anchoredPosition = joySticBasePos;
    //        joyStickPanel.anchoredPosition = joySticBasePos;
    //        if(bodyArrow)
    //        {
    //            bodyArrow.gameObject.SetActive(false);
    //        }

    //    }


    //    public void OnBeginDrag(PointerEventData data)
    //    {
    //        //OnClickDown(data);
    //    }
    //    public void OnEndDrag(PointerEventData data)
    //    {

    //    }

    //    public void OnDrag(PointerEventData data)
    //    {
    //        OnMove(data);
    //    }




    //    public void OnPointerClick(PointerEventData data)
    //    {

    //    }

    //    public void OnPointerEnter(PointerEventData data)
    //    {
    //        //Debug.Log("OnPointerEnter");

    //    }
    //    public void OnPointerDown(PointerEventData data)
    //    {
    //        //Debug.Log("OnPointerDown");
    //        if(this.m_HasLastClick)
    //        {
    //            OnDoubleClick(data);
    //        }
    //        OnClickDown(data);
    //        this.m_HasLastClick = true;
    //        this.m_IsClickDown = true;
    //        this.m_LastClickTimer = Time.realtimeSinceStartup;
    //    }
    //    public void OnPointerUp(PointerEventData data)
    //    {
    //        //Debug.Log("OnPointUp");
    //        m_IsClickDown = false;
    //        OnClickUp(data);
    //    }
    //    public void OnPointerExit(PointerEventData data)
    //    {
    //        //Debug.Log("OnPointerExit");
    //        ResetJoyStickPos();
    //    }


    //	// Update is called once per frame
    //	void Update () {
    //	    if(m_HasLastClick)
    //        {
    //            if(Time.realtimeSinceStartup-m_LastClickTimer>m_DoubleClickInterval)
    //            {
    //                this.m_HasLastClick = false;
    //                this.m_LastClickTimer = 0;
    //            }
    //        }
    //	}
    //}
}

