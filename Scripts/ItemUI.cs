using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    #region Data
    public Item Item { get; set; }
    public int Amount { get; set; }
    #endregion

    #region UI Component
    private Image itemImage;
    private Text textAmount;
    

    public Image ItemImage
    {
        get
        {
            if(itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }
            return itemImage;
        }
    }

    private Text TextAmount
    {
        get
        {
            if(textAmount == null)
            {
                textAmount = GetComponentInChildren<Text>();
            }
            return textAmount;
        }
    }
    #endregion


    private int smoothing = 4;
    private Vector3 targetScale = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 animationScale = new Vector3(1.4f, 1.4f, 1.4f);


    private void Update()
    {
        if (transform.localScale.x != targetScale.x)
        {
            float x = Mathf.Lerp(transform.localScale.x, targetScale.x, smoothing * Time.deltaTime);
            transform.localScale= new Vector3(x, x, x);
            //以防一直达不到目标值
            if(Mathf.Abs(transform.localScale.x - targetScale.x) < 0.02f)
            {
                transform.localScale = targetScale;
            }
        }
    }

    public void SetItem(Item item, int amount = 1)
    {
        this.Item = item;
        this.Amount = amount;

        //ItemImage.sprite = item.sprite; 我的天，就不能这么写，记住下面
        //大小写也分清楚
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        TextAmount.text = Amount.ToString();//amount.ToString()可以吗
        transform.localScale = animationScale; // 放大效果
    }

    /// <summary>
    /// 增加默认为1个
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeAmount(int amount = 1)
    {
        //Amount++;
        this.Amount += amount;
        TextAmount.text = Amount.ToString();
        transform.localScale = animationScale; // 放大效果
    }


    public void ReduceAmount(int amount = 1)
    {
        this.Amount -= amount;
        
       if (Item.Capacity > 1)
        {
            TextAmount.text = Amount.ToString();
        }
        else
        {
            TextAmount.text = "";
        }
    
        transform.localScale = animationScale; // 放大效果
    }

    /// <summary>
    /// 设置位置
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Vector3 position)
    {
        this.transform.localPosition = position;
    }

    public void ShowOrHide(bool showOrHide)
    {
        //不是 this.SetActive
        gameObject.SetActive(showOrHide);
    }


}
