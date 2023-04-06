using StarForce;
using SuperScrollView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineForm : UGuiForm
{
    LoopListView2 mLoopListView;
    MineListComponent LastComponent = MineListComponent.Max;
    int maxCount = 10;

    QToggleButton[] qToggleButtons = new QToggleButton[(int)MineListComponent.Max];
    IComponentBase[] componentBases = new IComponentBase[(int)MineListComponent.Max];

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        BottomMenu bottomMenu = transform.Find("BottomMenu").gameObject.AddComponent<BottomMenu>();
        bottomMenu.UIFormId = UIFormId.MineForm;
        mLoopListView = this.transform.Find("CenterScreen/Component_0/ScrollView_TopToBottom").GetComponent<LoopListView2>();
        mLoopListView.InitListView(maxCount, OnGetItemByIndex);
        for (int i = 0; i < (int)MineListComponent.Max; i++)
        {
            MineListComponent currentCpt = (MineListComponent)i;
            qToggleButtons[i] = this.transform.Find("CenterButtonList/Btn_" + i.ToString()).GetComponent<QToggleButton>();
            qToggleButtons[i].onClick.AddListener((isOn) => {
                if (isOn)
                {
                    OnToggleClick(currentCpt);
                }
            });
            componentBases[i] = this.transform.Find("CenterScreen/Component_" + i.ToString()).gameObject.AddComponent<IComponentBase>();
            componentBases[i].OnInit();
        }
        qToggleButtons[0].Press();
    }

    public void OnToggleClick(MineListComponent listComponent)
    {
        ShowListComponent(listComponent);
    }

    public void ShowListComponent(MineListComponent listComponent)
    {
        for (int i = 0; i < (int)MineListComponent.Max; i++)
        {
            if ((int)listComponent == i)
            {
                if (!componentBases[i].gameObject.activeInHierarchy)
                {
                    componentBases[i].gameObject.SetActive(true);
                    componentBases[i].OnResume();
                }

            }
            else
            {
                if (componentBases[i].gameObject.activeInHierarchy)
                {
                    componentBases[i].gameObject.SetActive(false);
                    componentBases[i].OnPause();
                }
            }

        }
        LastComponent = listComponent;
    }


    LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        if (index < 0 || index >= maxCount)
        {
            return null;
        }

        HomeData itemData = new HomeData();//ChatMsgDataSourceMgr.Get.GetChatMsgByIndex(index);
        if (itemData == null)
        {
            return null;
        }
        LoopListViewItem2 item = null;
        item = listView.NewListViewItem("CreationItem");

        //if (itemData.mPersonId == 0)
        //{
        //    item = listView.NewListViewItem("ItemPrefab1");
        //}
        //else
        //{
        //    item = listView.NewListViewItem("ItemPrefab2");
        //}
        IItemBase itemScript = item.GetComponent<IItemBase>();
        if (item.IsInitHandlerCalled == false)
        {
            item.IsInitHandlerCalled = true;
            itemScript.Init();
        }
        itemScript.SetItemData(null, index);
        return item;
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }
    protected override void OnPause()
    {
        base.OnPause();
    }

    protected override void OnResume()
    {
        base.OnResume();

    }
}
