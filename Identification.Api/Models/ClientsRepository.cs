using Identification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Identification.Api.Models
{
    public class ClientsRepository : IClientRepository
    {
       
        private readonly IdentityDbContext identityDbContext;

        public ClientsRepository (IdentityDbContext identityDbContext)
        {
            //logger.Debug("Подключение к базе!");
            this.identityDbContext = identityDbContext;
        }

        public async Task<Client> AddClient(Client cc)
        {
            //logger.Debug("Запись");
            cc.FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cc.FirstName).Trim();
            cc.LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cc.LastName).Trim();
            cc.MidName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cc.MidName).Trim();
            cc.FIO = cc.LastName + " " + cc.FirstName + " " + cc.MidName;
            cc.Number1 = "996" + cc.Number1.Trim();
            cc.Number2 = "996" + cc.Number2.Trim();
            var result = await identityDbContext.SavedClients.AddAsync(cc);
            await identityDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Client> DeleteClient(int id)
        {
            var result = await identityDbContext.SavedClients.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                identityDbContext.SavedClients.Remove(result);
                await identityDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<List<Client>> GetAll()
        {
            return await identityDbContext.SavedClients.ToListAsync();
        }

        public async Task<Client> GetFromDb(int clId)
        {
            return await identityDbContext.SavedClients.Include(x => x.Img).FirstOrDefaultAsync(e => e.Id == clId);
        }

        public async Task<Client> UpdateClient(Client cc)
        {
            var result = await identityDbContext.SavedClients.FirstOrDefaultAsync(e => e.Id == cc.Id);
            if(result != null)
            {
                result.FirstName = cc.FirstName;
                result.LastName = cc.LastName;
                result.MidName = cc.MidName;
                result.Number1 = cc.Number1;
                result.Number2 = cc.Number2;
                result.Status = cc.Status;
                result.Birthday = cc.Birthday;
                result.Gender = cc.Gender;
                result.MartialStatus = cc.MartialStatus;
                result.PassportSeries = cc.PassportSeries;
                result.PassportNumber = cc.PassportNumber;
                result.PassportInn = cc.PassportInn;
                result.PassportIssuedBy = cc.PassportIssuedBy;
                result.PassportDate = cc.PassportDate;
                result.PassportEndDate = cc.PassportEndDate;
                result.Region = cc.Region;
                result.District = cc.District;
                result.City = cc.City;
                result.Street = cc.Street;
                result.HouseNumber = cc.HouseNumber;
                result.AparmentNumber = cc.AparmentNumber;
                result.PassportRegion = cc.PassportRegion;
                result.PassportDistrict = cc.PassportDistrict;
                result.PassportCity = cc.PassportCity;
                result.PassportHouseNumber = cc.PassportHouseNumber;
                result.PassportAparmentNumber = cc.PassportAparmentNumber;

                await identityDbContext.SaveChangesAsync();

                return result;
            }
            return null;
        }
    }
}
