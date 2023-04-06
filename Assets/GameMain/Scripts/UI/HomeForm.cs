using StarForce;
using SuperScrollView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeForm : UGuiForm
{
    private Button BtnChuangzuo;
    LoopListView2 mLoopListView;

    public RectTransform  mViewPortRectTransform;
    int maxCount = 20;

    QToggleButton[] qToggleButtons = new QToggleButton[(int)HomeListComponent.Max];
    IComponentBase[] componentBases = new IComponentBase[(int)HomeListComponent.Max];
    HomeListComponent LastComponent = HomeListComponent.Max;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        BottomMenu bottomMenu = transform.Find("BottomMenu").gameObject.AddComponent<BottomMenu>();
        bottomMenu.UIFormId = UIFormId.HomeForm;
        BtnChuangzuo = transform.Find("ChuangZuo/BtnChuangzuo").GetComponent<Button>();
        mLoopListView = this.transform.Find("CenterScreen/Component_0/ScrollView_TopToBottom").GetComponent<LoopListView2>();
        mLoopListView.InitListView(maxCount, OnGetItemByIndex);

        for (int i = 0; i < (int)HomeListComponent.Max; i++)
        {
            HomeListComponent currentCpt = (HomeListComponent)i;
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

        BtnChuangzuo.onClick.AddListener(OnChuangzuoClick);
    }

    public void OnToggleClick(HomeListComponent listComponent)
    {
        ShowListComponent(listComponent);
    }

    public void ShowListComponent(HomeListComponent listComponent)
    {
        for (int i = 0; i < (int)HomeListComponent.Max; i++)
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
        LastComponent =listComponent;
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);

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

    public void OnChuangzuoClick()
    {
        GameEntry.UI.OpenUIForm(UIFormId.CreatePreviewForm);
    }

    public void OnBoutiqueClick(bool isOn)
    {
        if (isOn)
        {
            ShowListComponent(HomeListComponent.BoutiqueList);
        }
    }


    public void OnNearByClick(bool isOn)
    {
        if (isOn)
        {
            ShowListComponent(HomeListComponent.NearByCpt);
        }
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
        item = listView.NewListViewItem("ItemPrefab1");

        //if (itemData.mPersonId == 0)
        //{
        //    item = listView.NewListViewItem("ItemPrefab1");
        //}
        //else
        //{
        //    item = listView.NewListViewItem("ItemPrefab2");
        //}
        IItemBase itemScript = item.GetComponent<IItemBase>();
        if (itemScript == null)
        {
            itemScript = item.gameObject.AddComponent<HomeItem>();
        }

        if (item.IsInitHandlerCalled == false)
        {
            item.IsInitHandlerCalled = true;
            itemScript.Init();
        }
        itemScript.SetItemData(null, index);
        return item;
    }
    //void OnJumpBtnClicked()
    //{
    //    int itemIndex = 0;
    //    if (int.TryParse(mScrollToInput.text, out itemIndex) == false)
    //    {
    //        return;
    //    }
    //    if (itemIndex < 0)
    //    {
    //        return;
    //    }
    //    mLoopListView.MovePanelToItemIndex(itemIndex, 0);
    //}
}
