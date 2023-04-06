using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarForce;
using UnityEngine.UI;
public class CreateEditTextForm : UGuiForm
{
    Button backBtn, SaveBtn, ClearBtn;
    InputField NameInput, ContentInput;
    Text TipTextCount;
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);

        backBtn = transform.Find("Hand/BackBtn").GetComponent<Button>();
        SaveBtn = transform.Find("Hand/SaveBtn").GetComponent<Button>();
        ClearBtn = transform.Find("Hand/Namebg/ClearBtn").GetComponent<Button>();
        NameInput = transform.Find("Hand/Namebg/InputField").GetComponent<InputField>();
        ContentInput= transform.Find("Hand/Contentbg/InputField").GetComponent<InputField>();
        TipTextCount = transform.Find("Hand/Contentbg/TipTextCount").GetComponent<Text>();

        ContentInput.characterLimit = 30;
        ContentInput.onValueChanged.AddListener((value) =>
        {
            if (ContentInput.text.Length > 30)
            {
                return;
            }
              
            TipTextCount.text = ContentInput.text.Length.ToString() + "/30×Ö";
        });


        backBtn.onClick.AddListener(OnBackClick);
        SaveBtn.onClick.AddListener(OnSaveClick);
        ClearBtn.onClick.AddListener(OnClearClick);

    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }

    private void OnBackClick()
    {
        this.Close();
    }

    private void OnSaveClick()
    {

    }

    private void OnClearClick()
    {
        NameInput.text = "";
    }

}
