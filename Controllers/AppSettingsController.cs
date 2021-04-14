using AdminServicesGBO.Models.BLL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace AdminServicesGBO.Controllers
{

    public class AppSettingsController : Controller
    {

        private readonly IWebHostEnvironment webHostingEnvironment;
        public AppSettingsController(IWebHostEnvironment webHostingEnvironment)
        {
            this.webHostingEnvironment = webHostingEnvironment;
        }

        [Route("AppSettings/Backup")]
        public FileResult Backup()
        {
            try
            {

                var PathDatabaseZip = Path.Combine(Directory.GetCurrentDirectory(), "Backup", "DSS-GBO-Database_" + DateTime.Now.ToLongDateString() + ".zip");
                var PathFilesZip = Path.Combine(Directory.GetCurrentDirectory(), "Backup", "DSS-GBO-Documents_" + DateTime.Now.ToLongDateString() + ".zip");
                var webRootResult = Path.Combine(Directory.GetCurrentDirectory(), "UploadDocument", "DSS-GBO.zip");
                string PathFiles = Path.Combine(Directory.GetCurrentDirectory(), "Mails");

                var PathDB = BackupDB();
                string Backup = Path.Combine(Directory.GetCurrentDirectory(), "Backup");
                ZipFile.CreateFromDirectory(PathDB, PathDatabaseZip);
                ZipFile.CreateFromDirectory(PathFiles, PathFilesZip);
                if (Directory.Exists(PathDB))
                {
                    Directory.Delete(PathDB, true);
                }
                ZipFile.CreateFromDirectory(Backup, webRootResult);
                byte[] finalResult = System.IO.File.ReadAllBytes(webRootResult);
                System.Diagnostics.Debug.WriteLine("Backup17");
                if (System.IO.File.Exists(webRootResult))
                {
                    System.Diagnostics.Debug.WriteLine("Backup18");
                    //System.IO.File.Delete(PathDatabaseZip);
                    //System.IO.File.Delete(PathFilesZip);
                    //System.IO.File.Delete(webRootResult);
                    System.Diagnostics.Debug.WriteLine("Backup19");
                }
                System.Diagnostics.Debug.WriteLine("Backup20");
                if (finalResult == null || !finalResult.Any())
                {
                    System.Diagnostics.Debug.WriteLine("Backup21");
                    throw new Exception(String.Format("Rien n'a été trouvé"));
                }
                System.Diagnostics.Debug.WriteLine("Backup22");
                FileContentResult res = File(finalResult, "application/zip");
                return res;
                //return Json(new { success = true, message = "Sauvegarder avec success", data =  });
            }
            catch (Exception ex)
            {
                return null;
                //return Json(new { success = false, message = "Error server " + ex.Message });
            }
        }

        private string BackupDB()
        {
            string backupDIR = Path.Combine(Directory.GetCurrentDirectory(), "Backup", "DSS-GBO-Database");
            if (!Directory.Exists(backupDIR))
            {
                Directory.CreateDirectory(backupDIR);
            }
            string path = backupDIR + "\\GBO-Database_" + DateTime.Now.ToLongDateString() + ".txt";
            string result = BLL_Diagnostic.Backup(path);
            if (result != null)
                return backupDIR;
            else
                throw new Exception(result);
        }

        public string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            System.Diagnostics.Debug.Write("tempDirectory=" + tempDirectory);
            return tempDirectory;
        }
    }
}
