using UnityEngine;
using System;

namespace StarForce
{

    //����������Ʒ��Ϣ
    [Serializable]
    public  class ProductionData
    {
        public string production_name = "";         //��Ʒ����
        public string title = "";                   //��Ʒ����
        public int production_type = 1;             //��Ʒ����  Ĭ��ʹ��1
        public string cover_img_url = "";           //����ͼ·��
        public string description = "";             //��Ʒ����
        public int dh_id = 12;                      //������id
        public int br_id = 1;                       //������Դid
        public string address = "";                 //��Ʒ������ַ   ���״̬  1:�����2:��˳ɹ� 3:���ʧ��
        public string lon = "";                     //λ�þ���
        public string lat = "";                     //λ��γ��
        public CardData[] cardDatas = null;
    }

    //��ѯ��Ʒ��Ϣ�б���Ʒ����  1:��ͨ (���˴���)2:��Ʒ(����Ա����)��
    [Serializable]
    public  class QueryProductionData
    {
        public int id = 0;                          //����ID
        public string production_name;              //��Ʒ����
        public string title = "";                   //��Ʒ����
        public string cover_img_url = "";           //����ͼ·��
        public string description = "";             //��Ʒ����
        public int dh_id = 12;                      //������id
        public string human_name = "";              //����������
        public string thumbnail_url = "";           //����������ͼ
        public int sex = 1;                         //�������Ա�  (�Ա� 1����  2��Ů)
        public int model_type = 1;                  //������ģ������ (ģ������ 1����ͨ����  2��3Dдʵ)
        public string model_url = "";               //������ģ���ļ���ַ
        public string file_type = "";               //�������ļ�����(�ļ���չ��)
        public int br_id = 0;                       //������Դid
        public string name = "";                    //������Դ����
        public string file_url = "";                //���������ļ���ַ
        public string address = "";                 //��Ʒ������ַ
        public string lon = "";                     //λ�þ���
        public string lat = "";                     //λ��γ��
        public int audit_status = 1;                //���״̬ (���״̬  1:�����2:��˳ɹ� 3:���ʧ��)
        public string audit_remark = "";            //������ (���ʧ��ʱ��ʾ)
        public int c_uid = 0;                       //ӵ����id
        public string createUser = "";              //ӵ����

    }


    //ͨ��������ƷID��ѯ��Ʒ��Ƭ��Ϣ
    [Serializable]
    public  class QueryOwnCardsByProductionIdData
    {
        public int id = 0;
        public string card_remark = "";
        public string voice_content = "";
        public int resource_id = 0;                  //��ע����ѯ��Դ�ӿڵõ������б� ѡ��õ�
        public int index = 0;                        //��ע�����ſ�Ƭ��������  ����ԽСԽ��ǰ
        public string file_url = "";
        public string img_url = "";
        public int resource_type = 1;                //��Դ���� 1:ģ�� 2:ͼƬ
    }

    [Serializable]
    public  class CardData
    {
        public string card_remark = "";              //��Ʒ��Ƭ����
        public string voice_content = "";            //������������
        public int resource_id = 1;                  //��ԴID
        public int index = 1;                        //��Ʒ����
    }


    //��ѯ������Ʒ��Ϣ
    [Serializable]
    public class QueryNearbyProductionData
    {
        public string production_name = "";           //��Ʒ����
        public string title = "";                     //��Ʒ����
        public string cover_img_url = "";             //����ͼ·��
        public string description = "";               //��Ʒ����
        public string address = "";                   //��Ʒ������ַ
        public string lon = "";                       //λ�þ���
        public string lat = "";                       //λ��γ��
        public string createUser = "";                //ӵ����

    }


}