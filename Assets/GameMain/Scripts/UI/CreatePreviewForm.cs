using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarForce;
using UnityEngine.UI;

public class CreatePreviewForm : UGuiForm
{
    private Button CreatePreviewCloseBtn, CreatePreviewEditBtn;
    private Text AuthorName, AssetName, AssetDescribe;
    private Transform AssetParent, VirtualHumanPerfabParent;
    private Image AssetImg;
    private Button RightBtn, LeftBtn;
    private int CurrentIndex = 0;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        CreatePreviewCloseBtn = transform.Find("Hand/CreatePreviewCloseBtn").GetComponent<Button>();
        CreatePreviewEditBtn = transform.Find("Hand/CreatePreviewEditBtn").GetComponent<Button>();
        AuthorName = transform.Find("Hand/AuthorName").GetComponent<Text>();
        AssetParent = transform.Find("Centent/Asset").GetComponent<Transform>();
        AssetName = transform.Find("Centent/Name").GetComponent<Text>();
        AssetDescribe = transform.Find("Centent/Describe").GetComponent<Text>();
        AssetImg = transform.Find("Centent/AssetImg").GetComponent<Image>();
        VirtualHumanPerfabParent = transform.Find("VirtualHuman").GetComponent<Transform>();
        LeftBtn = transform.Find("Centent/LeftBtn0/LeftBtn1").GetComponent<Button>();
        RightBtn = transform.Find("Centent/RightBtn0/RightBtn1").GetComponent<Button>();

        CreatePreviewCloseBtn.onClick.AddListener(OnClickCreatePreviewClose);
        CreatePreviewEditBtn.onClick.AddListener(OnClickCreatePreviewEdit);
        LeftBtn.onClick.AddListener(OnClickLeft);
        RightBtn.onClick.AddListener(OnClickRight);

        ShowCreatePreviewContent(DefuteProductionData(), DefuteOwnCardsByProductionIdData());
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }

    private void ShowCreatePreviewContent(QueryProductionData queryProductionData,List<QueryOwnCardsByProductionIdData> queryOwnCardsByProductionIdDataList)
    {
        if (queryProductionData != null)
        {
            if (queryProductionData.production_name != "")
            {
                AuthorName.text = queryProductionData.production_name;
                //var VirtualHumanPrefab = Instantiate(Resources.Load("传入下载后的模型地址")) as GameObject;
                //VirtualHumanPrefab.transform.parent = VirtualHumanPerfabParent;
                //VirtualHumanPrefab.transform.localScale = Vector3.one;
                //VirtualHumanPrefab.transform.localPosition = Vector3.zero;
                //VirtualHumanPrefab.transform.localEulerAngles = Vector3.zero;
            }
        }

        Debug.Log("CurrentIndex: " + CurrentIndex);
        if (queryOwnCardsByProductionIdDataList != null)
        {
            if (queryOwnCardsByProductionIdDataList[CurrentIndex].card_remark != null)
            {
                AssetName.text = queryOwnCardsByProductionIdDataList[CurrentIndex].card_remark;
            }
            if (queryOwnCardsByProductionIdDataList[CurrentIndex].voice_content != null)
            {
                AssetDescribe.text = queryOwnCardsByProductionIdDataList[CurrentIndex].voice_content;
            }
            switch (queryOwnCardsByProductionIdDataList[CurrentIndex].resource_type)
            {
                case 1:
                    if (queryOwnCardsByProductionIdDataList[CurrentIndex].file_url != null)
                    {
                        AssetParent.gameObject.SetActive(true);
                        AssetImg.gameObject.SetActive(false);
                        //var AssetPerfab = Instantiate(Resources.Load("传入下载后的模型")) as GameObject;
                        //AssetPerfab.transform.parent = AssetParent;
                        //AssetPerfab.transform.localScale = Vector3.one;
                        //AssetPerfab.transform.localPosition = Vector3.zero;
                        //AssetPerfab.transform.localEulerAngles = Vector3.zero;
                    }
                    break;
                case 2:
                    if (queryOwnCardsByProductionIdDataList[CurrentIndex].file_url != null)
                    {
                        AssetParent.gameObject.SetActive(false);
                        AssetImg.gameObject.SetActive(true);
                        //AssetImg.sprite = (Sprite)Instantiate(Resources.Load("传入下载后的图片地址"));
                        //AssetImg.transform.parent = AssetParent;
                        //AssetImg.transform.localScale = Vector3.one;
                        //AssetImg.transform.localPosition = Vector3.zero;
                        //AssetImg.transform.localEulerAngles = Vector3.zero;
                    }
                    break;
                default:
                    break;
            }


        }

    }

    private void OnClickCreatePreviewClose()
    {
        this.Close();
    }

    private void OnClickCreatePreviewEdit()
    {
        GameEntry.UI.OpenUIForm(UIFormId.CreateViewForm);
    }

    private void OnClickLeft()
    {
        RightBtn.transform.gameObject.SetActive(true);
        CurrentIndex--;
        if (CurrentIndex == 0)
        {
            CurrentIndex = 0;
            LeftBtn.transform.gameObject.SetActive(false);
        }
        else
        {           
            LeftBtn.transform.gameObject.SetActive(true);
        }
        ShowCreatePreviewContent(DefuteProductionData(), DefuteOwnCardsByProductionIdData());
        Debug.Log("Left Op CurrentIndex: " + CurrentIndex);
    }
    private void OnClickRight()
    {
        LeftBtn.transform.gameObject.SetActive(true);
        CurrentIndex++;
        if (CurrentIndex == 5)
        {
            CurrentIndex = 5;
            RightBtn.transform.gameObject.SetActive(false);
        }
        else
        {
            RightBtn.transform.gameObject.SetActive(true);
        }
        ShowCreatePreviewContent(DefuteProductionData(), DefuteOwnCardsByProductionIdData());
        Debug.Log("Right Op CurrentIndex: " + CurrentIndex);
    }

    //默认数据用来测试
    QueryProductionData queryProductionData = new QueryProductionData();
    QueryProductionData DefuteProductionData()
    {
        queryProductionData.production_name = "我是李日天";
        return queryProductionData;
            
    }


    //默认数据用来测试
    List<QueryOwnCardsByProductionIdData> queryOwnCardsByProductionIdDatasList = new List<QueryOwnCardsByProductionIdData>(6);
    List<QueryOwnCardsByProductionIdData> DefuteOwnCardsByProductionIdData()
    {
        for (int i = 0; i < 6; i++)
        {
        QueryOwnCardsByProductionIdData queryOwnCardsByProductionIdData = new QueryOwnCardsByProductionIdData();
            queryOwnCardsByProductionIdData.card_remark = "我是卡片：" + i.ToString();
            queryOwnCardsByProductionIdData.voice_content = "我是介绍：" + i.ToString();
            queryOwnCardsByProductionIdData.resource_type = 1;
            if (i == 2 || i == 5)
            {
                queryOwnCardsByProductionIdData.resource_type = 2;
            }
            queryOwnCardsByProductionIdDatasList.Add(queryOwnCardsByProductionIdData);
        }
        return queryOwnCardsByProductionIdDatasList;
    }

}
