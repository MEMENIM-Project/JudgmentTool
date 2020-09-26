using JudgmentTool.Widgets;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JudgmentTool.Pages
{
    /// <summary>
    /// Interaction logic for RoomsPage.xaml
    /// </summary>
    public partial class RoomsPage : UserControl
    {
        public RoomsPage()
        {
            InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateRooms();
        }


        private async void lstRooms_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                var type = (item as RoomSelectorWidget).RoomType;
                string request = "";
                switch (type)
                {
                    case ERoomType.Dialog:
                        request = "api/v1/im.messages";
                        break;
                    case ERoomType.Channel:
                        request = "api/v1/channels.history";
                        break;
                    case ERoomType.Group:
                        request = "api/v1/groups.history";
                        break;
                }
                await UpdateMessages(request, (item as RoomSelectorWidget).RoomID);
            }
        }

        async Task UpdateRooms()
        {
            var roomsData = await RoomsHelper.GetRooms();
            if (roomsData == null || roomsData.Count == 0) { return; }
            lstRooms.Items.Clear();
            foreach (var room in roomsData)
            {
                var roomStatWidget = new RoomSelectorWidget()
                {
                    Title = room.fname,
                    Message = room.lastMessage?.msg,
                    RoomID = room._id,
                    RoomType = Utils.GetRoomTypeFromString(room.t),
                    Kind = Utils.GetIconKind(Utils.GetRoomTypeFromString(room.t))
                };
                lstRooms.Items.Add(roomStatWidget);
            }
        }

        async Task UpdateMessages(string request, string roomId)
        {
            var messagesResponse = await ApiHelper.GetRoomInfoRequest<MessagesResponce>(request, roomId, AppPersistent.Token);
            if (!messagesResponse.success)
            {
                return;
            }
            lstMessages.Items.Clear();
            for (int i = messagesResponse.messages.Count - 1; i > -1; --i)
            {
                var messageWidget = new MessageWidget()
                {
                    Message = messagesResponse.messages[i]
                };
                lstMessages.Items.Add(messageWidget);
            }
        }

        private async void LoginAs_Click(object sender, RoutedEventArgs e)
        {
            var messageWidget = (MessageWidget)lstMessages.SelectedItem;
            if(messageWidget != null)
            {
                AnonymProfileData data = new AnonymProfileData()
                {
                    id = messageWidget.Message.u.customFields.anonym_id,
                    login = messageWidget.Message.u.username
                };
                await Utils.LoginAs(data);
            }
        }

        private async void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            var roomWidget = (RoomSelectorWidget)lstRooms.SelectedItem;
            if(roomWidget != null)
            {
                await RoomsHelper.DeleteRoom(roomWidget.RoomID, roomWidget.RoomType);
                await UpdateRooms();
            }
        }

        private void GetModers_Click(object sender, RoutedEventArgs e)
        {
            var roomWidget = (RoomSelectorWidget)lstRooms.SelectedItem;
            if (roomWidget != null)
            {
                if(roomWidget.RoomType != ERoomType.Dialog)
                {
                    PageNavigationManager.SwitchToPage(new ModeratorsPage() { RoomID = roomWidget.RoomID, RoomType = roomWidget.RoomType });
                }    
                else
                {
                    MessageBox.Show("Not allowed");
                }
            }
        }
    }
}
