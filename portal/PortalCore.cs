using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArendaRESTLib;

namespace Portal
{
    // Главный класс портала
    public class PortalCore
    {
        // Список параметров GET
        public Dictionary<String, String> _get;
        // Список полученных COOKIE
        public Dictionary<String, String> _cookie;
        // Код пользователя
        private string userid;

        // Получение переменной GET
        private String getVar(string name)
        {
            if (_get.ContainsKey(name)) return _get[name]; else return "";
        }
        // Получение cookies
        private String getCookie(string name)
        {
            if (_cookie.ContainsKey(name)) return _cookie[name]; else return "";
        }
        // Ссылка на экземплятор HTTP-запроса
        private HttpProcessor p;
        public PortalCore(HttpProcessor Ap)
        {
            p = Ap;
            _get = new Dictionary<String, String>();
            _cookie = new Dictionary<String, String>();
        }
        // Провка, авторизован ли пользователь
        private bool isAuthMode()
        {
            if (getVar("logout") == "1")
            {
                p.setCookie("PortalUser", "");
                return false;
            }
            return (userid != "");
        }
        // Получение обработанного шаблона сайта
        public Template getResult()
        {
            // Получение пользователя из сессии
            userid = getCookie("PortalUser");
            // Получение текущей страницы
            String page = getVar("page");
            if (page == "") page = "start";

            Template html = new Template("main");
            html.setVar("USERNAME", "Гость");

            Template subpage = null;

            // Обработка стартовой страницы
            if (page == "start")
            {
                subpage = new Template(page);
            }
            else
                if (page == "about")
                {
                    subpage = new Template(page);
                }
            
            else
            // Обработка страницы поиска
                if (page == "findrooms")
                {
                    subpage = new Template(page);
                    string tekcity = getVar("citylist");

                    List<String> citylist = ObjModule.channel.GetCityList();
                    string options = "";
                    foreach (var opt in citylist)
                        options += "<option value='" + opt + "' "+(tekcity.Equals(opt)?"selected":"")+">"+opt+"</option>";
                    subpage.setVar("CITYLIST", options);

                    List<String> ids = ObjModule.channel.GetItems();

                    string roomshtml = "";
                    foreach (var id in ids)
                    {
                        ArendaItem item = ObjModule.channel.GetItemByID(id);
                        if ((tekcity == "") || (tekcity.Equals(item.city)))
                        {
                            Template roomtpl = new Template("_room");
                            roomtpl.setVar("ID", id);
                            roomtpl.setVar("TYPE", item.roomtype);
                            roomtpl.setVar("PRICE", item.price);
                            roomtpl.setVar("S", item.s);
                            roomtpl.setVar("CITY", item.city);
                            roomtpl.setVar("ADDRESS", item.address);
                            roomtpl.setVar("ELITE", item.elite ? "Премиум" : "Стандарт");
                            roomshtml += roomtpl.HTML();
                        }
                    }
                    subpage.setVar("ROOMS", roomshtml);
                
                }
                else
                {
                    subpage = new Template("notfound");
                    subpage.setVar("PAGE", page);
                }

            html.setVar("PAGEDATA", subpage.HTML());
            // Добавляем случайное значения для защиты от кэширования
            html.setVar("RND", (new Random()).Next(10000).ToString("D"));
            html.setVar("PAGE", page);
              
            return html;

 
        }
         
    }
}
