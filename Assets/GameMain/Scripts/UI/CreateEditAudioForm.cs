using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarForce;
using UnityEngine.UI;
using System;

public class CreateEditAudioForm : UGuiForm
{
    Button backBtn, SaveBtn, ClearBtn;
    InputField NameInput, ContentInput;
    Text TipTextCount;
    Button PlayBtn, PauseBtn;
    GameObject PlayIcon, PauseIcon;
    Button PeiYinBtn, PeiYinFinishBtn;
    GameObject PeiYinMask;
    Slider MySlider;
    Text CurrentTimeText, TotalTimeText;
    bool IsPlay;
    float CurrentTime, TotalTime = 60;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);

        backBtn = transform.Find("Hand/BackBtn").GetComponent<Button>();
        SaveBtn = transform.Find("Hand/SaveBtn").GetComponent<Button>();
        ClearBtn = transform.Find("Hand/Namebg/ClearBtn").GetComponent<Button>();
        NameInput = transform.Find("Hand/Namebg/InputField").GetComponent<InputField>();
        ContentInput = transform.Find("Hand/Contentbg/InputField").GetComponent<InputField>();
        TipTextCount = transform.Find("Hand/Contentbg/TipTextCount").GetComponent<Text>();
        PlayBtn = transform.Find("Bottom/PlayBtn").GetComponent<Button>();
        PauseBtn = transform.Find("Bottom/PauseBtn").GetComponent<Button>();
        PlayIcon = transform.Find("Bottom/PlayBtn/PlayIcon").gameObject;
        PauseIcon = transform.Find("Bottom/PlayBtn/PauseIcon").gameObject;
        PeiYinBtn = transform.Find("Bottom/PeiYinBtn").GetComponent<Button>();
        PeiYinMask = transform.Find("PeiYinMask").gameObject;
        PeiYinFinishBtn = transform.Find("PeiYinMask/PeiYinFinishBtn").GetComponent<Button>();
        MySlider = transform.Find("Bottom/Slider").GetComponent<Slider>();
        CurrentTimeText = MySlider.transform.Find("CurrentTime").GetComponent<Text>();
        TotalTimeText = MySlider.transform.Find("TotalTime").GetComponent<Text>();

        ContentInput.characterLimit = 180;
        ContentInput.onValueChanged.AddListener((value) =>
        {
            if (ContentInput.text.Length > 180)
            {
                return;
            }

            TipTextCount.text = ContentInput.text.Length.ToString() + "/180字";
        });


        backBtn.onClick.AddListener(OnBackClick);
        SaveBtn.onClick.AddListener(OnSaveClick);
        ClearBtn.onClick.AddListener(OnClearClick);
        PlayBtn.onClick.AddListener(OnPlayClick);
        PauseBtn.onClick.AddListener(OnPauseClick);
        PeiYinBtn.onClick.AddListener(OnPeiYinClick);
        PeiYinFinishBtn.onClick.AddListener(OnPeiYinFinishClick);

    }


    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        PeiYinMask.SetActive(false);
        PauseBtn.gameObject.SetActive(false);
        PeiYinBtn.gameObject.SetActive(true);
        PlayIcon.SetActive(true);
        PauseIcon.SetActive(false);
        MySlider.gameObject.SetActive(false);
        IsPlay = false;
        TotalTimeText.text = (TotalTime / 60) + ":" + string.Format("{0:D2}", (int)(TotalTime % 60));
        MySlider.value = 0;
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
        IsPlay = false;
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);

        if (IsPlay)
        {
            
            if (CurrentTime < TotalTime)
            {
                CurrentTime += Time.deltaTime;
                TimeSpan currentTimeTemp = new TimeSpan(0, 0, (int)CurrentTime);
                CurrentTimeText.text = string.Format("{0:D2}", currentTimeTemp.Minutes) + ":" + string.Format("{0:D2}", currentTimeTemp.Seconds);
                MySlider.value = CurrentTime / TotalTime;
            }
            else
            {
                IsPlay = false;
                CurrentTime = 0;
                CurrentTimeText.text = TotalTimeText.text;
            }
        }
    }


    //返回上一界面
    private void OnBackClick()
    {
        this.Close();
    }

    //点击保存进行数据保存
    private void OnSaveClick()
    {
        //TODO保存数据进行页面关闭
        this.Close();
    }

    //清空名字输入内容
    private void OnClearClick()
    {
        NameInput.text = "";
    }

    //点击语音播报
    private void OnPlayClick()
    {
        PlayIcon.SetActive(false);
        PauseIcon.SetActive(true);
        IsPlay = true;
    }

    //点击语音播报暂停
    private void OnPauseClick()
    {
        PlayIcon.SetActive(true);
        PauseIcon.SetActive(false);
        IsPlay = false;
    }


    //点击配音按钮
    private void OnPeiYinClick()
    {
        //TODO 进行文字转语音
        PeiYinMask.SetActive(true);
    }

    //关闭配音中界面
    private void OnPeiYinFinishClick()
    {
        PeiYinMask.SetActive(false);
        PeiYinBtn.gameObject.SetActive(false);
        PauseBtn.gameObject.SetActive(true);
        MySlider.gameObject.SetActive(true);
    }

}
