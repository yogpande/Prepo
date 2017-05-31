using priyankaApiProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace priyankaApiProject.Controllers
{
    public class MyController : ApiController
    {
        DBEntities db = new DBEntities();

        public Alphabet[] GetAlphabets()
        {
            return db.Alphabets.ToArray();
        }

        public Aphorism[] GetAphorism()
        {
            return db.Aphorism.ToArray();
        }

        public IEnumerable<Author> GetAuthor(int id)
        {
            List<Author> li = new List<Author>();

            var data = from a in db.Authors join b in db.TabletDescriptions on a.Id equals b.AuthorID where b.Tablet_ID == id select a; 

           foreach (var item in data)
           {
               Author a = new Author();
               a.Id = item.Id;
               a.AuthorName = item.AuthorName;
               a.AuthorImage = item.AuthorImage;
               a.BookName = item.BookName;
               li.Add(a);
           }
           return li.GroupBy(a=>a.AuthorName).Select(y=>y.First());
        }

       
        public Tablet[] GetTablets(int id)
        {
            return db.Tablets.Where(a => a.Alphabate_ID == id).ToArray();
        }

        public List<test> GetDesc(int id)
        {
            List<test> li = new List<test>();

            var data = from a in db.Organs join b in db.TabletDescriptions on a.Id equals b.OrganID where b.Tablet_ID == id select new { a, b };

            foreach (var item in data)
            {
                test t = new test();
                t.id = item.b.Id;
                t.organ = item.a.OrganName;
                t.des = item.b.Description;
                t.img = item.b.Image;
                li.Add(t);
            }

            return li;
        }

       
    }

   
}
