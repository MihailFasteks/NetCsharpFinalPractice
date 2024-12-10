using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetCsharpFinalPractice
{
    [Serializable]
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    public class Validation
    {
        public static bool ValidateLogin(string login)
        {
            string loginPattern = @"^[a-zA-Z0-9]{3,20}$"; // Логин: 3-20 символов, только буквы и цифры
            return Regex.IsMatch(login, loginPattern);
        }

        public static bool ValidatePassword(string password)
        {
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{6,20}$"; // Пароль: минимум 1 цифра, 1 заглавная, 1 строчная буква, длина от 6 до 20 символов
            return Regex.IsMatch(password, passwordPattern);
        }
    }
    public class FileManager
    {
        private static string usersFile = "users.txt";

        // Метод для сохранения данных в текстовый файл
        public static void SaveUsersToFile(List<User> users)
        {
            try
            {
                // Открытие потока для записи в файл
                using (FileStream fs = new FileStream(usersFile, FileMode.Create, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    foreach (var user in users)
                    {
                        // Запись информации о пользователе в строковом формате
                        writer.WriteLine($"{user.Login},{user.Password},{user.DateOfBirth}");
                    }
                }
                Console.WriteLine("Данные пользователей сохранены в файл.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
            }
        }

        // Метод для загрузки данных из текстового файла
        public static List<User> LoadUsersFromFile()
        {
            var users = new List<User>();
            try
            {
                if (File.Exists(usersFile))
                {
                    // Открытие потока для чтения из файла
                    using (FileStream fs = new FileStream(usersFile, FileMode.Open, FileAccess.Read))
                    using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split(',');
                            if (parts.Length == 3)
                            {
                                var user = new User
                                {
                                    Login = parts[0],
                                    Password = parts[1],
                                    DateOfBirth = DateTime.Parse(parts[2])
                                };
                                users.Add(user);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Файл с данными пользователей не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            }
            return users;
        }
    }
    public class Quiz
    {
        public string Category { get; set; }
        public List<Question> Questions { get; set; }

        public Quiz(string category)
        {
            Category = category;
            Questions = new List<Question>();
            GenerateQuestions();
        }

        private void GenerateQuestions()
        {
            // Пример вопросов для викторины
            if (Category == "Geography")
            {
                Questions.Add(new Question("What is the capital of France?", new List<string> { "Paris" }));
                Questions.Add(new Question("What is the longest river in the world?", new List<string> { "Nile" }));
            }
            else if (Category == "History")
            {
                Questions.Add(new Question("Who was the first President of the United States?", new List<string> { "George Washington" }));
                Questions.Add(new Question("What year did World War II end?", new List<string> { "1945" }));
            }
            else if (Category == "Chemistry")
            {
                Questions.Add(new Question("What is the chemical symbol for water?", new List<string> { "H2O" }));
                Questions.Add(new Question("Who discovered the electron?", new List<string> { "J.J. Thomson" }));
            }
            else // Mixed quiz
            {
                Questions.Add(new Question("What is the capital of Japan?", new List<string> { "Tokyo" }));
                Questions.Add(new Question("Who painted the Mona Lisa?", new List<string> { "Leonardo da Vinci" }));
            }
        }

        public void StartQuiz(User user)
        {
            int correctAnswers = 0;
            Console.WriteLine($"Starting {Category} Quiz for {user.Login}.");
            foreach (var question in Questions)
            {
                Console.WriteLine(question.Text);
                var answers = Console.ReadLine().Split(',');

                // Проверка правильных ответов
                foreach (var answer in answers)
                {
                    if (question.CorrectAnswers.Contains(answer.Trim()))
                    {
                        correctAnswers++;
                    }
                }
            }
            Console.WriteLine($"Quiz finished! You got {correctAnswers} out of {Questions.Count} correct.");
        }
    }

    public class Question
    {
        public string Text { get; set; }
        public List<string> CorrectAnswers { get; set; }

        public Question(string text, List<string> correctAnswers)
        {
            Text = text;
            CorrectAnswers = correctAnswers;
        }
    }

}
