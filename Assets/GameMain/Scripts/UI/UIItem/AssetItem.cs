using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetItem : MonoBehaviour, IItemBase
{
    Text AssetName;
    Image SelectImg;
    Image IconImg;
    Button bgBtn;
    public void Init()
    {
        SelectImg = transform.Find("sel").GetComponent<Image>();
        IconImg = transform.Find("icon").GetComponent<Image>();
        bgBtn = transform.Find("bg").GetComponent<Button>();
        bgBtn.onClick.AddListener(OnItemClick);
    }

    public void SetItemData(HomeData itemData, int itemIndex)
    {

    }

    public void OnItemClick()
    {

    }

}
