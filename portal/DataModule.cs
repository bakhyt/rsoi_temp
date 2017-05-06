using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal
{
    // Модуль данных
    public class DataModule
    {
        // Список пользователей
        private static List<User> users;
        // Загрузка модуля
        public static void Load()
        {
            users = FileHelper.createListFromFile<User>("users.dat");
        }
        // Проверка авторизации
        public static bool isAuthOK(string login, string pass, ref string userid)
        {
            // Встроенная учетка
            if ((login == "admin") && (pass == "123456"))
            {
                userid = "1024";
                return true;
            }
            else
            {
                // Авторизация пользователей
                User u = users.Find((user) => user.isAuthOk(login, pass));
                if (u != null)
                {
                    userid = u.ID.ToString("D");
                    return true;
                }
                else
                    return false;
            }

        }
        // Добавление пользователя
        public static bool addUser(string login, string pass, string email, ref string msg)
        {
            User u = users.Find((user) => user.Login == login);
            if (u != null)
            {
                msg = "Такой логин уже занят";
                return false;
            }

            u = new User();
            u.Login = login;
            u.Pass = pass;
            u.Email = email;
            u.FIO = "Новый пользователь";
            if (users.Count() == 0)
                u.ID = 1;
            else
                u.ID = users.Max((user) => user.ID) + 1;
            users.Add(u);
            FileHelper.saveListToFile<User>(users, "users.dat");

            msg = "Успешная регистрация";
            return true;

        }
        // Получение пользователя по коду
        public static string getUserName(string userid)
        {
            if (userid == "1024") return "Администратор";

            User u = users.Find((user) => user.ID.ToString("D") == userid);
            if (u != null)
                return u.Login;
            else
                return "Анонимный пользователь";
        }        
    }
}
