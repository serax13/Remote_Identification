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

namespace Identification.Server.Pages
{
    public partial class Index
    {
        [Inject]
        IClientService ClientService { get; set; }
        salym_remote_identityContext db = new salym_remote_identityContext();

        public Client Client { get; set; } = new Client() { Img = new Img() { Pic1 = Array.Empty<byte>(), Pic2 = Array.Empty<byte>(), Pic3 = Array.Empty<byte>() } };

        FluentValidationValidator validator;


        private bool Label_CheckBox;
        private bool tabStatus1 = true;
        private bool tabStatus2 = true;
        private bool license_accepted = false;
        private bool isVisible;
        private bool confirmDialog = false;
        private string fileName1;
        private string fileName2;
        private string fileName3;
        private string fileExeptionForPic1;
        private string fileExeptionForPic2;
        private string fileExeptionForPic3;


        public string[] districts =
        {
            "АК-СУЙСКИЙ", "ЖЕТИ-ОГУЗСКИЙ", "ИССЫК-КУЛЬСКИЙ", "ТОНСКИЙ",
            "ТЮПСКИЙ", "АЛА-БУКИНСКИЙ", "БАЗАР-КОРГОНСКИЙ", "АКСЫЙСКИЙ",
            "НООКЕНСКИЙ", "СУЗАКСКИЙ", "ТОГУЗ-ТОРОУСКИЙ",
            "ТОКТОГУЛЬСКИЙ", "ЧАТКАЛЬСКИЙ", "АК-ТАЛИНСКИЙ", "АТ-БАШЫНСКИЙ", "ДЖУМГАЛЬСКИЙ",
            "КОЧКОРСКИЙ", "НАРЫНСКИЙ", "БАТКЕНСКИЙ", "ЛЕЙЛЕКСКИЙ", "КАДАМЖАЙСКИЙ",
            "АЛАЙСКИЙ", "АРАВАНСКИЙ", "КАРА-СУУСКИЙ", "НООКАТСКИЙ",
            "КАРА-КУЛЖИНСКИЙ", "УЗГЕНСКИЙ", "ЧОН-АЛАЙСКИЙ", "КАРА-БУУРИНСКИЙ",
            "БАКАЙ-АТИНСКИЙ", "МАНАССКИЙ", "ТАЛАССКИЙ", "АЛАМУДУНСКИЙ",
            "ЫСЫК-АТИНСКИЙ", "ЖАЙЫЛСКИЙ", "КЕМИНСКИЙ", "МОСКОВСКИЙ",
            "ПАНФИЛОВСКИЙ", "СОКУЛУКСКИЙ", "ЧУЙСКИЙ", "ЛЕНИНСКИЙ",
            "ОКТЯБРЬСКИЙ", "ПЕРВОМАЙСКИЙ", "СВЕРДЛОВСКИЙ"
        };

        //Custom theme for MatBlazor
        MatTheme theme1 = new MatTheme()
        {
            Primary = "#e0292f",
            Secondary = "#e0292f"
        };

        //Tabs control


        MudTabs tabs;
        MudTabPanel panel01;
        MudTabPanel panel02;
        MudTabPanel panel03;

        public void Activate(MudTabPanel panel)
        {
            tabs.ActivatePanel(panel);
        }

        // Session state management
        protected override async Task OnInitializedAsync()
        {
            var result = await ProtectedSessionStore.GetAsync<Client>("client");
            var result2 = await ProtectedSessionStore.GetAsync<bool>("checkbox");
            //var resultTab1 = await ProtectedSessionStore.GetAsync<bool>("tabstatus1");
            //var resultTab2 = await ProtectedSessionStore.GetAsync<bool>("tabstatus2");
            Client = result.Success ? result.Value : new Client() { Img = new Img() { Pic1 = Array.Empty<byte>(), Pic2 = Array.Empty<byte>(), Pic3 = Array.Empty<byte>() } };
            Label_CheckBox = result2.Success ? result2.Value : false;
            //tabStatus1 = resultTab1.Success ? resultTab1.Value : true;
            //tabStatus2 = resultTab2.Success ? resultTab1.Value : true;
        }

        async Task SaveState()
        {
            await ProtectedSessionStore.SetAsync("client", Client);
        }

        //Google ReCAPTCHA V3.0 validation with token
        private bool IsValidToken(string token, string secretKey)
        {
            var hostURI = $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}";
            System.Net.HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(hostURI);
            request.Method = "GET";
            request.Timeout = 10000;
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

        private async Task CheckClientInn()
        {

            var resValidation = validator.Validate(options => options.IncludeRuleSets("Inn"));
            if (resValidation == true && IsValidToken(Client.Token, "6Le6HoAaAAAAAGObOWqHsUwTXwtBaV-xJ4pYGup0"))
            {
                OpenOverlay();
                var checkFromDb = await db.ClientViews.AnyAsync(c => c.KlInn == Client.PassportInn);
                if (checkFromDb == true)
                    ChangePositionSnackBar("Клиент найден!", Defaults.Classes.Position.BottomCenter);
                else
                    await OpenDialogForCheck("Ок", "Клиент не найден!\nНеобходимо ввести паспортные данные", "Подтвердить действие");
            }
            else
                Log.Warning(Client.Id.ToString() + ":" + " Инн не заполнен корректно!");

        }

        //Validation

        private async Task PartialSubmitValidForm1()
        {
            bool result;
            result = !Label_CheckBox ?
                validator.Validate(options => options.IncludeRuleSets("Passport", "PassportAdress")) :
                validator.Validate(options => options.IncludeRuleSets("Passport"));

            string info = !Label_CheckBox ? "Паспортные данные с пропиской заполнены!" : "Паспортные данные заполнены!";
            Log.Information(Client.Id.ToString() + ": " + Client.FirstName + " " + Client.LastName + " " + info);

            if (result)
            {
                tabStatus1 = false;
                await Task.Delay(300);
                tabs.ActivatePanel(panel02);
                await ProtectedSessionStore.SetAsync("tabstatus1", tabStatus1);
            }
            else
                Log.Warning(Client.Id.ToString() + ":" + " Паспортные данные не заполнены!");

        }


        async Task PartialSubmitValidForm2()
        {
            var result = validator.Validate(options => options.IncludeRuleSets("Generals"));

            if (result == true)
            {
                Log.Information(Client.Id.ToString() + ": " + Client.FirstName + " " + Client.LastName + " " + " Общие сведения заполнены!");
                tabStatus2 = false;
                await Task.Delay(300);
                tabs.ActivatePanel(panel03);
                await ProtectedSessionStore.SetAsync("tabstatus2", tabStatus2);
            }
            else
                Log.Warning(Client.Id.ToString() + ":" + " Общие сведения не заполнены!");

        }

        private async Task<bool> PartialSubmitValidForm3()
        {
            bool result;
            result = !Label_CheckBox ?
                validator.Validate(options => options.IncludeAllRuleSets()) :
                validator.Validate(options => options.IncludeRuleSets("Passport", "Generals").IncludeRulesNotInRuleSet());

            await ProtectedSessionStore.SetAsync("checkbox", Label_CheckBox);

            if (result)
            {
                Client.Token = await JsRuntime.InvokeAsync<string>(identifier: "runCaptcha");
                return true;
            }

            Log.Error(Client.Id.ToString() + ":" + "Данные не отправлены!");
            return false;
        }

        private async Task ProcessResult()
        {
            bool isForm3Valid = await PartialSubmitValidForm3();

            if (isForm3Valid && IsValidToken(Client.Token, "6Le6HoAaAAAAAGObOWqHsUwTXwtBaV-xJ4pYGup0"))
            {
                Log.Information(Client.Id.ToString() + ": " + Client.FirstName + " " + Client.LastName + " " + "Отправка токена");
                await OpenDialog();
            }
            else
            {
                Log.Error(Client.Id.ToString() + ":" + "Данные не отправлены!");
                //NavigationManager.NavigateTo("/Errors");
            }

            if (license_accepted)
                await SendApplication();
            else
                Log.Error(Client.Id + ":" + "Данные не отправлены!");
        }

        private async Task SendApplication()
        {
            try
            {
                OpenOverlay();
                Client.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                Client.Status = 3;
                //StartTimerForProgressBar();
                //ProgressStatus = true;

                await ClientService.AddClient(Client);

                Log.Information(Client.Id.ToString() + ": " + Client.FirstName + " " + Client.LastName + " " + "Данные с формы отправлены!");
                Client = new Client();
                Client.Img = new Img();
                fileName1 = string.Empty;
                fileName2 = string.Empty;
                fileName3 = string.Empty;
                tabStatus1 = true;
                tabStatus2 = true;
                confirmDialog = false;

                await ProtectedSessionStore.DeleteAsync("client");
                ChangePositionSnackBar("Данные отрправлены!", Defaults.Classes.Position.TopCenter);
            }
            catch (HttpRequestException exp)
            {
                string text = exp.ToString();
                ChangePositionAlertSnackBar("Сервер не доступен, повторите попытку позже!", Defaults.Classes.Position.TopCenter);
                Log.Error("Error message: " + text + "; From source: " + exp.Source);
            }
            catch (Exception e)
            {
                Log.Error("Error message: " + e.Message + "; From source: " + e.Source);
            }
        }

        private async Task<IEnumerable<string>> Search1(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return districts;
            return districts.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }


        void ChangePositionSnackBar(string message, string position)
        {
            Snackbar.Clear();
            Snackbar.Configuration.PositionClass = position;
            Snackbar.Add(message, MudBlazor.Severity.Success);
        }

        void ChangePositionAlertSnackBar(string message, string position)
        {
            Snackbar.Clear();
            Snackbar.Configuration.PositionClass = position;
            Snackbar.Add(message, MudBlazor.Severity.Error);
        }

        //Dialog
        private async Task OpenDialogForCheck(string BtnText, string ContentText, string HeaderText)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", ContentText);
            parameters.Add("ButtonText", BtnText);
            parameters.Add("Color", Color.Error);
            parameters.Add("Type", "Default");

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Small };
            var result = await DialogService.Show<Dialog>(HeaderText, parameters, options).Result;
            if (!result.Cancelled)
            {
                confirmDialog = (bool)(result.Data ?? false);
            }
            //return res;
        }
        private async Task OpenDialog()
        {
            var result = await DialogService.Show<DialogForLicense>("Согласие субъекта на сбор, обработку и передачу персональных данных").Result;

            if (!result.Cancelled)
            {
                license_accepted = (bool)(result.Data ?? false);
            }
        }

        // ProgressBar
        public int ValueProgressBar { get; set; }

        public async void StartTimerForProgressBar()
        {
            if (disposed)
                return;
            ValueProgressBar = 0;
            while (ValueProgressBar < 100)
            {
                ValueProgressBar = ValueProgressBar + 5;
                StateHasChanged();
                await Task.Delay(200);
                if (disposed)
                    return;
            }

        }

        bool disposed;
        public void Dispose()
        {
            disposed = true;
        }

        //Image Upload to folder

        //private static string CreateGuid(string Newguid = null)
        //{
        //    if (Newguid == null)
        //    {
        //        Newguid = Guid.NewGuid().ToString();
        //    }
        //    return Newguid;
        //}

        //public DirectoryInfo CreateDir()
        //{
        //    if (Myguid == null)
        //    {
        //        Myguid = CreateGuid();
        //    }
        //    var relativePath = Path.Combine("uploads", Myguid);
        //    var dirToSave = Path.Combine(@"C:\Users\Scoi\source\repos\remote_identification\Identification.Panel\wwwroot\", relativePath);
        //    return new DirectoryInfo(dirToSave);
        //}

        private async Task<byte[]> SaveFile(IMatFileUploadEntry[] files)
        {

            //var di = CreateDir();
            //if (!di.Exists)
            //{
            //    di.Create();
            //}
            var file = files.FirstOrDefault();
            var ms = new MemoryStream();
            //var filePath = Path.Combine(di.FullName, file.Name);
            //var pathToDb = Path.Combine("uploads", Myguid, file.Name);
            if (file != null)
            {
                await file.WriteToStreamAsync(ms);
                //await File.WriteAllBytesAsync(filePath, ms.ToArray());
            }

            return ms.ToArray();

        }

        async Task OnInputFileChange1(IMatFileUploadEntry[] e)
        {
            try
            {
                var file = e.FirstOrDefault();
                if (!file.Type.Contains("png") && !file.Type.Contains("jpg") && !file.Type.Contains("jpeg"))
                {
                    fileExeptionForPic1 = "Неверный формат файла!";
                    return;
                }
                else
                {
                    fileExeptionForPic1 = string.Empty;
                    var res = await SaveFile(e);
                    Client.Img.Pic1 = res;
                    fileName1 = file.Name;
                }

            }
            catch (Exception ex)
            {
                Log.Error(Client.Id.ToString() + ": " + Client.FirstName + " " + Client.LastName + " " + "Ошибка заргузки! " + ex.Message);
                throw;
            }
        }

        async Task OnInputFileChange2(IMatFileUploadEntry[] e)
        {
            try
            {
                var file = e.FirstOrDefault();
                if (!file.Type.Contains("png") && !file.Type.Contains("jpg") && !file.Type.Contains("jpeg"))
                {
                    fileExeptionForPic2 = "Неверный формат файла!";
                    return;
                }
                else
                {
                    fileExeptionForPic2 = string.Empty;
                    var res = await SaveFile(e);
                    Client.Img.Pic2 = res;
                    fileName2 = file.Name;
                }

            }
            catch (Exception ex)
            {
                Log.Error(Client.Id.ToString() + ": " + Client.FirstName + " " + Client.LastName + " " + "Ошибка заргузки! " + ex.Message);
                throw;
            }
        }

        async Task OnInputFileChange3(IMatFileUploadEntry[] e)
        {
            try
            {
                var file = e.FirstOrDefault();
                if (!file.Type.Contains("png") && !file.Type.Contains("jpg") && !file.Type.Contains("jpeg"))
                {
                    fileExeptionForPic3 = "Неверный формат файла!";
                    return;
                }
                else
                {
                    fileExeptionForPic3 = string.Empty;
                    var res = await SaveFile(e);
                    Client.Img.Pic3 = res;
                    fileName3 = file.Name;
                }

            }
            catch (Exception ex)
            {
                Log.Error(Client.Id.ToString() + ": " + Client.FirstName + " " + Client.LastName + " " + "Ошибка заргузки! " + ex.Message);
                throw;
            }
        }

        public async void OpenOverlay()
        {
            isVisible = true;
            await Task.Delay(3000);
            isVisible = false;
            StateHasChanged();
        }
    }
}
