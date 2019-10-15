using CoreNotes.AutoFac.IRepository;
using System.Collections;
using System.Collections.Generic;

namespace CoreNotes.AutoFac.Repository
{
    public class MenuRepository : IMenuRepository
    {
        public List<Hashtable> GetMenuList()
        {
            List<Hashtable> listMenu = new List<Hashtable>();
            Hashtable ht1 = new Hashtable();
            ht1.Add("id", 1);
            ht1.Add("pid", -1);
            ht1.Add("url", "/");
            ht1.Add("name", "首页");
            listMenu.Add(ht1);

            Hashtable ht2 = new Hashtable();
            ht2.Add("id", 2);
            ht2.Add("pid", -1);
            ht2.Add("url", "/news");
            ht2.Add("name", "资讯");
            listMenu.Add(ht2);

            Hashtable ht3 = new Hashtable();
            ht3.Add("id", 3);
            ht3.Add("pid", 2);
            ht3.Add("url", "/news/hot");
            ht3.Add("name", "热点");
            listMenu.Add(ht3);

            Hashtable ht4 = new Hashtable();
            ht4.Add("id", 4);
            ht4.Add("pid", 2);
            ht4.Add("url", "/news/latest");
            ht4.Add("name", "滚动新闻");
            listMenu.Add(ht4);

            Hashtable ht5 = new Hashtable();
            ht5.Add("id", 5);
            ht5.Add("pid", 4);
            ht5.Add("url", "/news/latest/international");
            ht5.Add("name", "国际快讯");
            listMenu.Add(ht5);

            Hashtable ht6 = new Hashtable();
            ht6.Add("id", 6);
            ht6.Add("pid", -1);
            ht6.Add("url", "/domain");
            ht6.Add("name", "行业");
            listMenu.Add(ht6);

            Hashtable ht7 = new Hashtable();
            ht7.Add("id", 7);
            ht7.Add("pid", 5);
            ht7.Add("url", "/news/latest/international/politics");
            ht7.Add("name", "政治");
            listMenu.Add(ht7);

            Hashtable ht8 = new Hashtable();
            ht8.Add("id", 8);
            ht8.Add("pid", 5);
            ht8.Add("url", "/news/latest/international/military");
            ht8.Add("name", "军事");
            listMenu.Add(ht8);
            return listMenu;
        }
    }
}
