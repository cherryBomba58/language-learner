using LanguageLearner.BLL.Models;
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
using System.Windows.Shapes;

namespace LanguageLearner.WPF.Menu
{
    /// <summary>
    /// Interaction logic for LearnableEditor.xaml
    /// </summary>
    public partial class LearnableEditor : Window
    {
        #region Constructor
        public LearnableEditor(List<CourseModel> courseList, CourseModel selectedCourse)
        {
            InitializeComponent();
            this.DataContext = this;

            Word = new LearnableModel();
            AvailableCourses = courseList.Select(c => new CourseModel
            {
                CourseID = c.CourseID,
                Name = c.Name
            }).ToList();
            if (selectedCourse == null)
                SelectedCourse = null;
            else
                SelectedCourse = AvailableCourses.Single(c => c.CourseID == selectedCourse.CourseID);
        }

        public LearnableEditor(List<CourseModel> courseList, LearnableModel word)
        {
            InitializeComponent();
            this.DataContext = this;

            Word = new LearnableModel {
                LearnableID = word.LearnableID,
                CourseID = word.CourseID,
                English = word.English,
                Hungarian = word.Hungarian,
                WordClass = word.WordClass,
                PictureUrl = word.PictureUrl
            };
            AvailableCourses = courseList.Select(c => new CourseModel {
                CourseID = c.CourseID,
                Name = c.Name }).ToList();
            SelectedCourse = AvailableCourses.Single(c => c.CourseID == Word.CourseID);
        }
        #endregion

        #region Properties
        public LearnableModel Word
        {
            get { return (LearnableModel)GetValue(WordProperty); }
            set { SetValue(WordProperty, value); }
        }

        public static readonly DependencyProperty WordProperty =
            DependencyProperty.Register("Word", typeof(LearnableModel), typeof(LearnableEditor), new PropertyMetadata(new LearnableModel()));

        public List<CourseModel> AvailableCourses
        {
            get { return (List<CourseModel>)GetValue(CourseListProperty); }
            set { SetValue(CourseListProperty, value); }
        }
        
        public static readonly DependencyProperty CourseListProperty =
            DependencyProperty.Register("AvailableCourses", typeof(List<CourseModel>), typeof(LearnableEditor), new PropertyMetadata(new List<CourseModel>()));
        


        public CourseModel SelectedCourse
        {
            get { return (CourseModel)GetValue(SelectedCourseProperty); }
            set { SetValue(SelectedCourseProperty, value); }
        }
        
        public static readonly DependencyProperty SelectedCourseProperty =
            DependencyProperty.Register("SelectedCourse", typeof(CourseModel), typeof(LearnableEditor), new PropertyMetadata(null));


        #endregion

        #region Buttons
        private void SaveWord_Click(object sender, RoutedEventArgs e)
        {
            Word.CourseID = SelectedCourse.CourseID;

            if (String.IsNullOrEmpty(Word.English) || String.IsNullOrEmpty(Word.English) || Word.CourseID == 0)
            {
                MessageBox.Show("Nem töltöttél ki minden szökséges mezőt." + System.Environment.NewLine +
                    "Ellenőrizd, hogy az angol és a magyar jelentés meg van adva, és választottál egy kategóriát!","Adatkitöltési hiba");
                return;
            }

            DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Word = null;
            this.Close();
        }
        #endregion
    }
}
