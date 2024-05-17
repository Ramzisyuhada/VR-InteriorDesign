using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate properties of friend info
    /// </summary>
    public class FriendInfo
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

        public FriendInfo(AndroidJavaObject obj)
        {
            this.accountID = YVRPlatform.YVR_Friends_GetActIdOfUser(obj);
            this.userName = YVRPlatform.YVR_Friends_GetNickOfUser(obj);
            this.userIcon = YVRPlatform.YVR_Friends_GetIconOfUser(obj);
            this.userSex = YVRPlatform.YVR_Friends_GetSexOfUser(obj);
            this.age = YVRPlatform.YVR_Friends_GetAgeOfUser(obj);
            this.onlineState = YVRPlatform.YVR_Friends_GetOnlineOfUser(obj);
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
            return str.ToString();
        }
    }
}