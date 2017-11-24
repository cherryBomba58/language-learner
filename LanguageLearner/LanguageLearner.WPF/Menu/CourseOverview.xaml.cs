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

            initCourses();
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



        public List<String> courseList
        {
            get { return (List<String>)GetValue(courseListProperty); }
            set { SetValue(courseListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty courseListProperty =
            DependencyProperty.Register("MyProperty", typeof(List<String>), typeof(CourseOverview), new PropertyMetadata(new List<String>()));
        #endregion


        private void initCourses()
        {
            courseList = new List<string>();

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
                        courseList.Add(course.Name);
                    }
                }
            }
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:57696/");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                CourseModel newCourse = new CourseModel();
                newCourse.Name = NewCourseName;
                var content = new StringContent(newCourse.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("api/course",content).Result;
                if (response.IsSuccessStatusCode)
                {
                    initCourses();
                }
            }
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
