using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YVR.Platform
{
    /// <summary>
    /// Encapsulate properties of account data
    /// </summary>
    public class AccountData
    {
        /// <summary>
        /// User account ID
        /// </summary>
        public readonly int accountID;
        /// <summary>
        /// User name
        /// </summary>
        public readonly string userName;
        /// <summary>
        /// User icon url
        /// </summary>
        public readonly string userIcon;
        /// <summary>
        /// User sex, 1:man, 2:women, 3:unknow
        /// </summary>
        public readonly int userSex;

        public AccountData(AndroidJavaObject obj)
        {
            this.accountID = YVRPlatform.YVR_Account_GetAccountID(obj);
            this.userName = YVRPlatform.YVR_Account_GetUserName(obj);
            this.userIcon = YVRPlatform.YVR_Account_GetUserIcon(obj);
            this.userSex = YVRPlatform.YVR_Account_GetUserSex(obj);
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(string.Format("accountID:【{0}】,\n\r", accountID));
            str.Append(string.Format("userName:【{0}】,\n\r", userName ?? "null"));
            str.Append(string.Format("userIcon:【{0}】,\n\r", userIcon ?? "null"));
            str.Append(string.Format("userSex:【{0}】,\n\r", userSex));
            return str.ToString();
        }
    }
}
