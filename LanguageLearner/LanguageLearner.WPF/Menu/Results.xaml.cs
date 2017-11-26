using LanguageLearner.BLL.Models;
using LanguageLearner.WPF.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Results : UserControl, ISwitchable
    {

        #region Constructor
        public Results()
        {
            InitializeComponent();

            this.DataContext = this;

            InitUserResults();
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region API access
        private void InitUserResults()
        {

            List<UsersCourseResultModel> currenResults = new List<UsersCourseResultModel>();

            using (var client = HttpHelpers.InitializeHttpClient())
            {
                HttpResponseMessage response = client.GetAsync("api/results").Result;
                if (HttpHelpers.IsSuccessfullRequest(response, "Váratlan hiba történt, a kuzusok lekérdezése nem sikerült. Kérjük, próbálja később!"))
                {
                    currenResults = response.Content.ReadAsAsync<IEnumerable<UsersCourseResultModel>>().Result.ToList();
                }
            }

            ICollectionView ResultsList = CollectionViewSource.GetDefaultView(currenResults);
            ResultsList.GroupDescriptions.Add(new PropertyGroupDescription("FullName"));
            ResultsDataGrid.DataContext = ResultsList;
        }
        #endregion
        
        #region Menu changes
        private void CoursesMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new CourseOverview());
        }

        private void ProfileMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Results());
        }

        private void LogoutMenu_Click(object sender, RoutedEventArgs e)
        {
            HttpHelpers.Username = "";
            HttpHelpers.Password = "";

            Switcher.Switch(new Login());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        #endregion
    }

}
