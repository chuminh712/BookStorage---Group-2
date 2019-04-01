using System;

namespace BookStorage.Common
{
    [Serializable]
    public class UserLogin
    {
        public int UserID { set; get; }
        public string Username { set; get; }
    }
}