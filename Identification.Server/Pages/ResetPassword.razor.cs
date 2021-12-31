using System;
using System.Collections.Generic;
using Identification.Models;
using Identification.Server.Data;
using Identification.Server.Services;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using MatBlazor;
using MudBlazor;
using Microsoft.AspNetCore.Components.Forms;
using FluentValidation;
using Identification.Server.Validation;
using Microsoft.JSInterop;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Net.Http;
using Identification.Panel.Shared.Components;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Security.Cryptography;
using System.Threading;

namespace Identification.Server.Pages
{
    partial class ResetPassword
    {
        salym_remote_identityContext db1 = new salym_remote_identityContext();
        i_klsContext db2 = new i_klsContext();
        calimContext calimContext = new calimContext();
        SmsSend sms = new SmsSend();
        bool Success;
        private bool confirmDialog = false;
        MudForm form;
        string Inn;
        string TypeCode;
        string CodeNumber;
        string ResultNumber;
        string NewPassword;

        private static string HashPassword(string login, string password)
        {
            string hashedPass = "";
            var md5 = new MD5CryptoServiceProvider();
            var data = Encoding.UTF8.GetBytes(login.Trim() + password.Trim());
            var md5data = md5.ComputeHash(data);
            for (int i = 0; i < md5data.Length; i++)
            {
                hashedPass += md5data[i].ToString("x").PadLeft(2, '0').ToUpper();
            }
            return hashedPass;
        }
        private async Task<bool> CheckInn(string inn)
        {
            return await db2.ILogins.AnyAsync(c => c.KlLogin == inn);
        }

        private async Task<bool> CheckCred()
        {
            if (TypeCode == "Кредит")
                return await db1.CheckDogkrViews.AnyAsync(c => c.KlInn == Inn && c.DgPozn == int.Parse(CodeNumber));
            if (TypeCode == "Депозит")
                return await db1.CheckDogvkViews.AnyAsync(c => c.KlInn == Inn && c.DvPozn == int.Parse(CodeNumber));
            else
                return false;
        }

        private async Task SubmitAsync()
        {
            if (await CheckInn(Inn))
            {
                if (await CheckCred())
                {
                    var Data = await db1.ClientViews.Where(c => c.KlInn == Inn.Trim()).Select(c => new { c.KlTel1, c.KlKod }).FirstOrDefaultAsync();
                    ResultNumber = "996 *** ** ** " + Data.KlTel1.Substring(10, 2);
                    await OpenDialogForCheck("Ок", "На следующий номер " + ResultNumber + " будет отправлен новый пароль", "Подтвердить действие", "Default");
                    if (confirmDialog)
                    {
                        NewPassword = RandomPass();
                        sms.KlKod = Data.KlKod;
                        sms.Phone = Data.KlTel1.Trim();
                        sms.Sms = "Ваш новый пароль: " + NewPassword.Trim();
                        sms.Status = 0;
                        await calimContext.SmsSends.AddAsync(sms);
                        await calimContext.SaveChangesAsync();
                        await UpdatePassword(Data.KlKod, Inn, NewPassword);
                        NavigationManager.NavigateTo("https://ib.salymfinance.kg/");
                    }
                }
                else
                {
                    await OpenDialogForCheck("Ок", "Клиент с таким кодом кредита/депозита не найден!\nПроверьте введенные данные. Номер телефона для поддержки 0555 781 556", "Подтвердить действие", "Default");
                    confirmDialog = false;
                }
            }
            
            else
            {
                await OpenDialogForCheck("Ок", "Клиент не найден!\nПроверьте паспортные данные. Номер телефона для поддержки 0555 781 556", "Подтвердить действие", "Default");
                confirmDialog = false;
            }
                
        }
        private string RandomPass()
        {
            Random generator = new Random();
            return generator.Next(0, 1000000).ToString("D6");
        }
        private async Task UpdatePassword(int? klkod, string inn, string newPass)
        {
            //string pass = RandomString(8);
            string hash = HashPassword(inn, newPass);

            var UpdatedUserPassword = await db2.ILogins.Where(c => c.KlKod == klkod).FirstOrDefaultAsync();
            UpdatedUserPassword.KlPassword = hash;
            UpdatedUserPassword.SetBlock = 0;
            UpdatedUserPassword.FirstEnter = 0;
            await db2.SaveChangesAsync();
        }

        private async Task OpenDialogForCheck(string BtnText, string ContentText, string HeaderText, string Type)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", ContentText);
            parameters.Add("ButtonText", BtnText);
            parameters.Add("Color", Color.Error);
            parameters.Add("Type", Type);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small };
            var result = await DialogService.Show<Dialog>(HeaderText, parameters, options).Result;
            if (!result.Cancelled)
            {
                confirmDialog = (bool)(result.Data ?? false);
            }
            //return res;
        }
    }
}
