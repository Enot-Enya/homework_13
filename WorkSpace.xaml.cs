using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;
using homework_15;


namespace homework_12
{

    /// <summary>
    /// Логика взаимодействия для WorkSpace.xaml
    /// </summary>
    public partial class WorkSpace : Window
    {
        public static event Action<string> OpenAccountEvent;
        public static event Action<string> DeleteAccountEvent;
        public static event Action<string> TransferAccountEvent;
        public static event Action<string> DeleteClient;
        public static event Action<string> AddClient;
        public static event Action<string> BaseCreateEvent;
        public static event Action<string> BaseDeleteEvent;

        public WorkSpace(string user)
        {
            InitializeComponent();
            Homework14.Initialize();
            WindowState = WindowState.Maximized;
            Title = user;
            Refresh();
            if (Title == "Менеджер")
            {
                ExpanderAddNewClient.Visibility = Visibility.Visible;
            }
            else ExpanderAddNewClient.Visibility = Visibility.Hidden;
        }


        /// <summary>
        /// Создает новую базу данных. Записывает ее в JSON формат
        /// </summary>
        private void BaseCreateButton(object sender, RoutedEventArgs e)
        {
            List<Client> temp = Client.Generate(int.Parse(dataBaseCountTextBox.Text));
            SaveToJson(temp);
            Refresh();
            BaseCreateEvent?.Invoke(Title);
        }

        /// <summary>
        /// Удаляет базу данных
        /// </summary>
        private void DeleteBaseButton(object sender, RoutedEventArgs e)
        {
            File.Delete("Clientlist.json");
            Refresh();
            BaseDeleteEvent?.Invoke(Title);
        }
        /// <summary>
        /// Метод смены пользователя
        /// </summary>
        private void UserChange(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        /// <summary>
        /// Открывает информацию о клиенте для редактирования
        /// </summary>
        private void OpenVision(object sender, MouseButtonEventArgs e)
        {
            // Я понимаю что можно сделать типо есть щас скрыто открыть, если открыто закрыть и сделать один метод а не 2
            // У меня объект появляется при 2 клике
            // Получилось бы 2 раза кликнул открылось, 2 раза кликнул закрылось
            // Я не хотел чтобы так было, поэтому разделил.

            SPOneClient.Visibility = Visibility.Visible;
            ButtonsForOneClient.Visibility = Visibility.Visible;
            GiveAccess(Title);
        }
        /// <summary>
        /// Скрывает информацию о клиенте для редактирования
        /// </summary>
        private void CloseVision()
        {
            SPOneClient.Visibility = Visibility.Hidden;
            ButtonsForOneClient.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Если работает менеджер открывает поля для редактирования
        /// </summary>
        private void GiveAccess(string title)
        {
            // Я понимаю что по смыслу он должен быть в менеджере
            // Если метод перености туда он говорит 
            // "для не статичного поля или метода требует ссылка на объект"
            // Решения лучше чем это я не нашел
            // Подскажите если как то можно адекватно из класса менеджер менять атрибуты полей
            if (title == "Менеджер")
            {
                TextBoxSurnameChange.IsReadOnly = false;
                TextBoxNameChange.IsReadOnly = false;
                TextBoxPatronymicChange.IsReadOnly = false;
                TextBoxPassportNumberChange.IsReadOnly = false;
            }
        }

        /// <summary>
        /// Сохраняет изменения в базе данных клиентов
        /// </summary>
        private void Save(object sender, RoutedEventArgs e)
        {
            TextBox[] newClientFields = { TextBoxSurnameChange, TextBoxNameChange, TextBoxPatronymicChange, TextBoxPassportNumberChange, TextBoxPhoneNumberChange };
            int count = 0;
            foreach (TextBox field in newClientFields)
            {
                if (String.IsNullOrEmpty(field.Text))
                {
                    field.Background = Brushes.Red;
                    MessageBox.Show("Поле не должно быть пустым");
                    break;
                }
                else
                {
                    count++;
                }
            }
            if (count == 5)
            {
                if (TextBoxPhoneNumberChange.Text.Length < 10)
                {
                    TextBoxPhoneNumberChange.Background = Brushes.Red;
                    MessageBox.Show("Не корректное значение");
                    count--;
                }
                if (TextBoxPassportNumberChange.Text.Length < 10)
                {
                    TextBoxPassportNumberChange.Background = Brushes.Red;
                    MessageBox.Show("Не корректное значение");
                    count--;
                }
            }
            if (count == 5)
            {
                List<Client> allClients = Client.JsonToList("Менеджер");
                int id = int.Parse(TextBoxId.Text);
                if (Title == "Менеджер")
                {
                    allClients[id].surname = TextBoxSurnameChange.Text;
                    allClients[id].name = TextBoxNameChange.Text;
                    allClients[id].patronymic = TextBoxPatronymicChange.Text;
                    allClients[id].passportNumber = TextBoxPassportNumberChange.Text;
                }
                allClients[id].phoneNumber = TextBoxPhoneNumberChange.Text;
                allClients[id].dateAndTimeChange = Convert.ToString(DateTime.Now);
                allClients[id].whoChanged = Title;
                SaveToJson(allClients);
                Refresh();
            }
            else Refresh();
            foreach (TextBox field in newClientFields)
            {
                field.Background = Brushes.Cornsilk;
            }
            CloseVision();
        }
        /// <summary>
        /// Удаляет клиента из списка
        /// </summary>
        private void Delete(object sender, RoutedEventArgs e)
        {
            List<Client> allClients = Client.JsonToList("Менеджер");
            int id = int.Parse(TextBoxId.Text);
            allClients.Remove(allClients[id]);
            SaveToJson(allClients);
            Refresh();
            CloseVision();
            DeleteClient?.Invoke(Title);
        }



        /// <summary>
        /// Добавляет клиента в список
        /// </summary>
        private void ButtonAddToList(object sender, RoutedEventArgs e)
        {
            TextBox[] newClientFields = { TextBoxSurname, TextBoxName, TextBoxPatronymic, TextBoxPhoneNumber, TextBoxPassportNumber };
            int count = 0;
            foreach (TextBox field in newClientFields)
            {
                if (String.IsNullOrEmpty(field.Text))
                {
                    field.Background = Brushes.Red;
                    field.ToolTip = "Поле не должно быть пустым";
                }
                else
                {
                    field.Background = Brushes.AliceBlue;
                    field.ToolTip = null;
                    count++;
                }
            }

            if (count == 5)
            {
                ExpanderAddNewClient.IsExpanded = false;
                List<Client> allClients = Client.JsonToList("Менеджер");
                allClients.Add(new Client()
                {
                    surname = newClientFields[0].Text,
                    name = newClientFields[1].Text,
                    patronymic = newClientFields[2].Text,
                    phoneNumber = newClientFields[3].Text,
                    passportNumber = newClientFields[4].Text,
                });
                SaveToJson(allClients);
                foreach (TextBox field in newClientFields)
                {
                    field.Text = null;
                }
                Refresh();
                AddClient?.Invoke(Title);
            }
        }
        /// <summary>
        /// Обновляет окно со списком клиентов
        /// </summary>
        private void Refresh()
        {
            List<Client> allClients = Client.JsonToList(Title.ToString());
            LVclientBase.ItemsSource = allClients;
        }
        /// <summary>
        /// Сортирует текст при клике по хедеру
        /// </summary>
        private void GridViewColumnHeaderClick(object sender, RoutedEventArgs e)
        {
            string fullHeaderName = Convert.ToString(e.OriginalSource as GridViewColumnHeader);
            string headerName = fullHeaderName.Remove(0, fullHeaderName.Length - (fullHeaderName.Length - fullHeaderName.LastIndexOf(' ')));
            // я не понял как проще получить текст выбранного хедера

            List<Client> allClients = Client.JsonToList("Менеджер");

            switch (headerName.Trim())
            {
                case "Фамилия": allClients.Sort(new Client.SortBySurname()); break;
                case "Имя": allClients.Sort(new Client.SortByName()); break;
                case "Отчество": allClients.Sort(new Client.SortByPatronymic()); break;
                case "данные": allClients.Sort(new Client.SortByPassportNumber()); break;
                case "телефона": allClients.Sort(new Client.SortByPhoneNumber()); break;
                default: MessageBox.Show("Ошибка"); break;
            }
            SaveToJson(allClients);
            Refresh();
        }
        /// <summary>
        /// Сохраняет базу данный в JSON
        /// </summary>
        /// <param name="allClient"></param>
        private void SaveToJson(List<Client> allClient)
        {
            var json = JsonConvert.SerializeObject(allClient);
            File.WriteAllText("Clientlist.json", json);
        }

        /// <summary>
        /// Удаляет счет клиента
        /// </summary>
        private void DeleteAccountButtonClick(object sender, RoutedEventArgs e)
        {
            List<Client> allClients = Client.JsonToList("Менеджер");
            int id = int.Parse(TextBoxId.Text);
            int accountId = AccountColums.SelectedIndex;

            allClients[id].accounts.Remove(allClients[id].accounts[accountId]);
            SaveToJson(allClients);
            Refresh();
            DeleteAccountEvent?.Invoke(Title);
        }
        /// <summary>
        /// Открывает панель перевода Денежных средств
        /// </summary>
        private void TransferAccountButtonClick(object sender, RoutedEventArgs e)
        {
            List<Client> allClients = Client.JsonToList("Менеджер");
            TransferStackPanel.Visibility = Visibility.Visible;

            TransferNameCombobox.ItemsSource = allClients;
        }
        /// <summary>
        /// Добавляет счет клиенту
        /// </summary>
        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(ComboBoxAccountType.Text))
            {
                MessageBox.Show("Выберите тип счета");
            }
            else
            {
                if (int.Parse(TextBoxId.Text) < 0)
                {
                    MessageBox.Show("Выберите клиента");
                }
                else
                {
                    List<Client> allClients = Client.JsonToList("Менеджер");
                    int id = int.Parse(TextBoxId.Text);
                    if (ComboBoxAccountType.Text == "Депозитный" && allClients[id].accounts.Where(account => account.isDeposit).Any())
                    {
                        MessageBox.Show("У клиента уже есть депозитный счет");
                    }

                    else if (ComboBoxAccountType.Text == "Не депозитный" && allClients[id].accounts.Where(account => account.isDeposit == false).Any())

                    {
                        MessageBox.Show("У клиента уже есть не депозитный счет");
                    }
                    else
                    {
                        allClients[id].accounts.Add(new Client.Account()
                        {
                            isDeposit = ComboBoxAccountType.Text == "Депозитный" ? true : false,
                            accountNumber = Client.GenerateAccountNumber(),
                            balance = 0
                        });
                        SaveToJson(allClients);
                        Refresh();
                        OpenAccountEvent?.Invoke(Title);
                    }
                }
            }
            ComboBoxAccountType.Text = null;
        }
        /// <summary>
        /// Выполняет перевод денежных средств
        /// </summary>
        private void TransferButtonGoClick(object sender, RoutedEventArgs e)
        {
       
            try
            {
                double summ = double.Parse(TBSumm.Text);
                if (summ < 0) throw new Exception();
                List<Client> allClients = Client.JsonToList("Менеджер");
                int idOut = int.Parse(TextBoxId.Text);
                int TranferTypeAccountOut = AccountColums.SelectedIndex;

                try
                {
                    if (allClients[idOut].accounts[TranferTypeAccountOut].balance - summ < 0) throw new SomeException();
                    allClients[idOut].accounts[TranferTypeAccountOut].balance -= summ;
                    int idIn = TransferNameCombobox.SelectedIndex;
                    int TranferTypeAccountIn = TranserAccountType.SelectedIndex;
                    allClients[idIn].accounts[TranferTypeAccountIn].balance += summ;
                    SaveToJson(allClients);
                    Refresh();
                    TransferAccountEvent?.Invoke(Title);
                    TransferStackPanel.Visibility = Visibility.Hidden;
                }
                catch (SomeException ex)
                {
                    MessageBox.Show($"{ex.Message}");
                }
            }
            catch (FormatException) 
            {
                MessageBox.Show("Не верный формат");
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
            
           

        }

    }
}
