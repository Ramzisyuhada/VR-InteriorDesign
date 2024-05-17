using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate properties of friend
    /// </summary>
    public class Friend
    {
        /// <summary>
        /// Friend account ID
        /// </summary>
        public readonly int accountID;
        /// <summary>
        /// Friend nick name
        /// </summary>
        public readonly string userName;
        /// <summary>
        /// Friend icon url
        /// </summary>
        public readonly string userIcon;
        /// <summary>
        /// Friend sex, 1:man, 2:women, 3:unknow
        /// </summary>
        public readonly int userSex;
        /// <summary>
        /// Friend age
        /// </summary>
        public readonly int age;
        /// <summary>
        /// Friend online state, 1:online, 2:offline
        /// </summary>
        public readonly int onlineState;

        private readonly AndroidJavaObject usingAppInfo;
        /// <summary>
        /// App url of which friend is using. This represents that friend is not using any app if is null or empty.
        /// </summary>
        public readonly string usingAppUrl;
        /// <summary>
        /// App package name of which friend is using. This represents that friend is not using any app if is null or empty.
        /// </summary>
        public readonly string usingAppPackagename;
        /// <summary>
        /// App type of which friend is using, 1:game, 2:application. This represents that friend is not using any app if zero.
        /// </summary>
        public readonly int usingAppType = 0;
        /// <summary>
        /// App name of which friend is using. This represents that friend is not using any app if is null or empty.
        /// </summary>
        public readonly string usingAppName;

        public Friend(AndroidJavaObject obj)
        {
            this.accountID = YVRPlatform.YVR_Friends_GetActIdOfFriendItem(obj);
            this.userName = YVRPlatform.YVR_Friends_GetNickOfFriendItem(obj);
            this.userIcon = YVRPlatform.YVR_Friends_GetIconOfFriendItem(obj);
            this.userSex = YVRPlatform.YVR_Friends_GetSexOfFriendItem(obj);
            this.age = YVRPlatform.YVR_Friends_GetAgeOfFriendItem(obj);
            this.onlineState = YVRPlatform.YVR_Friends_GetOnlineOfFriendItem(obj);
            this.usingAppInfo = YVRPlatform.YVR_Friends_GetUsingAppOfFriendItem(obj);
            if (usingAppInfo != null)
            {
                this.usingAppUrl = YVRPlatform.YVR_Friends_GetScoverOfUsingApp(usingAppInfo);
                this.usingAppPackagename = YVRPlatform.YVR_Friends_GetPkgOfUsingApp(usingAppInfo);
                this.usingAppType = YVRPlatform.YVR_Friends_GetTypeOfUsingApp(usingAppInfo);
                this.usingAppName = YVRPlatform.YVR_Friends_GetNameOfUsingApp(usingAppInfo);
            }
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(string.Format("accountID:¡¾{0}¡¿,\n\r", accountID));
            str.Append(string.Format("userName:¡¾{0}¡¿,\n\r", userName ?? "null"));
            str.Append(string.Format("userIcon:¡¾{0}¡¿,\n\r", userIcon ?? "null"));
            str.Append(string.Format("userSex:¡¾{0}¡¿,\n\r", userSex));
            str.Append(string.Format("age:¡¾{0}¡¿,\n\r", age));
            str.Append(string.Format("onlineState:¡¾{0}¡¿,\n\r", onlineState));
            str.Append(string.Format("usingAppUrl:¡¾{0}¡¿,\n\r", usingAppUrl ?? "null"));
            str.Append(string.Format("usingAppPackagename:¡¾{0}¡¿,\n\r", usingAppPackagename ?? "null"));
            str.Append(string.Format("usingAppType:¡¾{0}¡¿,\n\r", usingAppType));
            str.Append(string.Format("usingAppName:¡¾{0}¡¿,\n\r", usingAppName ?? "null"));
            return str.ToString();
        }
    }

    /// <summary>
    /// The storage of friend data
    /// </summary>
    public class FriendsList : DeserializableList<Friend>
    {
        public FriendsList(AndroidJavaObject obj)
        {
            int count = YVRPlatform.YVR_Friends_GetSizeOfFriends(obj);

            data = new List<Friend>(count);

            for (int i = 0; i < count; i++)
            {
                data.Add(new Friend(YVRPlatform.YVR_Friends_GetElementOfFriends(obj, i))); ;
            }
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            foreach (var item in data)
                str.Append(item.ToString() + "\n\r");
            return str.ToString();
        }
    }
}