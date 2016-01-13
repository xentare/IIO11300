/*
* Copyright (C) JAMK/IT/Esa Salmikangas
* This file is part of the IIO11300 course project.
* Created: 12.1.2016 Modified: 13.1.2016
* Authors: Tero ,Esa Salmikangas
* luotu Testit@Kotikone-branchi 13.1.2016 klo 19:21
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Tehtava1
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      IniMyStuff();
    }
    private void IniMyStuff()
    {
      //comboon värejä
      Type colorsType = typeof(System.Windows.Media.Colors);
      PropertyInfo[] colorsTypePropertyInfos = colorsType.GetProperties(BindingFlags.Public | BindingFlags.Static);
      foreach (PropertyInfo colorsTypePropertyInfo in colorsTypePropertyInfos)
        cbSetColor.Items.Add(colorsTypePropertyInfo.Name);
    }
    private void btnCalculate_Click(object sender, RoutedEventArgs e)
    {
      //TODO
      try
      {
        double result;
        result = BusinessLogicWindow.CalculatePerimeter(1, 1);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
      finally
      {
        //yield to an user that everything okay
      }
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }

    private void btnDraw_Click(object sender, RoutedEventArgs e)
    {
      //TODO piirrä ikkuna oikeassa koossa MyRectangle-elementillä
      //TODO tarkistukset että annetut arvot ok
      double w = double.Parse(txtWidht.Text);
      double h = double.Parse(txtHeight.Text);
      if (w > 0 & w <= 1000 & h > 0 & h <= 1000)
      {
        //piirretään ikkuna
        myRectangle.Width = w;
        myRectangle.Height = h;
        //keskelle
        
        if (chkAddCross.IsChecked.Value)
        {
          //piirretään pystyristikko
          Rectangle rect = new Rectangle();
          rect.StrokeThickness = 2.5;
          rect.Stroke = new SolidColorBrush(Colors.Black);
          rect.Width = 5;
          rect.Height = h;
          //TODO mihin paikkaan
          myCanvas.Children.Add(rect);
          Canvas.SetLeft(rect, (w / 2D) + 5 );
          //piirretään vaakaristikko
          Rectangle rect2 = new Rectangle();
          rect2.StrokeThickness = 2.5;
          rect2.Stroke = new SolidColorBrush(Colors.Black);
          rect2.Width = w;
          rect2.Height = 5;
          //TODO mihin paikkaan
          myCanvas.Children.Add(rect2);
          Canvas.SetTop(rect2,(h / 2D) + 5);
        }
      }
    }

    private void cbSetColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      //vaihdetaan ikkunan väri
      SolidColorBrush scb = new SolidColorBrush();
      //KESKEN tämä castaus ei toimi
      //TODO miten muutetaan käyttäjän valitsema värin nimi Coloriksi?
      scb.Color = (Color)cbSetColor.SelectedValue;
      myRectangle.Fill = scb;
    }
  }

  public class BusinessLogicWindow
  {
    /// <summary>
    /// CalculatePerimeter calculates the perimeter of a window
    /// </summary>
    public static double CalculatePerimeter(double widht, double height)
    {
      throw new System.NotImplementedException();
    }
  }
}
