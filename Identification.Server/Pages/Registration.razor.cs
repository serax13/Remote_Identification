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
    partial class Registration
    {
        salym_remote_identityContext db1 = new salym_remote_identityContext();
        i_klsContext db2 = new i_klsContext();

        EditForm editForm;
        bool isVisible;
        bool isShow;
        bool isOpenPopup;
        bool nextStep;
        bool btnStatus = true;
        bool btnDisabled;
        private bool confirmDialog = false;
        string[] errors = { };
        private static Random random = new Random();
        MudTextField<string> pwField1;
        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.Visibility;



        ForRegistrationExistingClient cl = new ForRegistrationExistingClient();

        protected override async Task OnInitializedAsync()
        {
            var result = await ProtectedSessionStore.GetAsync<ForRegistrationExistingClient>("register");
            cl = result.Success ? result.Value : new ForRegistrationExistingClient();

        }

        void OpenSnackBar(string message, string position, MudBlazor.Severity severity)
        {
            Snackbar.Clear();
            Snackbar.Configuration.PositionClass = position;
            Snackbar.Add(message, severity);
        }

        //Dialog
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

        public void ToggleOpen()
        {
            if (isOpenPopup)
                isOpenPopup = false;
            else
                isOpenPopup = true;
        }
        async Task SaveState()
        {
            await ProtectedSessionStore.SetAsync("register", cl);
        }

        public static string HashPassword(string login, string password)
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

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //Google ReCAPTCHA V3.0 validation with token
        private bool IsValidTokenGoogleReCaptcha(string token, string secretKey)
        {
            //string proxy = "192.168.0.253";
            //int port = 3128;
            //WebProxy myproxy = new WebProxy(proxy, port);
            var hostURI = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}";
            System.Net.HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(hostURI);
            request.Method = "GET";
            request.Timeout = 10000;
            //request.Proxy = myproxy;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                var JSONres = reader.ReadToEnd();
                var message = JsonConvert.DeserializeObject<dynamic>(JSONres);
                reader.Close();
                dataStream.Close();
                if (message.score <= 0.5)
                {
                    return false;
                }
                else
                    return true;
            }
        }

        public async void OpenOverlay()
        {
            isVisible = true;
            await Task.Delay(3000);
            isVisible = false;
            StateHasChanged();
        }

        private async Task NewAccount(int? klkod, string fio, string phone)
        {
            ILogin account = new ILogin();
            //string pass = RandomString(8);
            string hash = HashPassword(cl.passportInn.Trim(), cl.Password.Trim());

            account.KlLogin = cl.passportInn;
            account.KlPassword = hash;
            account.KlKod = klkod;
            account.Fio = fio;
            account.OtLevel = 9;
            account.OtDate = DateTime.Now;
            account.KlLevel = 3;
            account.SetBlock = 0;
            account.OtKodkr = 1;
            account.FirstEnter = 1;
            account.IsIb = 1;
            account.Pin = "123456";
            account.NumPodp = 0;
            account.IsMobile = 0;
            account.IsIb = 1;
            account.Otv = 0;
            account.KlPhone = phone.Trim();
            db2.ILogins.Add(account);
            await db2.SaveChangesAsync();
        }

        private async Task<bool> CheckCred()
        {
            if (cl.TypeCode == "Кредит")
                return await db1.CheckDogkrViews.AnyAsync(c => c.KlInn == cl.passportInn && c.DgPozn == int.Parse(cl.codeNumber));
            if (cl.TypeCode == "Депозит")
                return await db1.CheckDogvkViews.AnyAsync(c => c.KlInn == cl.passportInn && c.DvPozn == int.Parse(cl.codeNumber));
            else
                return false;
        }

        private async Task<bool> CheckNumber()
        {
            if (cl.TypeCode == "Кредит")
                return await db1.CheckDogkrViews.AnyAsync(c => c.KlTel1 == "996" + cl.Number.Trim());
            if (cl.TypeCode == "Депозит")
                return await db1.CheckDogvkViews.AnyAsync(c => c.KlTel1 == "996" + cl.Number.Trim());
            else
                return false;
        }



        private async Task OnValidSubmit(EditContext context)
        {
            OpenOverlay();
            btnStatus = false;
            var token = await JsRuntime.InvokeAsync<string>(identifier: "runCaptcha");
            var checkFromDb = await CheckCred();
            var checkIsExist = await db2.ILogins.AnyAsync(c => c.KlLogin == cl.passportInn);
            //var checkNumber = await CheckNumber();
            if (IsValidTokenGoogleReCaptcha(token, "6Le6HoAaAAAAAGObOWqHsUwTXwtBaV-xJ4pYGup0"))
            {
                //if(checkIsExist)
                //    await OpenDialogForCheck("Войти", "Данный клиент уже существует, войдите в личный кабинет!", "Подтвердить действие", "RedirectLogin");

                //if (checkFromDb && checkNumber)
                if (checkFromDb && !checkIsExist)
                {

                    OpenSnackBar("Клиент найден!", Defaults.Classes.Position.TopCenter, MudBlazor.Severity.Success);
                    nextStep = true;
                    btnStatus = false;

                }
                else if (!checkFromDb)
                {

                    await OpenDialogForCheck("Ок", "Клиент не найден!\nПроверьте паспортные данные и код кредита или депозита. Номер телефона для поддержки 0555 781 556", "Подтвердить действие", "Default");
                    btnStatus = true;
                }
                else if (checkIsExist)
                {


                    await OpenDialogForCheck("Войти", "Данный клиент уже существует, войдите в личный кабинет!", "Подтвердить действие", "RedirectLogin");
                    btnStatus = true;
                }

            }
            StateHasChanged();
        }

        private async Task CreateAccount()
        {
            OpenOverlay();
            try
            {
                if (cl.TypeCode == "Кредит")
                {
                    var data = db1.CheckDogkrViews.Where(c => c.KlInn == cl.passportInn).Select(c => new { c.DgKodkl, c.KlNam, c.KlTel1 }).FirstOrDefault();
                    await NewAccount(data.DgKodkl, data.KlNam, data.KlTel1);
                }
                if (cl.TypeCode == "Депозит")
                {
                    var data = db1.CheckDogvkViews.Where(c => c.KlInn == cl.passportInn).Select(c => new { c.DvKodkl, c.KlNam, c.KlTel1 }).FirstOrDefault();
                    await NewAccount(data.DvKodkl, data.KlNam, data.KlTel1);
                }

                //await OpenDialogForCheck()


                OpenSnackBar("Аккаунт создан! Будет выполнен переход на страницу входа", Defaults.Classes.Position.TopCenter, MudBlazor.Severity.Success);
                cl = new ForRegistrationExistingClient();
                await ProtectedSessionStore.DeleteAsync("register");
                await Task.Delay(3000);
                NavigationManager.NavigateTo("https://ib.salymfinance.kg/");
            }
            catch (Exception e)
            {
                OpenSnackBar("Заполните все поля!", Defaults.Classes.Position.TopCenter, MudBlazor.Severity.Error);
            }
        }

        private IEnumerable<string> PasswordStrength(string pw)
        {
            if (string.IsNullOrWhiteSpace(pw))
            {
                yield return "Необходимо ввести пароль!";
                yield break;
            }
            if (pw.Length < 6)
                yield return "Длина пароля должна быть минимум 6 символов";
            //if (!Regex.IsMatch(pw, @"[A-Z]"))
            //    yield return "Пароль должен содержать буквы верхнего регистра";
            if (!Regex.IsMatch(pw, @"[a-z]"))
                yield return "Пароль должен содержать буквы";
            if (!Regex.IsMatch(pw, @"[0-9]"))
                yield return "Пароль должен содержать цифры";
        }

        private string PasswordMatch(string arg)
        {
            if (pwField1.Value != arg)
                return "Введеные пароли не совпадают!";
            return null;
        }

        private void ShowPassword()
        {
            if (isShow)
            {
                isShow = false;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Password;
            }
            else
            {
                isShow = true;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Text;
            }
        }
    }
}
