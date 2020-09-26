using JudgmentTool.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for ModeratorsPage.xaml
    /// </summary>
    public partial class ModeratorsPage : UserControl
    {
        public string RoomID { get; set; }
        public ERoomType RoomType { get; set; }
        public ModeratorsPage()
        {
            InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            string request = "";
            switch(RoomType)
            {
                case ERoomType.Channel:
                    request = "api/v1/channels.roles";
                    break;
                case ERoomType.Group:
                    request = "api/v1/groups.roles";
                    break;
            }
            var resp = await ApiHelper.GetRoomInfoRequest<RolesResponse>(request, RoomID, AppPersistent.Token);
            if(resp.success)
            {
                foreach(var user in resp.roles)
                {
                    var userWidget = new ModeratorUserWidget()
                    {
                        UserData = user.u,
                        Role = user.roles[0]
                    };
                    lstUsers.Items.Add(userWidget);
                }
            }
        }

        private async void LoginAs_Click(object sender, RoutedEventArgs e)
        {
            var userWidget = (ModeratorUserWidget)lstUsers.SelectedItem;
            if (userWidget != null)
            {
                AnonymProfileData data = new AnonymProfileData()
                {
                    id = userWidget.UserData.customFields.anonym_id,
                    login = userWidget.UserData.username
                };
                await Utils.LoginAs(data);
            }

        }
    }
}
