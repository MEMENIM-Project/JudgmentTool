using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JudgmentTool
{

    public enum ERoomType
    {
        Dialog,
        Channel,
        Group
    }

    public class AuthTokens
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }

    public class AuthData
    {
        public string user { get; set; }
        public string password { get; set; }
    }


    public class AuthResponse
    {
        public string status { get; set; }
        public AuthStatus data { get; set; }
        public ProfileData me { get; set; }
    }

    public class AuthStatus
    {
        public string userId { get; set; }
        public string authToken { get; set; }
    }

    public class ProfileData
    {
        public string _id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
    }

    public class FindUsersResponse
    {
        public bool error { get; set; }
        public List<AnonymProfileData> data { get; set; }
    }

    public class AnonymProfileData    
    {
        public int id { get; set; }
        public string login { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
    }

    public class GetRoomResponse
    {
        public List<RoomData> update { get; set; }
    }

    public class RoomData
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string fname { get; set; }
        public string t { get; set; }
        public LastRoomMessage? lastMessage { get; set; }
    }

    public class LastRoomMessage
    {
        public string msg { get; set; }
    }

    public class CustomFields
    {
        public int anonym_id { get; set; }
        public string photoUrl { get; set; }
    }

    public class UserInfo
    {
        public string _id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public CustomFields customFields { get; set; }
    }

    public class MessageData
    {
        public string _id { get; set; }
        public string rid { get; set; }
        public string msg { get; set; }
        public UserInfo u { get; set; }
    }

    public class MessagesResponce
    {
        public List<MessageData> messages { get; set; }
        public bool success { get; set; }
    }
    
}
