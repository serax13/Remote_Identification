using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Identity_App.Data.Repository
{
    public class SqlRepository : IReporsoitory
    {
        private readonly IdentityDbContext _context;


        public SqlRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public void AddToDB(Client cc, Img ic)
        {
            /*
            cc.FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cc.FirstName).Trim();
            cc.LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cc.LastName).Trim();
            cc.MidName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cc.MidName).Trim();
            cc.Number1 = "996" + cc.Number1.Trim();
            cc.Number2 = "996" + cc.Number2.Trim();
            cc.Satus = 0;
            */

            _context.SavedClients.Add(cc);
            _context.SavedImg.Add(ic);
            _context.SaveChanges();
        }

        public void DeleteImg(int id)
        {
            var deletedItem = _context.SavedImg.Find(id);
            if(deletedItem != null)
            {
                _context.SavedImg.Remove(deletedItem);
                _context.SaveChanges();
            }
           
        }
    }
}
