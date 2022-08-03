using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace homework_12
{
    /// <summary>
    /// Содержит структуры информации о клиентах, а так же методы для работы с ними
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string patronymic { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// Серия номер паспорта
        /// </summary>
        public string passportNumber { get; set; }
        /// <summary>
        /// Дата изменений
        /// </summary>
        public string dateAndTimeChange { get; set; }

        /// <summary>
        /// Кто поменял
        /// </summary>
        public string whoChanged { get; set; }

        /// <summary>
        /// Информация о счетах клиента
        /// </summary>
        public List<Account> accounts { get; set; }

        

        private static readonly string[] maleSername = ($"Иванов Васильев Петров Смирнов Михайлов Фёдоров Соколов Яковлев Попов Андреев Алексеев Александров Лебедев Григорьев Степанов Семёнов Павлов Богданов Николаев Дмитриев Егоров Волков Кузнецов Никитин Соловьёв Тимофеев " +
               $"Орлов Афанасьев Филиппов Сергеев Захаров Матвеев Виноградов Кузьмин Максимов Козлов Ильин Герасимов Марков Новиков Морозов Романов Осипов Макаров Зайцев Беляев Гаврилов Антонов Ефимов Леонтьев Давыдов Гусев Данилов Киселёв Сорокин Тихомиров " +
               $"Крылов Никифоров Кондратьев Кудрявцев Борисов Жуков Воробьёв Щербаков Поляков Савельев Шмидт Трофимов Чистяков Баранов Сидоров Соболев Карпов Белов Миллер Титов Львов Фролов Игнатьев Комаров Прокофьев Быков Абрамов Голубев Пономарёв Покровский " +
               $"Мартынов Кириллов Шульц Миронов Фомин Власов Троицкий Федотов Назаров Ушаков Денисов Константинов Воронин Наумов").Split();

        private static readonly string[] femaleSername = ($"Белокрылова Дмитриева Соколович Ковальчук Сафронова Каменская Романенко Бондаренко Вершинина Соловьева Голубева Федосеева Чехова Павленко Меркулова Кармазина Канаева Наварская Суворова Рощина Ленина Марченко Дементьева Архипова " +
            $"Шорохова Снежная Кудрявцева Дубровская Минаева Мечникова Путина Богданова Исаева Солнцева Бердинских Крымская Швец Платонова Пугачева Тихомирова Давыдова Ржевская Аверина Добровольская Садовская Дроздова Бородина Брежнева Баринова Доценко Вишневская " +
            $"Шевченко Абрамович Берестова Сочинская Данилова Радецкая Репина Литковская Владимирова Михайлова Шанская Любимова Громова Астахова Высоцкая Городецкая Жукова Сомова Звездная Шаповалова Галицына Быстрова Зорина Сахарова Жданова Редкая Травникова Смирнова " +
            $"Соболь Осипова Андрианова Якубович Виноградова Федорчук Герасименко Бойцова Оленникова Ермилова Пименова Дарьянова Чудина Лаврентьева Павловская Точеная Снегова Добронравова Алтырева Лазарева Калашникова Серебрянникова Вещая Чайка Дружинина Валинина Довлатова Варфоломеева Кротова " +
            $"Верховская Богачева Золотухина Милованова Матвиенко Казакова Захарова Белоусова Санина Ерошенко Лучная Шпагина Кутузова Емельянова Круглова Кароль Баскова Уланова Краснова Железная Гоголь Бровина Румянцева Подорожная").Split();

        private static readonly string[] maleName = ($"Александр Дмитрий Максим Сергей Андрей Алексей Артём Илья Кирилл Михаил Никита Матвей Роман Егор Арсений Иван Денис Евгений Даниил Тимофей Владислав Игорь Владимир Павел Руслан Марк Константин Тимур Олег Ярослав Антон " +
                $"Николай Глеб Данил Савелий Вадим Степан Юрий Богдан Артур Семен Макар Лев Виктор Елисей Виталий Вячеслав Захар Мирон Дамир Георгий Давид Платон Анатолий Григорий Демид Данила Станислав Василий Федор Родион Леонид Одиссей Валерий Святослав " +
                $"Борис Эдуард Марат Герман Даниэль Петр Амир Всеволод Мирослав Гордей Артемий Эмиль Назар Савва Ян Рустам Игнат Влад Альберт Тамерлан Айдар Роберт Адель Марсель Ильдар Самир Тихон Рамиль Ринат Радмир Филипп Арсен Ростислав Святогор Яромир").Split();

        private static readonly string[] femaleName = ($"Анастасия Анна Мария Елена Дарья Алина Ирина Екатерина Арина Полина Ольга Юлия Татьяна Наталья Виктория Елизавета Ксения Милана Вероника Алиса Валерия Александра Ульяна Кристина София Марина Светлана " +
            $"Варвара Софья Диана Яна Кира Ангелина Маргарита Ева Алёна Дарина Карина Василиса Олеся Аделина Оксана Таисия Надежда Евгения Элина Злата Есения Милена Вера Мирослава Галина Людмила Валентина Нина Эмилия Камилла Альбина Лилия Любовь Лариса Эвелина Инна " +
            $"Агата Амелия Амина Эльвира Ярослава Стефания Регина Алла Виолетта Лидия Амалия Наталия Марьяна Анжелика Нелли Влада Виталина Майя Тамара Мелания Лиана Василина Зарина Алия Владислава Самира Антонина Ника Мадина Наташа Каролина Снежана Юлиана Ариана Эльмира Ясмина Жанна").Split();

        private static readonly string[] malePatronymic = ($"Александрович Алексеевич Анатольевич Андреевич Антонович Аркадьевич Арсеньевич Артемович Афанасьевич Богданович Борисович Вадимович Валентинович Валериевич Васильевич Викторович Витальевич Владимирович " +
               $"Всеволодович Вячеславович Гаврилович Геннадиевич Георгиевич Глебович Григорьевич Давыдович Данилович Денисович Дмитриевич Евгеньевич Егорович Емельянович Ефимович Иванович Игоревич Ильич Иосифович Кириллович Константинович Корнеевич Леонидович Львович Макарович Максимович " +
               $"Маркович Матвеевич Митрофанович Михайлович Назарович Наумович Николаевич Олегович Павлович Петрович Платонович Робертович Родионович Романович Савельевич Семенович Сергеевич Станиславович Степанович Тарасович Тимофеевич Тихонович Федорович Феликсович Филиппович Эдуардович Юрьевич Яковлевич Ярославович").Split();

        private static readonly string[] femalePatronymic = ($"Александровна Алексеевна Анатольевна Андреевна Антоновна Аркадьевна Арсеньевна Артемовна Афанасьевна Богдановна Борисовна Вадимовна Валентиновна Валериевна Васильевна Викторовна Витальевна Владимировна " +
            $"Всеволодовна Вячеславовна Гавриловна Геннадиевна Георгиевна Глебовна Григорьевна Давыдовна Даниловна Денисовна Дмитриевна Евгеньевна Егоровна Емельяновна Ефимовна Ивановна Игоревна Ильинишна Иосифовна Кирилловна Константиновна Корнеевна Леонидовна Львовна Макаровна Максимовна " +
            $"Марковна Матвеевна Митрофановна Михайловна Назаровна Наумовна Николаевна Олеговна Павловна Петровна Платоновна Робертовна Родионовна Романовна Савельевна Семеновна Сергеевна Станиславовна Степановна Тарасовна Тимофеевна Тихоновна Федоровна Феликсовна Филипповна Эдуардовна Юрьевна Яковлевна Ярославовна").Split();

        static Random random = new Random();
        /// <summary>
        /// Создает базу клиентов
        /// </summary>
        /// <param name="count">Сколько нужно создать клиентов</param>
        /// <returns></returns>
        public static List<Client> Generate(int count)
        {

            List<Client> clients = new List<Client>();
            for (int i = 0; i < count; i++)
            {
                int maleFemaleRand = random.Next(2);
                // 0 - мужчина, 1 - женщина.

                clients.Add(new Client()
                {
                    surname = SernameGenerator(maleFemaleRand),
                    name = NameGenerator(maleFemaleRand),
                    patronymic = PatronymicGenerator(maleFemaleRand),
                    phoneNumber = "+7 " + random.Next(910, 999).ToString() + " " + random.Next(000, 999).ToString("000") + " " + random.Next(0000, 9999).ToString("0000"),
                    passportNumber = random.Next(1000, 9999).ToString() + " " + random.Next(000000, 999999).ToString("000000"),
                    accounts = AccountGenerator()
                });
            }

            return clients;
        }

        /// <summary>
        /// Генерирует случайные фамилии
        /// </summary>
        /// <param name="maleFemaleRand">0 - мужчина, 1 - женщина</param>
        /// <returns></returns>
        static string SernameGenerator(int maleFemaleRand)
        {
            if (maleFemaleRand == 1)
            {
                return femaleSername[random.Next(femaleSername.Length)];
            }
            else return maleSername[random.Next(maleSername.Length)];

        }

        /// <summary>
        /// Генерирует имя
        /// </summary>
        /// <param name="maleFemaleRand">0 - мужчина, 1 - женщина</param>
        /// <returns></returns>
        static string NameGenerator(int maleFemaleRand)
        {
            return maleFemaleRand == 1 ? femaleName[random.Next(femaleName.Length)] : maleName[random.Next(maleName.Length)];
        }
        /// <summary>
        /// Генерирует отчество
        /// </summary>
        /// <param name="maleFemaleRand">0 - мужчина, 1 - женщина</param>
        /// <returns></returns>
        static string PatronymicGenerator(int maleFemaleRand)
        {
            return maleFemaleRand == 1 ? femalePatronymic[random.Next(femalePatronymic.Length)] : malePatronymic[random.Next(malePatronymic.Length)];
        }

        /// <summary>
        /// Десериализация базы данных и вывод ее на экран
        /// </summary>
        static public List<Client> JsonToList(string user)
        {
            if (File.Exists("Clientlist.json"))
            {
                List<Client> clientList = new List<Client>();
                string json = File.ReadAllText("Clientlist.json");
                clientList = JsonConvert.DeserializeObject<List<Client>>(json);

                if (user != "Менеджер")
                {
                    foreach (Client client in clientList)
                    {
                        client.passportNumber = "**** ******";
                    }
                }
                return clientList;
            }
            else return null;
        }
        /// <summary>
        /// Генерирует список счетов
        /// </summary>
        static List<Account> AccountGenerator()
        {
            List<Account> accountList = new List<Account>();
            int count = random.Next(0,3);
            for(int i = 0; i<count; i++)
            {
                accountList.Add(new Account()
                {

                    isDeposit = (i == 1 ? false : true),
                    accountNumber = GenerateAccountNumber(),
                    balance = Convert.ToDouble((random.Next(0, 5) == 2 ? random.Next(00000, 99999) : random.Next(0000, 9999)) + (random.NextDouble().ToString("0.00")))
                }); ;
            }
            return accountList;
        }

        static public string GenerateAccountNumber()
        {
            return "408 178 " + random.Next(000000, 999999).ToString("000 000 ") + random.Next(000000, 999999).ToString("000 000");
        }

        #region Методы сортировок
        public class SortByPassportNumber : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = (Client)x;
                Client Y = (Client)y;
                if (UInt64.Parse(X.passportNumber.Replace(" ", "")) > UInt64.Parse(Y.passportNumber.Replace(" ", ""))) return 1;
                else if (UInt64.Parse(X.passportNumber.Replace(" ", "")) < UInt64.Parse(Y.passportNumber.Replace(" ", ""))) return -1;
                else return 0;
            }
        }
        public class SortByPhoneNumber : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = (Client)x;
                Client Y = (Client)y;
                if (UInt64.Parse(X.phoneNumber.Replace(" ", "")) > UInt64.Parse(Y.phoneNumber.Replace(" ", ""))) return 1;
                else if (UInt64.Parse(X.phoneNumber.Replace(" ", "")) < UInt64.Parse(Y.phoneNumber.Replace(" ", ""))) return -1;
                else return 0;
            }
        }
        public class SortBySurname : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = (Client)x;
                Client Y = (Client)y;
                return String.Compare(X.surname, Y.surname);
            }
        }
        public class SortByName: IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = (Client)x;
                Client Y = (Client)y;
                return String.Compare(X.name, Y.name);
            }
        }
        public class SortByPatronymic : IComparer<Client>
        {
            public int Compare(Client x, Client y)
            {
                Client X = (Client)x;
                Client Y = (Client)y;
                return String.Compare(X.patronymic, Y.patronymic);
            }
        }
        #endregion
        /// <summary>
        /// Информация о счете клиента
        /// </summary>
        public class Account
        {
            /// <summary>
            /// Тип счета. True - депозитный False - нет
            /// </summary>
            public bool isDeposit { get; set; }

            /// <summary>
            /// Номер счета
            /// </summary>
            public string accountNumber { get; set; }

            /// <summary>
            /// Баланс счета
            /// </summary>
            public double balance { get; set; }

        }

    }
}
