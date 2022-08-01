using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace homework_12
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
       
    }

    /// <summary>
    /// Конвертирует тип счета из булева в стринга для удобного одображения в окне
    /// </summary>
    public class BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
       System.Globalization.CultureInfo culture)
        {
            if ((bool)value == true)
                return "Депозитный";
            else
                return "Не депозитный";
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if ((string)value == "Депозитный")
                return true;
            else
                return false;
        }

    }
}
