using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarForce;
using SuperScrollView;
using UnityEngine.UI;


public class AssetSelectMenu : MonoBehaviour
{
    LoopListView2 mLoopListView;
    int maxCount = 10;
    private Button OnCloseBtn;

    private void Awake()
    {
        mLoopListView = this.transform.Find("SelectArea/ScrollView_Asset").GetComponent<LoopListView2>();
        OnCloseBtn = transform.Find("CloseBtn").GetComponent<Button>();
        mLoopListView.InitListView(maxCount, OnGetItemByIndex);

        OnCloseBtn.onClick.AddListener(() => {
            this.transform.parent.gameObject.SetActive(false);
        });
    }


    LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        if (index < 0 || index >= maxCount)
        {
            return null;
        }

        HomeData itemData = new HomeData();
        if (itemData == null)
        {
            return null;
        }
        LoopListViewItem2 item = null;
        item = listView.NewListViewItem("AssetItem");

        IItemBase itemScript = item.GetComponent<IItemBase>();
        if (item.IsInitHandlerCalled == false)
        {
            item.IsInitHandlerCalled = true;
            itemScript.Init();
        }
        itemScript.SetItemData(null, index);
        return item;
    }

}
