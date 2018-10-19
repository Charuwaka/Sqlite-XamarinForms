using System.Collections.Generic;
using System.Threading.Tasks;
using EntryProject.Model;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace Todo
{
	public class TodoItemDatabase
	{
		readonly SQLiteConnection database;
        public TodoItemDatabase(string dbPath)
		{
            database = new SQLiteConnection(dbPath);
			database.CreateTable<Employee>();
            database.CreateTable<Duty>();
        }

		public  Employee GetItemsAsync()
		{
            try
            {
                return  database.GetWithChildren<Employee>(2);
            }
            catch (SQLiteException ex)
            {

                return null;
            }
        }



        public int SaveStudentItemAsyn(Duty _studentItem)
        {
            try
            {
                return  database.Insert(_studentItem);
            }
            catch (SQLiteException ex)
            {

                return 0;
            }
         
        }

        public  int SaveStudentData(Employee _studentdata)
        {
            try
            {
                return database.Insert(_studentdata);
                
            }
            catch (System.Exception ex)
            {

                return 0;
            }
           
        }

        public  int UpdateData(Employee _studentdata)
        {
            try
            {
                database.UpdateWithChildren(_studentdata);
                return 1;
            }
            catch (System.Exception ex)
            {

                return 0;
            }
            
        }
    }
}

