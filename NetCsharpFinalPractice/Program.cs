// See https://aka.ms/new-console-template for more information
using NetCsharpFinalPractice;

internal class Program
{
    private static void Main(string[] args)
    {
        var users = FileManager.LoadUsersFromFile();
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("1. Войти");
            Console.WriteLine("2. Зарегистрироваться");
            Console.WriteLine("3. Выйти");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("Введите логин:");
                string login = Console.ReadLine();
                Console.WriteLine("Введите пароль:");
                string password = Console.ReadLine();

                var user = users.Find(u => u.Login == login && u.Password == password);
                if (user != null)
                {
                    Console.WriteLine("Добро пожаловать, " + user.Login);
                    StartQuizMenu(user, users);
                }
                else
                {
                    Console.WriteLine("Неверный логин или пароль.");
                }
            }
            else if (choice == "2")
            {
                Console.WriteLine("Введите логин:");
                string login = Console.ReadLine();

                if (!Validation.ValidateLogin(login))
                {
                    Console.WriteLine("Неверный логин.");
                    continue;
                }

                Console.WriteLine("Введите пароль:");
                string password = Console.ReadLine();

                if (!Validation.ValidatePassword(password))
                {
                    Console.WriteLine("Неверный пароль.");
                    continue;
                }

                Console.WriteLine("Введите дату рождения (yyyy-MM-dd):");
                DateTime dob = DateTime.Parse(Console.ReadLine());

                var newUser = new User { Login = login, Password = password, DateOfBirth = dob };
                users.Add(newUser);
                FileManager.SaveUsersToFile(users);
                Console.WriteLine("Регистрация прошла успешно.");
            }
            else if (choice == "3")
            {
                isRunning = false;
                Console.WriteLine("Выход...");
            }
        }
    }
    private static void StartQuizMenu(User user, List<User> users)
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("1. Стартовать викторину");
            Console.WriteLine("2. Просмотреть результаты");
            Console.WriteLine("3. Изменить настройки");
            Console.WriteLine("4. Выйти");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("Выберите раздел: География, История, Химия или Смешанная викторина");
                string category = Console.ReadLine();
                var quiz = new Quiz(category);
                quiz.StartQuiz(user);
            }
            else if (choice == "2")
            {
                Console.WriteLine("Здесь будут ваши результаты...");
            }
            else if (choice == "3")
            {
                Console.WriteLine("Введите новый пароль:");
                string newPassword = Console.ReadLine();
                Console.WriteLine("Введите новую дату рождения (yyyy-MM-dd):");
                DateTime newDob = DateTime.Parse(Console.ReadLine());

                user.Password = newPassword;
                user.DateOfBirth = newDob;
                FileManager.SaveUsersToFile(users);
                Console.WriteLine("Настройки обновлены.");
            }
            else if (choice == "4")
            {
                isRunning = false;
                Console.WriteLine("Выход...");
            }
        }
    }
}