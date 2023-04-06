using UnityEngine;
using System;

namespace StarForce
{

    //创作个人作品信息
    [Serializable]
    public  class ProductionData
    {
        public string production_name = "";         //作品名称
        public string title = "";                   //作品标题
        public int production_type = 1;             //作品类型  默认使用1
        public string cover_img_url = "";           //封面图路径
        public string description = "";             //作品描述
        public int dh_id = 12;                      //数字人id
        public int br_id = 1;                       //背景资源id
        public string address = "";                 //作品发布地址   审核状态  1:审核中2:审核成功 3:审核失败
        public string lon = "";                     //位置经度
        public string lat = "";                     //位置纬度
        public CardData[] cardDatas = null;
    }

    //查询作品信息列表（作品类型  1:普通 (个人创建)2:精品(管理员创建)）
    [Serializable]
    public  class QueryProductionData
    {
        public int id = 0;                          //主键ID
        public string production_name;              //作品名称
        public string title = "";                   //作品标题
        public string cover_img_url = "";           //封面图路径
        public string description = "";             //作品描述
        public int dh_id = 12;                      //数字人id
        public string human_name = "";              //数字人名称
        public string thumbnail_url = "";           //数字人缩略图
        public int sex = 1;                         //数字人性别  (性别 1：男  2：女)
        public int model_type = 1;                  //数字人模型类型 (模型类型 1：卡通类型  2：3D写实)
        public string model_url = "";               //数字人模型文件地址
        public string file_type = "";               //数字人文件类型(文件扩展名)
        public int br_id = 0;                       //背景资源id
        public string name = "";                    //背景资源名称
        public string file_url = "";                //背景声音文件地址
        public string address = "";                 //作品发布地址
        public string lon = "";                     //位置经度
        public string lat = "";                     //位置纬度
        public int audit_status = 1;                //审核状态 (审核状态  1:审核中2:审核成功 3:审核失败)
        public string audit_remark = "";            //审核意见 (审核失败时显示)
        public int c_uid = 0;                       //拥有者id
        public string createUser = "";              //拥有者

    }


    //通过个人作品ID查询作品卡片信息
    [Serializable]
    public  class QueryOwnCardsByProductionIdData
    {
        public int id = 0;
        public string card_remark = "";
        public string voice_content = "";
        public int resource_id = 0;                  //备注：查询资源接口得到下拉列表 选择得到
        public int index = 0;                        //备注：六张卡片排序自增  数字越小越排前
        public string file_url = "";
        public string img_url = "";
        public int resource_type = 1;                //资源类型 1:模型 2:图片
    }

    [Serializable]
    public  class CardData
    {
        public string card_remark = "";              //作品卡片描述
        public string voice_content = "";            //语音描述内容
        public int resource_id = 1;                  //资源ID
        public int index = 1;                        //作品排序
    }


    //查询附近作品信息
    [Serializable]
    public class QueryNearbyProductionData
    {
        public string production_name = "";           //作品名称
        public string title = "";                     //作品标题
        public string cover_img_url = "";             //封面图路径
        public string description = "";               //作品描述
        public string address = "";                   //作品发布地址
        public string lon = "";                       //位置经度
        public string lat = "";                       //位置纬度
        public string createUser = "";                //拥有者

    }


}