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

namespace projectSchool
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            HospitalManagmentDatabaseEntities db = new HospitalManagmentDatabaseEntities();
            var docs = from d in db.Doctors
                       select new
                       {
                           DoctorName = d.Name,
                           Speciality = d.Spectalization
                       };
            foreach (var item in docs)
            {
                Console.WriteLine(item.DoctorName);
                Console.WriteLine(item.Speciality);
            }
            this.gridDoctors.ItemsSource = docs.ToList();
        }
        private int updatingDoctorID = 0;
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridDoctors.SelectedIndex >= 0)
            { 
                if (this.gridDoctors.SelectedItems.Count >= 0)
                {
                    if (this.gridDoctors.SelectedItems[0].GetType() == typeof(Doctor))
                    {
                        Doctor d = (Doctor)this.gridDoctors.SelectedItems[0];
                        this.txtName2.Text = d.Name;
                        this.txtQualification2.Text = d.Qualification;
                        this.txtSpecialization2.Text = d.Spectalization;
                        this.updatingDoctorID = d.Id;
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagmentDatabaseEntities db = new HospitalManagmentDatabaseEntities();
            Doctor doctorObject = new Doctor()
            {
                Name = txtName.Text,
                Qualification = txtQualification.Text,
                Spectalization = txtSpecialization.Text
            };
            db.Doctors.Add(doctorObject);
            db.SaveChanges();
        }

        private void btnLoadDoctors_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagmentDatabaseEntities db = new HospitalManagmentDatabaseEntities();
            this.gridDoctors.ItemsSource = db.Doctors.ToList();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagmentDatabaseEntities db = new HospitalManagmentDatabaseEntities();

            var r = from d in db.Doctors
                    where d.Id == this.updatingDoctorID
                    select d;
            Doctor obj = r.SingleOrDefault();
            if(obj != null)
            {
                obj.Name = this.txtName2.Text;
                obj.Qualification = this.txtQualification2.Text;
                obj.Spectalization = this.txtSpecialization2.Text;
                db.SaveChanges();
            }
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
           MessageBoxResult msgBoxResult = MessageBox.Show("Are you sure you want to delete?", 
                "Delete Doctor",
                 MessageBoxButton.YesNo,
                 MessageBoxImage.Warning,
                 MessageBoxResult.No);
            if(msgBoxResult == MessageBoxResult.Yes)
            {
                HospitalManagmentDatabaseEntities db = new HospitalManagmentDatabaseEntities();

                var r = from d in db.Doctors
                        where d.Id == this.updatingDoctorID
                        select d;
                Doctor obj = r.SingleOrDefault();
                if (obj != null)
                {
                    db.Doctors.Remove(obj);
                    db.SaveChanges();
                }
            }
           
        }
    }
}
