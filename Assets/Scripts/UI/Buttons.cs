using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Buttons : MonoBehaviour
{
    public UnityEvent onClick;
    protected SpriteRenderer spriteRenderer;
    protected BoxCollider2D boxCollider2D;
    protected Bounds bounds;
    protected bool isButtonEnabled = true;
    public float delay = 0.1f;//延时
    public bool actived = true; //按钮是否激活，没激活按钮就没有反应
    Bounds GetBounds(BoxCollider2D collider)
    {
        Bounds bounds = collider.bounds;
        return bounds;
    }
    void Start()
    {
        actived = true;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        if (boxCollider2D == null)
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        if (boxCollider2D == null)
        {
            Debug.LogError("BoxCollider2D is not assigned or found.");
        }

        NextStart();



    }
    /// <summary>
    /// 开启按钮激活时的特效。
    /// </summary>
    public virtual void LightOn()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.65f);
    }


    /// <summary>
    /// 关闭按钮激活时的特效。
    /// </summary>
    public virtual void LightOff()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
    /// <summary>
    /// 交给子类重写的方法，如果为false，则鼠标移动至按钮时按钮无响应。
    /// </summary>
    /// <returns>
    /// 如果为false，则鼠标移动至按钮时不会有反应。
    /// </returns>
    public virtual bool CheckMouseAllowed()
    {
        return true;
    }
    public virtual void Update()
    {
        if (actived == false) return;
        if (TouchMouse())
        {
            if (!CheckMouseAllowed())
            {
                return;
            }
            LightOn();
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("点击到了");
                if (isButtonEnabled)
                {
                    onClick.Invoke();
                    StartCoroutine(HandleButtonClick());
                }

            }

        }
        else
        {
            LightOff();
        }

    }

    /// <summary>
    /// 判断鼠标是否移动至按钮上，通过boxCollider2D组件的collider检测。
    /// 如果按钮上面有其他角色，那么无法点击。
    /// 注：由于摄像机在负轴，所以z越小越高。
    /// </summary>
    /// <returns></returns>
    bool TouchMouse()
    {
        Vector3 mousePos = MousePosition.GetMousePosition();
        bounds = GetBounds(boxCollider2D);
        if (mousePos.x > bounds.min.x && mousePos.x < bounds.max.x & mousePos.y > bounds.min.y && mousePos.y < bounds.max.y)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(bounds.center, bounds.size, 0f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("UI") && collider.gameObject.transform.position.z < this.transform.position.z)
                {
                    return false; // 角色在按钮上面
                }
            }
            return true;
        }
        else
        {
            return false;
        }

    }
    /// <summary>
    /// 协程，用于给按钮增加延时。
    /// </summary>
    /// <returns></returns>
    IEnumerator HandleButtonClick()
    {
        isButtonEnabled = false;
        yield return new WaitForSeconds(delay);
        isButtonEnabled = true;
    }
    /// <summary>
    /// 方便子类按钮新增自己的初始化。
    /// </summary>
    public virtual void NextStart()
    {
    }
}
