using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private Text textToolTip;
    private Text textContent;
    private Image imgBG;
    //private float canvasAlpha = 0;
    private CanvasGroup canvas;
    private int smooth = 10;  //隐藏速度
    private int target = 0;//初始值为0，隐藏提示

    private void Start()
    {
        //因为是private，不能直接拖拽，通过这种方式获取
        textToolTip = GetComponent<Text>();
        //canvasAlpha = GetComponent<CanvasGroup>().alpha;
        canvas = GetComponent<CanvasGroup>();

        //为什么这个不对
        //textContent = transform.GetComponentInChildren<Text>();
        textContent = transform.Find("Content").GetComponent<Text>();
        imgBG = transform.GetComponentInChildren<Image>();
        //gameObject.SetActive(false);
        target = 0;
    }


    public void Show(string text)
    {
        //也可以使用setActive方法
        gameObject.SetActive(true);
        textToolTip.text = text;
        textContent.text = text;
        target = 1;
    }

    public void Hide()
    {
        target = 0;
    }

    /// <summary>
    /// 设置位置
    /// </summary>
    /// <param name="position"></param>
    public void SetToolTipPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    void Update()
     {
        if (target != canvas.alpha)
        {
            canvas.alpha = Mathf.Lerp(canvas.alpha, target, smooth * Time.deltaTime);
            if (Mathf.Abs(target - canvas.alpha) < 0.1)
            {
                canvas.alpha = target;
            }
        }
     }

}
