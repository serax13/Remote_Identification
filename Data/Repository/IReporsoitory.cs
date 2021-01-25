using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_App.Data.Repository
{
    public interface IReporsoitory
    {
        public void AddToDB(Client cc, Img ic);
        void DeleteImg(int id);
    }
}
