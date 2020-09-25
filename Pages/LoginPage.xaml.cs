using JudgmentTool.Widgets;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    { 
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            AuthData data = new AuthData()
            {
                user = txtLogin.Text,
                password = txtPass.Password
            };
            var response = await ApiHelper.RequestInternalJson<AuthResponse>("api/v1/login", data, null);
            if(response.status == "success")
            {
                AppPersistent.Token = new AuthTokens()
                {
                    UserId = response.data.userId,
                    Token = response.data.authToken
                };
                PageNavigationManager.SwitchToPage(new RoomsPage());
            }
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            await FindUsers(txtUsername.Text);
        }

        async Task FindUsers(string username)
        {
            var data = new { name = username };
            var res = await ApiHelper.RequestInternalJson<FindUsersResponse>("users/getByName", data, null, ApiHelper.ERequestType.POST, "http://dev.apianon.ru:3000/");
            if(res.error)
            {
                return;
            }

            lstUsers.Items.Clear();
            foreach(var profile in res.data)
            {
                var userWidget = new AnonimProfileWidget()
                {
                    ProfileData = profile
                };
                lstUsers.Items.Add(userWidget);
            }

        }

        private async void LoginAs_Click(object sender, RoutedEventArgs e)
        {
            await Utils.LoginAs((lstUsers.SelectedItem as AnonimProfileWidget).ProfileData);
        }
    }
}
