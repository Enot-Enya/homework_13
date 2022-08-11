using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace homework_12
{

    internal class Homework14
    {
        public static void Initialize() 
        {
            if (!File.Exists("log.txt")) File.Create("log.txt");
            WorkSpace.BaseCreateEvent += BaseCreate;
            WorkSpace.BaseDeleteEvent += BaseDelete;
            WorkSpace.DeleteClient += DeleteClient;
            WorkSpace.AddClient += AddClient;
            WorkSpace.DeleteAccountEvent += DeleteAccount;
            WorkSpace.OpenAccountEvent += OpenAccount;
            WorkSpace.TransferAccountEvent += TransferAccount;

        }
        static public void OpenAccount(string worker) 
        {
            string line = "Добавление счета для клиента";
            DoLog(worker, line);
           
        }
        static public void DeleteAccount(string worker)
        {
            string line = "Удаление счета клиента";
            DoLog(worker, line);
        }
        static public void TransferAccount(string worker)
        {
            string line = "Перевод денежных средств";
            DoLog(worker, line);
        }
        static public void DeleteClient(string worker)
        {
            string line = "Удаление клиента";
            DoLog(worker, line);
        }
        static public void AddClient(string worker)
        {
            string line = "Добавление клиента";
            DoLog(worker, line);

        }
        static public void BaseDelete(string worker)
        {
            string line = "Удаление базы данных";
            DoLog(worker, line);
        }
        static public void BaseCreate(string worker)
        {
            string line = "Создание базы данных";
            DoLog(worker, line);
        }

        static public async void DoLog(string worker, string whatHappens)
        {
            string line = DateTime.Now.ToString();
            line += " " + worker + " " + whatHappens;
            using (StreamWriter sw = new StreamWriter("log.txt", true))
            {
                sw.WriteLine(line);
            }
           await DoAlert(line);
        }

        /// <summary>
        /// Выводит уведомления
        /// </summary>
        /// <param name="line">Текст уведомления</param>
        /// <returns></returns>


        /*
       public static async Task DoAlert(string line)
        {
            WorkSpace.TBAlert.Visibility = Visibility.Visible;
            WorkSpace.TBAlert.Text = line;
            await Task.Delay(2000);
            WorkSpace.TBAlert.Visibility = Visibility.Hidden;
        }
        */
        public static async Task DoAlert(string line)
        {
            Alert doAlert = new Alert();
            doAlert.Show();
            doAlert.TBAlert.Text = line;
            await Task.Delay(2000);
            doAlert.Close();
        }
    }
}
