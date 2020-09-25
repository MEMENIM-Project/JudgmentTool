using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgmentTool
{
    static class RoomsHelper
    {
        public static async Task<List<RoomData>> GetRooms()
        {
            var result = await ApiHelper.RequestInternalJson<GetRoomResponse>("api/v1/rooms.get", null, AppPersistent.Token, ApiHelper.ERequestType.GET);
            return result.update;
        }

        public static async Task<object> DeleteRoom(string roomID, ERoomType type)
        {
            string request = "";
            switch(type)
            {
                case ERoomType.Channel:
                    request = "api/v1/channels.delete";
                    break;
                case ERoomType.Group:
                    request = "/api/v1/groups.delete";
                    break;
                case ERoomType.Dialog:
                    request = "/api/v1/im.close";
                    break;
            }

            var data = new { roomId = roomID };

            var res = await ApiHelper.RequestInternalJson<object>(request, data, AppPersistent.Token);
            return res;
        }
    }
}
