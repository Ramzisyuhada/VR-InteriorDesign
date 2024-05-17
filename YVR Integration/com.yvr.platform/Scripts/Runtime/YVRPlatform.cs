﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate all android API
    /// </summary>
    public class YVRPlatform
    {
        private static readonly AndroidJavaClass permissionJavaClass = new AndroidJavaClass("com.yvr.thirdsdk.YvrPermission");
        private static readonly AndroidJavaClass requestJavaClass = new AndroidJavaClass("com.yvr.thirdsdk.Request");
        private static readonly AndroidJavaClass accountManagerJavaClass = new AndroidJavaClass("com.yvr.thirdsdk.AccountManager");
        private static readonly AndroidJavaClass achievementJavaClass = new AndroidJavaClass("com.yvr.thirdsdk.Achievement");
        private static readonly AndroidJavaClass deeplinkJavaClass = new AndroidJavaClass("com.yvr.thirdsdk.ContextUtil");
        private static readonly AndroidJavaClass friendsJavaClass = new AndroidJavaClass("com.yvr.thirdsdk.FriendsManager");

        /// <summary>
        /// Whether platform sdk is initialized
        /// </summary>
        public static bool IsInitialized
        {
            get
            {
                if (!isPlatformInitialized)
                    Debug.LogError("[YVRPlatform] YVR Platform is not initialized.");

                return isPlatformInitialized;
            }
        }

        private static bool isPlatformInitialized = false;

        /// <summary>
        /// Initialize platform sdk
        /// </summary>
        /// <param name="appId"></param>
        public static void Initialize(long appId)
        {
            Debug.Log("App ID : " + appId);

            isPlatformInitialized = YVR_Initialize_PlatformInit(appId);

            if (!isPlatformInitialized)
            {
                throw new UnityException("[YVRPlatform] YVR Platform failed to initialize.");
            }

            (new GameObject("YVRCallbackRunner")).AddComponent<YVRCallbackRunner>();
        }

        #region Entitlement
        private static bool YVR_Initialize_PlatformInit(long appId)
        {
            return requestJavaClass.CallStatic<bool>("initYvrPlatformSdk", appId, null);
        }

        /// <summary>
        /// Asynchronous function for getting whether user is entitled to this app
        /// </summary>
        /// <returns>The ID of this request</returns>
        public static int YVR_Permission_GetViewerEntitled()
        {
            return permissionJavaClass.CallStatic<int>("getViewerEntitled");
        }

        internal static bool YVR_Permission_IsViewerEntitled(AndroidJavaObject obj)
        {
            return permissionJavaClass.CallStatic<bool>("isViewerEntitled", obj);
        }
        #endregion

        #region Request
        internal static AndroidJavaObject YVR_PopMessage()
        {
            return requestJavaClass.CallStatic<AndroidJavaObject>("yvr_PopMessage");
        }

        internal static int YVR_Message_GetRequestID(AndroidJavaObject obj)
        {
            return requestJavaClass.CallStatic<int>("yvr_getRequestId", obj);
        }

        internal static string YVR_Message_GetRequestType(AndroidJavaObject obj)
        {
            return requestJavaClass.CallStatic<string>("yvr_getRequestType", obj);
        }

        internal static bool YVR_Message_IsError(AndroidJavaObject obj)
        {
            return requestJavaClass.CallStatic<bool>("yvr_isError", obj);
        }

        internal static void YVR_Message_FreeRequest(int requestID)
        {
            requestJavaClass.CallStatic("yvr_freeRequest", requestID);
        }
        #endregion

        #region Error
        internal static string YVR_Error_GetErrorMessage(AndroidJavaObject obj)
        {
            return requestJavaClass.CallStatic<string>("yvr_getErrorMsg", obj);
        }

        internal static int YVR_Error_GetErrorCode(AndroidJavaObject obj)
        {
            return requestJavaClass.CallStatic<int>("yvr_getErrorCode", obj);
        }
        #endregion

        #region Account
        internal static int YVR_Account_GetLoggedInUser()
        {
            return accountManagerJavaClass.CallStatic<int>("yvr_user_GetLoggedInUser");
        }

        internal static int YVR_Account_GetAccountID(AndroidJavaObject obj)
        {
            return accountManagerJavaClass.CallStatic<int>("getYvrAccountId", obj);
        }

        internal static string YVR_Account_GetUserName(AndroidJavaObject obj)
        {
            return accountManagerJavaClass.CallStatic<string>("getYvrUserName", obj);
        }

        internal static string YVR_Account_GetUserIcon(AndroidJavaObject obj)
        {
            return accountManagerJavaClass.CallStatic<string>("getYvrUserIcon", obj);
        }

        internal static int YVR_Account_GetUserSex(AndroidJavaObject obj)
        {
            return accountManagerJavaClass.CallStatic<int>("getYvrUserSex", obj);
        }
        #endregion

        #region Achievement
        internal static int YVR_AchievementUpdate_AddCount(string achievementName, long count)
        {
            return achievementJavaClass.CallStatic<int>("addCount", achievementName, count);
        }

        internal static int YVR_AchievementUpdate_AddFields(string achievementName, string fields)
        {
            return achievementJavaClass.CallStatic<int>("addFields", achievementName, fields);
        }

        internal static int YVR_AchievementUpdate_UnlockAchievement(string achievementName)
        {
            return achievementJavaClass.CallStatic<int>("unlockAchievement", achievementName);
        }

        internal static int YVR_AchievementDefinition_GetAllDefinitions()
        {
            return achievementJavaClass.CallStatic<int>("getAllDefinitions");
        }

        internal static int YVR_AchievementDefinition_GetDefinitionByName(string[] names)
        {
            return achievementJavaClass.CallStatic<int>("getDefinitionByNames", names.javaArrayFromCS());
        }

        internal static int YVR_AchievementDefinition_GetSizeOfAllDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<int>("getSizeOfAllDefinitions", obj);
        }

        internal static AndroidJavaObject YVR_AchievementDefinition_GetElementOfDefinitions(AndroidJavaObject obj, int index)
        {
            return achievementJavaClass.CallStatic<AndroidJavaObject>("getElementOfDefinitions", obj, index);
        }

        internal static int YVR_AchievementDefinition_GetIdFromElementOfDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<int>("getIdFromElementOfDefinitions", obj);
        }

        internal static string YVR_AchievementDefinition_GetApiNameFromElementOfDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<string>("getApiNameFromElementOfDefinitions", obj);
        }

        internal static int YVR_AchievementDefinition_GetTypeFromElementOfDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<int>("getAchievementTypeFromElementOfDefinitions", obj);
        }

        internal static int YVR_AchievementDefinition_GetPolicyFromElementOfDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<int>("getPolicyFromElementOfDefinitions", obj);
        }

        internal static int YVR_AchievementDefinition_GetTargetFromElementOfDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<int>("getTargetFromElementOfDefinitions", obj);
        }

        internal static int YVR_AchievementDefinition_GetBitfieldLengthFromElementOfDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<int>("getBitfieldLengthFromElementOfDefinitions", obj);
        }

        internal static bool YVR_AchievementDefinition_GetIsAchievedFromElementOfDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<bool>("getIsAchievedFromElementOfDefinitions", obj);
        }

        internal static string YVR_AchievementDefinition_GetTitleFromElementOfDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<string>("getTitleFromElementOfDefinitions", obj);
        }

        internal static string YVR_AchievementDefinition_GetDescriptionFromElementOfDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<string>("getDescriptionFromElementOfDefinitions", obj);
        }

        internal static string YVR_AchievementDefinition_GetUnlockedDescriptionFromElementOfDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<string>("getUnlockedDescriptionFromElementOfDefinitions", obj);
        }

        internal static bool YVR_AchievementDefinition_GetIsSecretFromElementOfDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<bool>("getIsSecretFromElementOfDefinitions", obj);
        }

        internal static string YVR_AchievementDefinition_GetLockedImageFromElementOfDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<string>("getLockedImageFromElementOfDefinitions", obj);
        }

        internal static string YVR_AchievementDefinition_GetUnlockedImageFromElementOfDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<string>("getUnlockedImageFromElementOfDefinitions", obj);
        }

        internal static long YVR_AchievementDefinition_GetCreatedTimeFromElementOfDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<long>("getCreatedTimeFromElementOfDefinitions", obj);
        }

        internal static long YVR_AchievementDefinition_GetUpdateTimeFromElementOfDefinitions(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<long>("getUpdateTimeFromElementOfDefinitions", obj);
        }

        internal static int YVR_AchievementProgress_GetAllProgress()
        {
            return achievementJavaClass.CallStatic<int>("GetAllProgress");
        }

        internal static int YVR_AchievementProgress_GetProgressByName(string[] names)
        {
            return achievementJavaClass.CallStatic<int>("GetProgressByName", names.javaArrayFromCS());
        }

        internal static int YVR_AchievementProgress_GetSizeOfAchievementProgress(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<int>("getSizeOfAchievementProgress", obj);
        }

        internal static AndroidJavaObject YVR_AchievementProgress_GetElementOfAchievementProgress(AndroidJavaObject obj, int index)
        {
            return achievementJavaClass.CallStatic<AndroidJavaObject>("getElementOfAchievementProgress", obj, index);
        }

        internal static int YVR_AchievementProgress_GetIdFromElementOfAchievementProgress(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<int>("getIdFromElementOfAchievementProgress", obj);
        }

        internal static AndroidJavaObject YVR_AchievementProgress_GetDefinitionFromElementOfAchievementProgress(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<AndroidJavaObject>("getDefinitionFromElementOfAchievementProgress", obj);
        }

        internal static string YVR_AchievementProgress_GetNameFromDefinitionOfAchievementProgress(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<string>("getNameFromDefinitionOfAchievementProgress", obj);
        }

        internal static int YVR_AchievementProgress_GetTargetFromDefinitionOfAchievementProgress(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<int>("getTargetFromDefinitionOfAchievementProgress", obj);
        }

        internal static int YVR_AchievementProgress_GetCountProgressFromElementOfAchievementProgress(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<int>("getCountProgressFromElementOfAchievementProgress", obj);
        }

        internal static string YVR_AchievementProgress_GetBitfieldProgressFromElementOfAchievementProgress(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<string>("getBitfieldProgressFromElementOfAchievementProgress", obj);
        }

        internal static bool YVR_AchievementProgress_IsUnlockedFromElementOfAchievementProgress(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<bool>("isUnlockedFromElementOfAchievementProgress", obj);
        }

        internal static long YVR_AchievementProgress_GetUnlockTimeFromElementOfAchievementProgress(AndroidJavaObject obj)
        {
            return achievementJavaClass.CallStatic<long>("getUnlockTimeFromElementOfAchievementProgress", obj);
        }
        #endregion

        #region DeepLink
        internal static bool YVR_Deeplink_IsDeeplinkLaunch()
        {
            return deeplinkJavaClass.CallStatic<bool>("isDeeplinkLaunch");
        }

        internal static string YVR_Deeplink_GetDeeplinkRoomId()
        {
            return deeplinkJavaClass.CallStatic<string>("getDeeplinkRoomId");
        }

        internal static string YVR_Deeplink_GetDeeplinkApiName()
        {
            return deeplinkJavaClass.CallStatic<string>("getDeeplinkApiName");
        }
        #endregion

        #region Friends
        internal static int YVR_Friends_GetYvrFriends()
        {
            return friendsJavaClass.CallStatic<int>("getYvrFriends");
        }

        internal static int YVR_Friends_GetSizeOfFriends(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<int>("getFriendsSize", obj);
        }

        internal static AndroidJavaObject YVR_Friends_GetElementOfFriends(AndroidJavaObject obj, int index)
        {
            return friendsJavaClass.CallStatic<AndroidJavaObject>("getItemOfFriendsList", obj, index);
        }

        internal static int YVR_Friends_GetActIdOfFriendItem(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<int>("getActIdOfFriendItem", obj);
        }

        internal static string YVR_Friends_GetNickOfFriendItem(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<string>("getNickOfFriendItem", obj);
        }

        internal static int YVR_Friends_GetAgeOfFriendItem(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<int>("getAgeOfFriendItem", obj);
        }

        internal static int YVR_Friends_GetSexOfFriendItem(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<int>("getSexOfFriendItem", obj);
        }

        internal static string YVR_Friends_GetIconOfFriendItem(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<string>("getIconOfFriendItem", obj);
        }

        internal static int YVR_Friends_GetOnlineOfFriendItem(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<int>("getOnlineOfFriendItem", obj);
        }

        internal static AndroidJavaObject YVR_Friends_GetUsingAppOfFriendItem(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<AndroidJavaObject>("getUsingAppOfFriendItem", obj);
        }

        internal static string YVR_Friends_GetScoverOfUsingApp(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<string>("getScoverOfUsingApp", obj);
        }

        internal static int YVR_Friends_GetTypeOfUsingApp(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<int>("getTypeOfUsingApp", obj);
        }

        internal static string YVR_Friends_GetPkgOfUsingApp(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<string>("getPkgOfUsingApp", obj);
        }

        internal static string YVR_Friends_GetNameOfUsingApp(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<string>("getNameOfUsingApp", obj);
        }

        internal static int YVR_Friends_GetYvrFriendInfo(int accountID)
        {
            return friendsJavaClass.CallStatic<int>("getYvrFriendInfo", accountID);
        }

        internal static int YVR_Friends_GetActIdOfUser(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<int>("getActIdOfUser", obj);
        }

        internal static string YVR_Friends_GetNickOfUser(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<string>("getNickOfUser", obj);
        }

        internal static int YVR_Friends_GetAgeOfUser(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<int>("getAgeOfUser", obj);
        }

        internal static int YVR_Friends_GetSexOfUser(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<int>("getSexOfUser", obj);
        }

        internal static string YVR_Friends_GetIconOfUser(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<string>("getIconOfUser", obj);
        }

        internal static int YVR_Friends_GetOnlineOfUser(AndroidJavaObject obj)
        {
            return friendsJavaClass.CallStatic<int>("getOnlineOfUser", obj);
        }
        #endregion
    }
}
