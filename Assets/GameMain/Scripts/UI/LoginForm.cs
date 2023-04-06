using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarForce;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public class LoginForm : UGuiForm
{
    private InputField m_IphoneInputField;
    private InputField m_VerificationCodeInputField;
    private Button m_CloseIphoneNumberBtn;
    private Button m_SendMessageBtn;
    private Button m_LoginInActionBtn, m_LoginInNotActionBtn;
    private Button m_IphoneNumberLoginInBtn;
    private Text m_VerificationCountDownText;
    private string m_VerificationCode;


    private bool isSend;
    private int time;
    private float t;



    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        m_IphoneInputField = transform.Find("login_bg/iphoneNumberBg/iphoneInputField").GetComponent<InputField>();
        m_CloseIphoneNumberBtn = transform.Find("login_bg/iphoneNumberBg/closeIphoneNumberBtn").GetComponent<Button>();
        m_CloseIphoneNumberBtn.gameObject.SetActive(false);
        m_VerificationCodeInputField = transform.Find("login_bg/verificationCodeBg/verificationCodeInputField").GetComponent<InputField>();
        m_SendMessageBtn = transform.Find("login_bg/verificationCodeBg/sendMessageBtn").GetComponent<Button>();
        m_VerificationCountDownText = m_SendMessageBtn.transform.Find("CountDownText").GetComponent<Text>();
        m_VerificationCountDownText.text = "��ȡ��֤��";
        m_LoginInActionBtn = transform.Find("login_bg/loginInActionBtn").GetComponent<Button>();
        m_LoginInActionBtn.gameObject.SetActive(false);
        m_LoginInNotActionBtn = transform.Find("login_bg/loginInNoActionBtn").GetComponent<Button>();
        m_LoginInNotActionBtn.gameObject.SetActive(true);
        m_IphoneNumberLoginInBtn = transform.Find("login_bg/iphoneNumberLoginInBtn").GetComponent<Button>();


        //��֤��Ĭ��ֵ123456
        m_VerificationCode = "123456";

        m_IphoneInputField.onValueChanged.AddListener((value) =>
        {
            if (m_IphoneInputField.text.Length > 0)
            {
                m_CloseIphoneNumberBtn.gameObject.SetActive(true);
            }
            else
            {
                m_CloseIphoneNumberBtn.gameObject.SetActive(false);
            }
            if (m_VerificationCodeInputField.text.Length == 6 && m_IphoneInputField.text.Length == 11)
            {
                m_LoginInActionBtn.gameObject.SetActive(true);
                m_LoginInNotActionBtn.gameObject.SetActive(false);
            }
            else
            {
                m_LoginInActionBtn.gameObject.SetActive(false);
                m_LoginInNotActionBtn.gameObject.SetActive(true);
            }
        });
        m_VerificationCodeInputField.onValueChanged.AddListener((value) =>
        {
            if (m_VerificationCodeInputField.text.Length == 6 && m_IphoneInputField.text.Length == 11)
            {
                m_LoginInActionBtn.gameObject.SetActive(true);
                m_LoginInNotActionBtn.gameObject.SetActive(false);
            }
            else
            {
                m_LoginInActionBtn.gameObject.SetActive(false);
                m_LoginInNotActionBtn.gameObject.SetActive(true);
            }
        });


        m_CloseIphoneNumberBtn.onClick.AddListener(OnClearIphoneNumber);
        m_SendMessageBtn.onClick.AddListener(OnVerificationCodeSendMessage);
        m_LoginInActionBtn.onClick.AddListener(OnLogin);
        m_IphoneNumberLoginInBtn.onClick.AddListener(OnOneKeyLoginIn);

        Debug.LogError("Open LoginFOrm  Qiuwenxue  Sucesss ------------");
    }

    protected override void OnOpen(object userData)
    { 
        base.OnOpen(userData);
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
    }


    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);

        if (isSend)
        {
            //����ʱ
            m_VerificationCountDownText.text = "���»�ȡ"+ time.ToString()+"s";
            t += Time.deltaTime;
            if (t >= 1)
            {
                time--;
                t = 0;
            }
            if (time < 0)
            {
                isSend = false;
                m_SendMessageBtn.gameObject.GetComponent<Button>().enabled = true;
                m_VerificationCountDownText.text = "��ȡ��֤��";
            }
        }
    }

    /// <summary>
    /// ����ֻ���
    /// </summary>
    private void OnClearIphoneNumber()
    {
        m_IphoneInputField.text = null;
    }

    /// <summary>
    /// �����ֻ��Ż�ȡ��֤��
    /// </summary>
    private void OnVerificationCodeSendMessage()
    {
        if (m_IphoneInputField.text.Length != 11)
        {
            m_IphoneInputField.text = null;
            StarForce.GameEntry.UI.OpenDialog(new DialogParams()
            {
                Mode = 1,
                Title = "��¼",
                Message = "�������ֻ��Ż�����ȷ��֤��",
                OnClickConfirm = delegate (object userData) { StarForce.GameEntry.UI.OpenUIForm(UIFormId.LoginForm); },
            });
            return;
        }
        m_SendMessageBtn.gameObject.GetComponent<Button>().enabled = false;
        isSend = true;
        time = 59;
        OnReturnVerificationCode();
    }

    /// <summary>
    /// ������֤��
    /// </summary>
    /// <returns></returns>
    private string OnReturnVerificationCode()
    {
        return m_VerificationCode;
    }

    /// <summary>
    /// ��¼
    /// </summary>
    private void OnLogin()
    {

        if (m_VerificationCodeInputField.text != m_VerificationCode)
        {
            StarForce.GameEntry.UI.OpenDialog(new DialogParams()
            {
                Mode = 1,
                Title = "��¼",
                Message = "�������ֻ��Ż�����ȷ��֤��",
                OnClickConfirm = delegate (object userData) { StarForce.GameEntry.UI.OpenUIForm(UIFormId.LoginForm); },
            });
        }
        //��¼�ɹ�������
        if (m_IphoneInputField.text.Length == 11 && m_VerificationCodeInputField.text == m_VerificationCode)
        {
            m_IphoneInputField.text = null;
            m_VerificationCodeInputField.text = null;
            isSend = false;
            m_SendMessageBtn.gameObject.GetComponent<Button>().enabled = true;
            m_VerificationCountDownText.text = "��ȡ��֤��";
            StarForce.GameEntry.UI.OpenUIForm(UIFormId.HomeForm);
        }
    }

    private void OnOneKeyLoginIn()
    {
        StarForce.GameEntry.UI.OpenUIForm(UIFormId.HomeForm);
        return;
        StarForce.GameEntry.UI.OpenDialog(new DialogParams()
        {
            Mode = 1,
            Title = "һ����¼",
            Message = "�ù������ڿ����У������ڴ���",
            OnClickConfirm = delegate (object userData) { StarForce.GameEntry.UI.OpenUIForm(UIFormId.LoginForm); },
        });
    }


}
