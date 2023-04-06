using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomMenu : MonoBehaviour
{
    public UIFormId uiFormId = UIFormId.Undefined;
    QToggleButton Btn_Home;
    QToggleButton Btn_Mine;

     static int BackId = -1;
    // Start is called before the first frame update
    void Awake()
    {
        Btn_Home = this.transform.Find("BottomImage/Btn_Home").GetComponent<QToggleButton>();
        Btn_Mine = this.transform.Find("BottomImage/Btn_Mine").GetComponent<QToggleButton>();

        Btn_Home.onClick.AddListener(OnHomeClick);
        Btn_Mine.onClick.AddListener(OnMineClick);
    }

    public UIFormId UIFormId
    {
        set
        {
            uiFormId = value;
            Refresh();
        }
    }

    private void Refresh()
    {
        if (uiFormId == UIFormId.HomeForm)
        {
            Btn_Mine.SetNormalStatus();
            Btn_Home.SetSelectedStatus();
        }
        if (uiFormId == UIFormId.MineForm)
        {
            Btn_Home.SetNormalStatus();
            Btn_Mine.SetSelectedStatus();
        }
    }

    public void OnEnable()
    {
        Refresh();
    }

    void OnHomeClick(bool isOn)
    {
        if (uiFormId == UIFormId.HomeForm)
        {
            return;
        }
        if (isOn)
        {
            if (BackId>0)
            {
                GameEntry.UI.CloseUIForm(BackId);
            }
        }
    }

    void OnMineClick(bool isOn)
    {
        if (uiFormId == UIFormId.MineForm)
        {
            return;
        }
        if (isOn)
        {
            BackId = (int)GameEntry.UI.OpenUIForm(UIFormId.MineForm);
        }
    }
}
