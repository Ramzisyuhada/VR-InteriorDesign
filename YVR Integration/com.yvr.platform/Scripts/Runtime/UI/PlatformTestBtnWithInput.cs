using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformTestBtnWithInput : PlatformTestBtn
{
    public Button requestBtn;

    private void Start()
    {
        InitInputList();

        requestBtn.onClick.AddListener(OnButtonClicked);
    }

    private List<ParamInputComponent> inputList = new List<ParamInputComponent>();

    private void InitInputList()
    {
        inputList.AddRange(transform.GetComponentsInChildren<ParamInputComponent>());
    }

    public override void OnButtonClicked()
    {
        switch (btnType)
        {
            case BtnType.Default:
                break;
            case BtnType.AchievementAddCount:
                AchievementAddCount();
                break;
            case BtnType.AchievementAddFields:
                AchievementAddFields();
                break;
            case BtnType.UnlockAchievement:
                UnlockAchievement();
                break;
            case BtnType.GetDefinitionByName:
                GetDefinitionByName();
                break;
            case BtnType.GetProgressByName:
                GetProgressByName();
                break;
            case BtnType.Initialize:
                Initialize();
                break;
            case BtnType.GetFriendInfo:
                GetFriendInfo();
                break;
            case BtnType.None:
                break;
            default:
                break;
        }
    }

    private void Initialize()
    {
        if (inputList.Count < 1)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        long appID = long.Parse(inputList[0].GetParamData());

        YVR.Platform.YVRPlatform.Initialize(appID);
    }

    private void AchievementAddCount()
    {
        if (inputList.Count < 2)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        string name = inputList[0].GetParamData();
        int count = int.Parse(inputList[1].GetParamData());

        YVR.Platform.Achievement.AchievementAddCount(name, count).OnComplete(AchievementAddCountCallback);
    }
    private void AchievementAddCountCallback(YVR.Platform.YVRMessage msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.Log("Achievement add count callback");
    }

    private void AchievementAddFields()
    {
        if (inputList.Count < 2)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        string name = inputList[0].GetParamData();
        string field = inputList[1].GetParamData();

        YVR.Platform.Achievement.AchievementAddFields(name, field).OnComplete(AchievementAddFieldsCallback);
    }
    private void AchievementAddFieldsCallback(YVR.Platform.YVRMessage msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.Log("Achievement add fields callback");
    }

    private void UnlockAchievement()
    {
        if (inputList.Count < 1)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        string name = inputList[0].GetParamData();

        YVR.Platform.Achievement.UnlockAchievement(name).OnComplete(UnlockAchievementCallback);
    }
    private void UnlockAchievementCallback(YVR.Platform.YVRMessage msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.Log("Unlock achievement callback");
    }

    private void GetDefinitionByName()
    {
        if (inputList.Count < 1)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        string[] names = inputList[0].GetParamData().Split(';');

        YVR.Platform.Achievement.GetDefinitionByName(names).OnComplete(GetDefinitionByNameCallback);
    }
    private void GetDefinitionByNameCallback(YVR.Platform.YVRMessage<YVR.Platform.AchievementDefinitionList> msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.Log("DefinitionByName : " + msg.data.Count);
    }

    private void GetProgressByName()
    {
        if (inputList.Count < 1)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        string[] names = inputList[0].GetParamData().Split(';');

        YVR.Platform.Achievement.GetProgressByName(names).OnComplete(GetProgressByNameCallback);
    }
    private void GetProgressByNameCallback(YVR.Platform.YVRMessage<YVR.Platform.AchievementProgressList> msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.Log("ProgressByName : " + msg.data.Count);
    }

    private void GetFriendInfo()
    {
        if (inputList.Count < 1)
        {
            Debug.LogError("Param InputField count is not enough");
            return;
        }

        int accountID = int.Parse(inputList[0].GetParamData());

        YVR.Platform.Friends.GetFriendInfomation(accountID).OnComplete(GetFriendInfoCallback);
    }
    private void GetFriendInfoCallback(YVR.Platform.YVRMessage<YVR.Platform.FriendInfo> msg)
    {
        if (msg.isError)
        {
            Debug.LogError("curr msg is error!");
            return;
        }

        Debug.LogFormat("account ID : {0} \nuser name : {1} \nuser age : {2} \nuser sex : {3}\nuser icon : {4} \nuser online state : {5}", msg.data.accountID, msg.data.userName, msg.data.age, msg.data.userSex, msg.data.userIcon, msg.data.onlineState);
    }
}
