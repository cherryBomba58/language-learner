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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LanguageLearner.BLL.Models;

namespace LanguageLearner.WPF.Menu
{
    /// <summary>
    /// Interaction logic for CourseOverview.xaml
    /// </summary>
    public partial class CourseOverview : UserControl, ISwitchable
    {
        #region Constructor
        public CourseOverview()
        {
            InitializeComponent();
            this.DataContext = this;

            InitCourses();
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Properties
        public String NewCourseName
        {
            get { return (String)GetValue(NewCourseNameProperty); }
            set { SetValue(NewCourseNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewCourseName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewCourseNameProperty =
            DependencyProperty.Register("NewCourseName", typeof(String), typeof(CourseOverview), new PropertyMetadata(""));



        public List<CourseModel> CourseList
        {
            get { return (List<CourseModel>)GetValue(CourseListProperty); }
            set { SetValue(CourseListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CourseListProperty =
            DependencyProperty.Register("CourseList", typeof(List<CourseModel>), typeof(CourseOverview), new PropertyMetadata(new List<CourseModel>()));



        public CourseModel SelectedCourse
        {
            get { return (CourseModel)GetValue(SelectedCourseProperty); }
            set { SetValue(SelectedCourseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for selectedCourse.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedCourseProperty =
            DependencyProperty.Register("SelectedCourse", typeof(CourseModel), typeof(CourseOverview), new PropertyMetadata(null));


        #endregion


        #region API access
        private void InitCourses()
        {
            SelectedCourse = null;
            List<CourseModel> currentCourses = new List<CourseModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57696/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/course").Result;
                if (response.IsSuccessStatusCode)
                {
                    //Getting the result and mapping to a Product object
                    IEnumerable<CourseModel> courses = response.Content.ReadAsAsync<IEnumerable<CourseModel>>().Result;

                    foreach (var course in courses)
                    {
                        currentCourses.Add(course);
                    }
                }
            }

            CourseList = currentCourses;
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            if (!IsAvailableCourseName())
            {
                MessageBox.Show("Már van ilyen nevű kurzus.", "Kurzusfelvétel hiba");
                return;
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57696/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                CourseModel newCourse = new CourseModel();
                newCourse.Name = NewCourseName;
                var content = new StringContent(newCourse.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsJsonAsync("api/course", newCourse).Result;
                if (response.IsSuccessStatusCode)
                {
                    InitCourses();
                    NewCourseName = "";
                }
                else
                {
                    MessageBox.Show("Váratlan hiba történt, a kurzusfelvétel nem sikerült. Kérjük, próbálja később!", "Kurzusfelvétel hiba");
                }
            }
        }

        private void DeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCourse == null)
            {
                MessageBox.Show("Nincs egyetlen kurzus se kiválasztva.", "Kurzustörlés hiba");
                return;
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57696/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.DeleteAsync("api/course/" + SelectedCourse.CourseID).Result;
                if (response.IsSuccessStatusCode)
                {
                    InitCourses();
                }
                else
                {
                    MessageBox.Show("Váratlan hiba történt, a kurzus törlése nem sikerült. Kérjük, próbálja később!", "Kurzustörlés hiba");
                }
            }
        }

        #endregion

        private bool IsAvailableCourseName()
        {
            return !String.IsNullOrEmpty(NewCourseName) &&
                !CourseList.Select(c => c.Name).ToList().Contains(NewCourseName);
        }


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
            Switcher.Switch(new Login());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        #endregion
    }
}
