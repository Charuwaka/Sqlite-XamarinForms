using Acr.UserDialogs;
using Android.OS;
using EntryProject.Model;
using Java.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using Java.Nio.Channels;
using Android.Widget;

namespace EntryProject
{
	public partial class MainPage : ContentPage
	{
        
        public MainPage()
		{

            InitializeComponent ();
            

           
        }

        private void SaveData(object sender, EventArgs e)
        {
            try
            {
                Task.Run(async () =>
                {
                    var employee = new Employee
                    {
                        Name = "Andrew",
                        LastName = "Programmer"
                    };

                    var duty1 = new Duty()
                    {
                        Description = "Project A Management",
                        Deadline = new DateTime(2017, 10, 31)
                    };

                    var duty2 = new Duty()
                    {
                        Description = "Reporting work time",
                        Deadline = new DateTime(2022, 12, 31)
                    };

                    int success = 0;
                    success= App.Database.SaveStudentData(employee);
                    if (success==1)
                    {
                        System.Diagnostics.Debug.WriteLine("Employee Data Inserted Successfully");
                    }
                    success= App.Database.SaveStudentItemAsyn(duty1);
                    if (success == 1)
                    {
                        System.Diagnostics.Debug.WriteLine("Duty-1 Data Inserted Successfully");
                    }
                    success = App.Database.SaveStudentItemAsyn(duty2);
                    if (success == 1)
                    {
                        System.Diagnostics.Debug.WriteLine("Duty-2 Data Inserted Successfully");
                    }

                    employee.Duties = new List<Duty> { duty1, duty2 };
                    success=  App.Database.UpdateData(employee);
                    if (success == 1)
                    {
                        System.Diagnostics.Debug.WriteLine("Employee Data Updated Successfully");
                    }

                    var employeeStored =App.Database.GetItemsAsync();
                    if (employeeStored!=null && employeeStored.Duties!=null)
                    {
                        System.Diagnostics.Debug.WriteLine("Employee Data Fetched Successfully");
                    }
                });
            }
            catch (Exception exx)
            {

               //
            }
          
        }

        private async void ExportData(object sender, EventArgs e)
        {
            await Task.Run(async () =>
             {
                 await ExportDB();
             });
        }

        private static async Task ExportDB()
        {
            try
            {
                File sd = Android.OS.Environment.ExternalStorageDirectory;
                File data = Android.OS.Environment.DataDirectory;
                if (sd.CanWrite())
                {
                    String currentDBPath = "/user/0/com.companyname/files/.local/share/TodoSQLite.db3";
                    String backupDBPath = "BackupHorarios";
                    File currentDB = new File(data, currentDBPath);
                    File backupDB = new File(sd, backupDBPath);
                    if (currentDB.Exists())
                    {
                        FileChannel SourcePath = new FileInputStream(currentDB).Channel;
                        FileChannel DestinationPath = new FileOutputStream(backupDB).Channel;
                        await DestinationPath.TransferFromAsync(SourcePath, 0, SourcePath.Size());
                        SourcePath.Close();
                        DestinationPath.Close();
                    }
                }

            }
            catch (Exception e)
            {
            }
                
        }
    }
}

