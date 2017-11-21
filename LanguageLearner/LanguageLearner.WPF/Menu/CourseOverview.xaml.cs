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
        public CourseOverview()
        {
            InitializeComponent();
            this.DataContext = this;

            initCourses();
        }

        private void initCourses()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:26100/");
                
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

        public List<String> courseList
        {
            get { return (List<String>)GetValue(courseListProperty); }
            set { SetValue(courseListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty courseListProperty =
            DependencyProperty.Register("MyProperty", typeof(List<String>), typeof(CourseOverview), new PropertyMetadata(new List<String>()));



        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
