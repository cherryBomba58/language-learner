using LanguageLearner.BLL.Models;
using LanguageLearner.WPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace LanguageLearner.WPF.Menu
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl, ISwitchable
    {

        #region Constructor
        public Login()
        {
            InitializeComponent();
            
            this.DataContext = this;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Properties
        public String Username
        {
            get { return (String)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }
        
        public static readonly DependencyProperty UsernameProperty =
            DependencyProperty.Register("Username", typeof(String), typeof(Login), new PropertyMetadata(""));
        #endregion

        #region Menu changes
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            HttpHelpers.Username = Username;
            HttpHelpers.Password = Password.Password;

            //Check account rights
            using (var client = HttpHelpers.InitializeHttpClient())
            {
                HttpResponseMessage response = client.GetAsync("api/results").Result;
                if (HttpHelpers.IsSuccessfullRequest(response, "Váratlan hiba történt, a belépés nem sikerült. Kérjük, próbálja később!"))
                {
                    try
                    {
                        response.Content.ReadAsAsync<IEnumerable<UsersCourseResultModel>>().Result.ToList();
                    } catch (Exception)
                    {
                        MessageBox.Show("A bejelentkezés nem sikerült. A megadott adatokhoz nem tartozik egyetlen tanári jogosultságú felhasználó se.","Bejelentkezési hiba");
                        return;
                    }
                }
            }

            Switcher.Switch(new CourseOverview());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        #endregion
    }
}
