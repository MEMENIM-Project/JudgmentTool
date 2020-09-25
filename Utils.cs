using JudgmentTool.Pages;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgmentTool
{
    static class Utils
    {
        public static ERoomType GetRoomTypeFromString(string type)
        {
            if (type == "d")
            {
                return ERoomType.Dialog;
            }
            if (type == "c")
            {
                return ERoomType.Channel;
            }
            if (type == "p")
            {
                return ERoomType.Group;
            }
            return ERoomType.Dialog;
        }

        public static PackIconModernKind GetIconKind(ERoomType type)
        {
            switch (type)
            {
                case ERoomType.Channel:
                    return PackIconModernKind.PeopleMultiple;
                case ERoomType.Dialog:
                    return PackIconModernKind.People;
                case ERoomType.Group:
                    return PackIconModernKind.Lock;
                default:
                    return PackIconModernKind.SmileyHappy;
            }
        }

        public static async Task LoginAs(AnonymProfileData data)
        {
            AuthData authData = new AuthData()
            {
                user = data.login,
                password = "p" + data.id.ToString()
            };
            var response = await ApiHelper.RequestInternalJson<AuthResponse>("api/v1/login", authData, null);
            if (response.status == "success")
            {
                AppPersistent.Token = new AuthTokens()
                {
                    UserId = response.data.userId,
                    Token = response.data.authToken
                };
            }
            PageNavigationManager.SwitchToPage(new RoomsPage());
        }

    }
}
