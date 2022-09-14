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
    /// Interaction logic for WyniksControl.xaml
    /// </summary>
    public partial class WyniksControl : Window
    {



        public WyniksControl()
        {
            InitializeComponent();

            SklepDatabaseEntities db = new SklepDatabaseEntities();
            var wyniks = from d in db.Wyniks
                         select new
                           {
                             Id = d.Id,
                               Id_Sklepu = d.Id_Sklepu,
                               Budżet__na_ten_miesiąc= d.Budżet_na_ten_miesiąc,
                               Zrealizowano = d.Zrealizowano,
                               Wynik_budżetu = d.Wynik_budżetu,
                               Straty_rzeczywiste = d.Straty_rzeczywiste,
                               Straty_oczekiwane=d.Straty_oczekiwane,
                               Wynik_strat = d.Wynik_strat
                           };
            foreach (var item in wyniks)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Id_Sklepu);
                Console.WriteLine(item.Budżet__na_ten_miesiąc);
                Console.WriteLine(item.Zrealizowano);
                Console.WriteLine(item.Wynik_budżetu);
                Console.WriteLine(item.Straty_oczekiwane);
                Console.WriteLine(item.Straty_rzeczywiste);
                Console.WriteLine(item.Wynik_strat);

            }
            this.gridWyniks.ItemsSource = wyniks.ToList();
        }

        private int updatingWynikID = 0;
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridWyniks.SelectedIndex >= 0)
            {
                if (this.gridWyniks.SelectedItems.Count >= 0)
                {
                    if (this.gridWyniks.SelectedItems[0].GetType() == typeof(Wynik))
                    {
                        Wynik d = (Wynik)this.gridWyniks.SelectedItems[0];
                        this.box_idSklepu.Text = Convert.ToString(d.Id_Sklepu);
                        this.box_budzetNamies.Text = Convert.ToString(d.Budżet_na_ten_miesiąc);
                        this.box_BudzZreal.Text = Convert.ToString(d.Zrealizowano);
                        this.box_WynikBudz.Text = Convert.ToString(d.Wynik_budżetu);
                        this.box_StratyOcze.Text = Convert.ToString(d.Straty_oczekiwane);
                        this.box_StratyRzecz.Text = Convert.ToString(d.Straty_rzeczywiste);
                        this.box_WynikStrat.Text = Convert.ToString(d.Wynik_strat);


                    }
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SklepDatabaseEntities db = new SklepDatabaseEntities();
            
                Wynik WynikObject = new Wynik()
                {
                    Id_Sklepu = Convert.ToInt32(box_idSklepu.Text),
                    Budżet_na_ten_miesiąc = Convert.ToDouble(box_budzetNamies.Text),
                    Zrealizowano = Convert.ToDouble(box_BudzZreal.Text),
                    Wynik_budżetu = Convert.ToDouble(box_WynikBudz.Text),
                    Straty_oczekiwane = Convert.ToDouble(box_StratyOcze.Text),
                    Straty_rzeczywiste = Convert.ToDouble(box_StratyRzecz.Text),
                    Wynik_strat = Convert.ToDouble(box_WynikStrat.Text),

                };
                db.Wyniks.Add(WynikObject);
                db.SaveChanges();
                this.gridWyniks.ItemsSource = db.Wyniks.ToList();
          
                 
        }


        private void btnLoadData_Click(object sender, RoutedEventArgs e)
        {
            SklepDatabaseEntities db = new SklepDatabaseEntities();
            this.gridWyniks.ItemsSource = db.Wyniks.ToList();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            SklepDatabaseEntities db = new SklepDatabaseEntities();

            var r = from d in db.Wyniks
                    where d.Id == this.updatingWynikID
                    select d;
            Wynik obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.Id_Sklepu = Convert.ToInt32(this.box_idSklepu.Text);
                obj.Budżet_na_ten_miesiąc= Convert.ToDouble(this.box_budzetNamies.Text);
                obj.Zrealizowano= Convert.ToDouble(this.box_BudzZreal.Text);
                obj.Wynik_budżetu= Convert.ToDouble(this.box_WynikBudz.Text);
                obj.Straty_oczekiwane = Convert.ToDouble(this.box_StratyOcze.Text);
                obj.Straty_rzeczywiste = Convert.ToDouble(this.box_StratyRzecz.Text);
                obj.Wynik_strat = Convert.ToDouble(this.box_WynikStrat.Text);


                db.SaveChanges();
                this.gridWyniks.ItemsSource = db.Wyniks.ToList();
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Czy na pewno chcesz usunąć?",
                "Usunąć Wynik ",
                 MessageBoxButton.YesNo,
                 MessageBoxImage.Warning,
                 MessageBoxResult.No);
            if (msgBoxResult == MessageBoxResult.Yes)
            {
                SklepDatabaseEntities db = new SklepDatabaseEntities();

                var r = from d in db.Wyniks
                        where d.Id == this.updatingWynikID
                        select d;
                Wynik obj = r.SingleOrDefault();
                if (obj != null)
                {
                    db.Wyniks.Remove(obj);
                    db.SaveChanges();
                    this.gridWyniks.ItemsSource = db.Wyniks.ToList();
                }

            }
        }

        private void box_Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
