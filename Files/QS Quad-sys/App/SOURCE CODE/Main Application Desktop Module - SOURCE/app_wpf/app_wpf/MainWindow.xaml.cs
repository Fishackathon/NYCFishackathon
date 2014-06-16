using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace app_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LocationConverter locConverter = new LocationConverter();
        Pushpin pin1 = new Pushpin();
        Pushpin pin2 = new Pushpin();
        Pushpin pin3 = new Pushpin();
        Pushpin pin4 = new Pushpin();
        Pushpin pin5 = new Pushpin();
        Pushpin qcpin = new Pushpin();
        DataTable myDataTable = new DataTable();
        List<Location> path = new List<Location>();
        Boolean p1 = false, p2 = false, p3 = false, p4 = false;
        MapPolygon area = new MapPolygon();
        int valueRead = 0;
        public MainWindow()
        {
            InitializeComponent();
            myDataTable.Columns.Add("Position");
        }

        private void updateSquare()
        {
            if(p1&&p2&&p3&&p4)
            {
                try
                {
                    map.Children.Remove(area);
                }
                catch(Exception exp)
                {
                }
                area.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
                area.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
                area.StrokeThickness = 5;
                area.Opacity = 0.2;
                area.Locations = new LocationCollection() { 
                    pin1.Location,
                    pin2.Location,
                    pin4.Location,
                    pin3.Location};
                map.Children.Add(area);
                Location pt1, pt2, pt3, pt4;
                pt1 = pin1.Location;
                pt2 = pin2.Location;
                pt3 = pin3.Location;
                pt4 = pin4.Location;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            valueRead = 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            valueRead = 2;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            valueRead = 3;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            valueRead = 4;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            valueRead = 5;
        }

        private void map_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Point mousePosition = e.GetPosition(this);
            Location pinLocation = map.ViewportPointToLocation(mousePosition);
            Pushpin temp = new Pushpin();
            switch(valueRead)
            {
                case 0:
                    break;
                case 1:
                   temp.Location = pinLocation;
                   map.Children.Add(temp);
                   try
                   {
                       map.Children.Remove(pin1);
                   }
                    catch(Exception exp)
                   { }
                   pin1 = temp;
                   cordinatesA.Text = pin1.Location.ToString();
                   p1 = true;
                   break;
                case 2:
                   temp.Location = pinLocation;
                   map.Children.Add(temp);
                   try
                   {
                       map.Children.Remove(pin2);
                   }
                   catch (Exception exp)
                   { }
                   pin2 = temp;
                   cordinatesB.Text = pin2.Location.ToString();
                   p2 = true;
                   break;
                case 3:
                   temp.Location = pinLocation;
                   map.Children.Add(temp);
                   try
                   {
                       map.Children.Remove(pin3);
                   }
                   catch (Exception exp)
                   { }
                   pin3 = temp;
                   cordinatesC.Text = pin3.Location.ToString();
                   p3 = true;
                   break;
                case 4:
                   temp.Location = pinLocation;
                   map.Children.Add(temp);
                   try
                   {
                       map.Children.Remove(pin4);
                   }
                   catch (Exception exp)
                   { }
                   p4=true;
                   pin4 = temp;
                   cordinatesD.Text = pin4.Location.ToString();
                   break;
                case 5:
                   temp.Location = pinLocation;
                   map.Children.Add(temp);
                   try
                   {
                       map.Children.Remove(pin5);
                   }
                   catch (Exception exp)
                   { }
                   pin5 = temp;
                   cordinatesE.Text = pin5.Location.ToString();
                   break;
                case 6:
                   temp.Location = pinLocation;
                   map.Children.Add(temp);
                   try
                   {
                       map.Children.Remove(qcpin);
                   }
                   catch (Exception exp)
                   { }
                   qcpin = temp;
                   cordinatesqcp.Text = qcpin.Location.ToString();
                   break;
                default:
                   break;
            }
            updateSquare();
            Keyboard.Focus(map);
        }

        private void releaseQC_Click(object sender, RoutedEventArgs e)
        {
            valueRead = 6;
            Keyboard.Focus(map);
        }

        private void map_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.Key == Key.Left)
            {
                try
                {
                    map.Children.Remove(qcpin);
                }
                catch (Exception excp)
                { }
                qcpin.Location.Longitude -= 0.001;
                map.Children.Add(qcpin);
            }
            if (e.Key == Key.Right)
            {
                try
                {
                    map.Children.Remove(qcpin);
                }
                catch (Exception excp)
                { }
                qcpin.Location.Longitude += 0.001;
                map.Children.Add(qcpin);
            }
            if (e.Key == Key.Up)
            {
                try
                {
                    map.Children.Remove(qcpin);
                }
                catch (Exception excp)
                { }
                qcpin.Location.Latitude += 0.001;
                map.Children.Add(qcpin);
            }
            if (e.Key == Key.Down)
            {
                try
                {
                    map.Children.Remove(qcpin);
                }
                catch (Exception excp)
                { }
                qcpin.Location.Latitude -= 0.001;
                map.Children.Add(qcpin);
            }
            if(e.Key == Key.Space)
            {
                try
                {
                    myDataTable.Rows.Add(qcpin.Location.Latitude.ToString() + ", " + qcpin.Location.Longitude.ToString());
                    dtgrid.ItemsSource = myDataTable.DefaultView;
                }
                catch(Exception expc)
                { }
            }
        }

        private void dtgrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Load image from quadcopter.");
        }
    }
}
