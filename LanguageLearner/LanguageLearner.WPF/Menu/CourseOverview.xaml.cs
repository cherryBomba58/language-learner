using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LanguageLearner.BLL.Models;
using LanguageLearner.WPF.Helpers;

namespace LanguageLearner.WPF.Menu
{
    /// <summary>
    /// Interaction logic for CourseOverview.xaml
    /// </summary>
    public partial class CourseOverview : System.Windows.Controls.UserControl, ISwitchable
    {
        #region Constructor
        public CourseOverview()
        {
            InitializeComponent();
            this.DataContext = this;

            InitCoursesList();
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Properties
        public CourseModel NewCourse
        {
            get { return (CourseModel)GetValue(NewCourseProperty); }
            set { SetValue(NewCourseProperty, value); }
        }
        
        public static readonly DependencyProperty NewCourseProperty =
            DependencyProperty.Register("NewCourse", typeof(CourseModel), typeof(CourseOverview), new PropertyMetadata(new CourseModel()));



        public List<CourseModel> CourseList
        {
            get { return (List<CourseModel>)GetValue(CourseListProperty); }
            set { SetValue(CourseListProperty, value); }
        }
        
        public static readonly DependencyProperty CourseListProperty =
            DependencyProperty.Register("CourseList", typeof(List<CourseModel>), typeof(CourseOverview), new PropertyMetadata(new List<CourseModel>()));



        public CourseModel SelectedCourse
        {
            get { return (CourseModel)GetValue(SelectedCourseProperty); }
            set { SetValue(SelectedCourseProperty, value); }
        }
        
        public static readonly DependencyProperty SelectedCourseProperty =
            DependencyProperty.Register("SelectedCourse", typeof(CourseModel), typeof(CourseOverview), new PropertyMetadata(null));



        public List<LearnableModel> Learnables
        {
            get { return (List<LearnableModel>)GetValue(LearnablesProperty); }
            set { SetValue(LearnablesProperty, value); }
        }
        
        public static readonly DependencyProperty LearnablesProperty =
            DependencyProperty.Register("Learnables", typeof(List<LearnableModel>), typeof(CourseOverview), new PropertyMetadata(new List<LearnableModel>()));



        public LearnableModel SelectedLearnable
        {
            get { return (LearnableModel)GetValue(SelectedLearnableProperty); }
            set { SetValue(SelectedLearnableProperty, value); }
        }
        
        public static readonly DependencyProperty SelectedLearnableProperty =
            DependencyProperty.Register("SelectedLearnable", typeof(LearnableModel), typeof(CourseOverview), new PropertyMetadata(null));
        
        #endregion

        #region API access
        private void InitCoursesList()
        {
            List<CourseModel> currentCourses = new List<CourseModel>();

            using (var client = HttpHelpers.InitializeHttpClient())
            {
                HttpResponseMessage response = client.GetAsync("api/course").Result;
                if (HttpHelpers.IsSuccessfullRequest(response, "Váratlan hiba történt, a kuzusok lekérdezése nem sikerült. Kérjük, próbálja később!"))
                {
                    currentCourses = response.Content.ReadAsAsync<IEnumerable<CourseModel>>().Result.ToList();
                }
            }

            int selectedCourseId = 0;
            if (SelectedCourse != null)
                selectedCourseId = SelectedCourse.CourseID;

            CourseList = currentCourses;
            
            if (selectedCourseId != 0)
                SelectedCourse = CourseList.First(c => c.CourseID == selectedCourseId);
        }

        private void InitLearnablesList()
        {
            if (SelectedCourse == null)
            {
                Learnables = new List<LearnableModel>();
                return;
            }

            List<LearnableModel> currentLearnables = new List<LearnableModel>();

            using (var client = HttpHelpers.InitializeHttpClient())
            {
                HttpResponseMessage response = client.GetAsync("api/learnable/" + SelectedCourse.CourseID).Result;
                if (HttpHelpers.IsSuccessfullRequest(response, "Váratlan hiba történt, a szavak lekérdezése nem sikerült. Kérjük, próbálja később!"))
                {
                    currentLearnables = response.Content.ReadAsAsync<IEnumerable<LearnableModel>>().Result.ToList();
                }
            }

            Learnables = currentLearnables;
        }

        private void SelectedCourse_Changed(object sender, SelectionChangedEventArgs e)
        {
            InitLearnablesList();
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAvailableCourseName())
            {
                System.Windows.Forms.MessageBox.Show("Már van ilyen nevű kurzus.", "Kurzusfelvétel hiba");
                return;
            }

            using (var client = HttpHelpers.InitializeHttpClient())
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/course", NewCourse).Result;
                if (HttpHelpers.IsSuccessfullRequest(response, "Váratlan hiba történt, a kurzusfelvétel nem sikerült. Kérjük, próbálja később!"))
                {
                    InitCoursesList();
                    NewCourse = new CourseModel();
                }
            }
        }

        private void DeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCourse == null)
            {
                System.Windows.Forms.MessageBox.Show("Nincs egyetlen kurzus se kiválasztva.", "Kurzustörlés hiba");
                return;
            }

            if (Learnables != null && Learnables.Count > 0)
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show(
                    "A kuzushoz tartozó szavak törölve lesznek. Biztos, hogy folytatja?", "Kurzus törlése", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                    return;
            }

            using (var client = HttpHelpers.InitializeHttpClient())
            {
                HttpResponseMessage response = client.DeleteAsync("api/course/" + SelectedCourse.CourseID).Result;
                if (HttpHelpers.IsSuccessfullRequest(response, "Váratlan hiba történt, a kurzus törlése nem sikerült. Kérjük, próbálja később!"))
                {
                    SelectedCourse = null;
                    InitCoursesList();
                    InitLearnablesList();
                }
            }
        }

        private void NewLearnable_Click(object sender, RoutedEventArgs e)
        {
            LearnableEditor dialog = new LearnableEditor(CourseList, SelectedCourse);
            if (dialog.ShowDialog().Value)
            {
                if (dialog.Word == null)
                    return;

                using (var client = HttpHelpers.InitializeHttpClient())
                {
                    HttpResponseMessage response = client.PostAsJsonAsync("api/learnable", dialog.Word).Result;
                    if (HttpHelpers.IsSuccessfullRequest(response, "Váratlan hiba történt, a szó felvétele nem sikerült. Kérjük, próbálja később!"))
                    {
                        InitLearnablesList();
                    }
                }
            }
        }

        private void EditLearnable_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLearnable == null)
            {
                System.Windows.Forms.MessageBox.Show("Nincs kiválasztva a szó, amit szerkeszteni szeretnél!", "Szószerkesztés hiba");
                return;
            }

            LearnableEditor dialog = new LearnableEditor(CourseList, SelectedLearnable);
            if (dialog.ShowDialog().Value)
            {
                if (dialog.Word == null)
                    return;

                using (var client = HttpHelpers.InitializeHttpClient())
                {
                    HttpResponseMessage response = client.PutAsJsonAsync("api/learnable/" + dialog.Word.LearnableID, dialog.Word).Result;
                    if (HttpHelpers.IsSuccessfullRequest(response, "Váratlan hiba történt, a szó módosítása nem sikerült. Kérjük, próbálja később!"))
                    {
                        InitLearnablesList();
                    }
                }
            }
        }

        private void DeleteLearnable_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLearnable == null)
            {
                System.Windows.Forms.MessageBox.Show("Nincs kiválasztva a szó, amit törölni szeretnél!", "Szótörlés hiba");
                return;
            }

            using (var client = HttpHelpers.InitializeHttpClient())
            {
                HttpResponseMessage response = client.DeleteAsync("api/learnable/" + SelectedLearnable.LearnableID).Result;
                if (HttpHelpers.IsSuccessfullRequest(response, "Váratlan hiba történt, a szó törlése nem sikerült. Kérjük, próbálja később!"))
                {
                    InitLearnablesList();
                }
            }
        }
        #endregion

        #region Helpers
        private bool IsAvailableCourseName()
        {
            return !String.IsNullOrEmpty(NewCourse.Name) &&
                !CourseList.Select(c => c.Name).ToList().Contains(NewCourse.Name);
        }
        #endregion

        #region Menu changes
        private void CoursesMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new CourseOverview());
        }

        private void ProfileMenu_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Profile());
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
