using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarForce;
using UnityEngine.UI;


public enum Resource_Type
{
    Model,
    Image
}

public class CreateViewForm : UGuiForm
{
    Button CreateViewCloseBtn, CreateViewSaveBtn, EditFengMianBtn, EditVirtualHumanBtn;
    Button Bottom_ModelBtn, Bottom_ImageBtn, Bottom_TextBtn, Bottom_AudioBtn;
    GameObject AssetSelect;
    Button DefautAddBtn;
    GameObject AssetModel;
    Text AssetName, AssetDescribe;
    Image AssetImg;

    Resource_Type resource_Type;

    int MaxCardCount;//���Ƭλ
    int CurrentCardCount;//��ǰӵ�п�Ƭ����
    int CurrentSelectIndex = 0;//��ǰ�༭�Ŀ�Ƭ
    Button[] CardBtns;
    Image[] CardIcons;

    ProductionData EditProductionData = new ProductionData();//�༭��������(�ϴ���������)
    ProductionData ShowProductionData = new ProductionData();//չʾ��������(�ӷ�������ȡ����)



    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        CreateViewCloseBtn = transform.Find("Hand/CreateViewCloseBtn").GetComponent<Button>();
        CreateViewSaveBtn = transform.Find("Hand/CreateViewSaveBtn").GetComponent<Button>();
        EditFengMianBtn = transform.Find("Hand/EditFengMianBtn").GetComponent<Button>();
        EditVirtualHumanBtn = transform.Find("Hand/EditVirtualHumanBtn").GetComponent<Button>();

        Bottom_ModelBtn = transform.Find("Bottoms/ModelBtn").GetComponent<Button>();
        Bottom_ImageBtn = transform.Find("Bottoms/ImageBtn").GetComponent<Button>();
        Bottom_TextBtn = transform.Find("Bottoms/TextBtn").GetComponent<Button>();
        Bottom_AudioBtn = transform.Find("Bottoms/AudioBtn").GetComponent<Button>();
        AssetSelect = transform.Find("Bottoms/Contens/conten0/AssetSelectMenu").GetComponent<AssetSelectMenu>().gameObject;

        DefautAddBtn = transform.Find("Centent/DefuatAddIconBtn").GetComponent<Button>();
        AssetModel = transform.Find("Centent/AssetModel").gameObject;
        AssetImg = transform.Find("Centent/AssetImg").GetComponent<Image>();
        AssetName = transform.Find("Centent/Name").GetComponent<Text>();
        AssetDescribe = transform.Find("Centent/Describe").GetComponent<Text>();
        AssetModel.SetActive(false);
        AssetImg.gameObject.SetActive(false);



        MaxCardCount = 6;
        CurrentCardCount = 2;//�༭��ʱ���Լ���ӻ���ֱ�Ӵӷ�����������
        CardBtns = new Button[MaxCardCount];
        CardIcons = new Image[MaxCardCount];
        for (int i = 0; i < MaxCardCount; i++)
        {
            CardBtns[i] = transform.Find("Hand/Cards/List/Point" + (i+1).ToString() + "/add").GetComponent<Button>();
            CardIcons[i] = transform.Find("Hand/Cards/List/Point" + (i+1).ToString() + "/img").GetComponent<Image>();
            CardBtns[i].gameObject.SetActive(false);
            CardIcons[i].gameObject.SetActive(false);
            CardBtns[i].onClick.AddListener(() => {
                ShowCardsData(i);
            });
            CardIcons[i].GetComponent<Button>().onClick.AddListener(() => {
                ShowCardsData(i);
            });
        }

        if (CurrentCardCount != MaxCardCount)
        {
            CardBtns[CurrentCardCount].gameObject.SetActive(true);
        }
        for (int j = 0; j < CurrentCardCount; j++)
        {
            if (j < CurrentCardCount)
            {
                CardIcons[j].gameObject.SetActive(true);
            }
        }


        CreateViewCloseBtn.onClick.AddListener(OnCloseClick);
        CreateViewSaveBtn.onClick.AddListener(OnSavaClick);
        EditFengMianBtn.onClick.AddListener(OnEditFengMianClick);
        EditVirtualHumanBtn.onClick.AddListener(OnEditVirtualHumanClick);
        Bottom_ModelBtn.onClick.AddListener(OnModelClick);
        Bottom_ImageBtn.onClick.AddListener(OnImageClick);
        Bottom_TextBtn.onClick.AddListener(OnTextClick);
        Bottom_AudioBtn.onClick.AddListener(OnAudioClick);
        DefautAddBtn.onClick.AddListener(OnAssetAddClick);

    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }

    //�رմ�������
    private void OnCloseClick()
    {
        this.Close();
    }

    //���洴��
    private void OnSavaClick()
    {
        Debug.Log("Sava...");
    }

    //���ģ��/���˴�������༭
    private void OnEditFengMianClick()
    {
        Debug.Log("OnEditFengMianClick...");
    }


    //��������˱༭
    private void OnEditVirtualHumanClick()
    {
        Debug.Log("OnEditVirtualHumanClick...");
    }

    private void OnAssetAddClick()
    {
        switch (resource_Type)
        {
            case Resource_Type.Model:
                OnModelClick();
                break;
            case Resource_Type.Image:
                OnImageClick();
                break;
            default:
                break;
        }
    }


    //���ģ��ģ��
    private void OnModelClick()
    {
        AssetSelect.gameObject.transform.parent.gameObject.SetActive(true);
    }


    //���ͼƬģ��
    private void OnImageClick()
    {
        Debug.Log("OnImageClick...");
        AssetSelect.gameObject.transform.parent.gameObject.SetActive(true);
    }

    //����ı�ģ��
    private void OnTextClick()
    {
        Debug.Log("OnTextClick...");
        GameEntry.UI.OpenUIForm(UIFormId.CreateEditTextForm);
    }

    //�����Ƶģ��
    private void OnAudioClick()
    {
        Debug.Log("OnAudioClick...");
        GameEntry.UI.OpenUIForm(UIFormId.CreateEditAudioForm);
    }


    //ˢ�¿�Ƭ����
    private void RefrshCardsData()
    {

    }

    //��ʾ��ǰ��Ƭ����
    private void ShowCardsData(int index)
    {
        if (queryOwnCardsByProductionIdDatas == null)
            return;
        AssetName.text = queryOwnCardsByProductionIdDatas[index].card_remark;
        AssetDescribe.text = queryOwnCardsByProductionIdDatas[index].voice_content;
        if (queryOwnCardsByProductionIdDatas[index].resource_type == 1)
        {
            AssetImg.gameObject.SetActive(false);
            AssetModel.SetActive(true);
            //����ģ��
            //var Perfab = Instantiate();
            
        }
        else if (queryOwnCardsByProductionIdDatas[index].resource_type == 2)
        {
            AssetImg.gameObject.SetActive(true);
            AssetModel.SetActive(false);
            //����ͼƬ
            //AssetImg.sprite =
        }
    }

    //�û��༭��Ʒ��Ϣ���ݣ������ϴ�������
    private void UserEditProductionData(string production_name, string title, int production_type, string cover_img_url, 
        string description, int dh_id, int br_id, string address, string lon, string lat, CardData[] cardDatas)
    {
        EditProductionData.production_name = production_name;
        EditProductionData.title = title;
        EditProductionData.production_type = production_type;
        EditProductionData.cover_img_url = cover_img_url;
        EditProductionData.description = description;
        EditProductionData.dh_id = dh_id;
        EditProductionData.br_id = br_id;
        EditProductionData.address = address;
        EditProductionData.lon = lon;
        EditProductionData.lat = lat;
        EditProductionData.cardDatas = cardDatas;
    }

    //�༭��Ƭ����
    private CardData UserEditCardsData(string card_remark, string voice_content, int resource_id, int index)
    {
        CardData cardData = new CardData
        {
            card_remark = card_remark,
            voice_content = voice_content,
            resource_id = resource_id,
            index = index
        };
        return cardData;
    }

    QueryOwnCardsByProductionIdData[] queryOwnCardsByProductionIdDatas;
    //ͨ����ƷID���п�Ƭ���ݲ�ѯ
    private void GetCardData(int production_id)
    {
        int CardCount = 6;
        queryOwnCardsByProductionIdDatas = new QueryOwnCardsByProductionIdData[CardCount];
        for (int i = 0; i < CardCount; i++)
        {
            queryOwnCardsByProductionIdDatas[i].card_remark = "";
            queryOwnCardsByProductionIdDatas[i].voice_content = "";
            queryOwnCardsByProductionIdDatas[i].resource_id = 0;
            queryOwnCardsByProductionIdDatas[i].index = 0;
            queryOwnCardsByProductionIdDatas[i].file_url = "";
            queryOwnCardsByProductionIdDatas[i].img_url = "";
            queryOwnCardsByProductionIdDatas[i].resource_type = 1;
        }


    }

}
