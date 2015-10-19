using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using SQLite.Net.Async;
using System.IO;
using System.Diagnostics;
using SQLite;
using SQLite.Net;
using SQLite.Net.Attributes;
using SQLite.Net.Interop;
using AmecFWUPI.DataBaseModels;
using System.Data;
namespace AmecFWUPI
{

    public class DebugTraceListener : ITraceListener
    {
        public void Receive(string message)
        {
            Debug.WriteLine(message);
        }
    }
    public class DBAccess
    {
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        public static SQLiteAsyncConnection dbAsyncConnection;
        public static SQLiteConnectionWithLock connection;
        const String _dbName = "db.sqlite";

        public DBAccess()
        {
            CreateDB(); //open sqlite database or create if not exists, e.g., first run   
        }

        private void CreateDB()
        {
            string path;

            path = Path.Combine(ApplicationData.Current.LocalFolder.Path, _dbName);

            connection = new SQLiteConnectionWithLock(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), new SQLiteConnectionString(path, false));
            
            connection.TraceListener = new DebugTraceListener();
            dbAsyncConnection = new SQLiteAsyncConnection(() => connection);

        }

        async public Task<bool> dbCreateTableLoginInfoAsync()
        {
            bool success;

            await dbAsyncConnection.CreateTableAsync<LoginInfo>();

            success = true;
            
            return success;
        }

        async public Task<bool> dbCreateTablePoleInfoAsync()
        {
            bool success;
            
            await dbAsyncConnection.CreateTableAsync<PoleInfo>();

            success = true;

            return success;
        }

        

        async public Task<bool> dbUpdateTablePoleInfoAsync(PoleInfo pInfo)
        {

            await dbAsyncConnection.UpdateAsync(pInfo);


            return true;
        }

        async public Task<List<PoleInfo>> dbFetchTasksByDateTablePoleInfoAsync(string taskdate)
        {
            
            List<PoleInfo> l = await ( from s in dbAsyncConnection.Table<PoleInfo>()
                                      where s.dateTaskAdded == taskdate
                                      select s).ToListAsync();
            
            return l;

        }
        
                async public Task<PoleInfo> dbFetchTasksByIdTablePoleInfoAsync(string id)
        {

            int poleid = Convert.ToInt32(id, 10);

            PoleInfo l = await (from s in dbAsyncConnection.Table<PoleInfo>()
                                where s.id == poleid
                                select s).FirstOrDefaultAsync();
            

            return l;

        }

        async public Task<PoleInfo> dbFetchTasksByPoleIdTablePoleInfoAsync(string poleid)
        {

            

            PoleInfo l = await (from s in dbAsyncConnection.Table<PoleInfo>()
                                where s.poleID == poleid
                                select s).FirstOrDefaultAsync();


            return l;

        }

        async public Task<string> dbInsertTableLoginInfoAsync(string username, string password)
        {

            string info;

            LoginInfo l = await (from s in dbAsyncConnection.Table<LoginInfo>()
                                 where s.userName == username
                                 select s).FirstOrDefaultAsync();

            if (l == null)
            {
                await dbAsyncConnection.InsertAsync(new LoginInfo { userName = "shah", password = "shah" });
                await dbAsyncConnection.InsertAsync(new LoginInfo { userName = "admin", password = "1234" });

                info = "inserted successfully";
            }
            else
                info = "already exists";

            return info;
        }

        async public Task<string> dbFetchPasswordTableLoginInfoAsync(string username)
        {

            LoginInfo l = await (from s in dbAsyncConnection.Table<LoginInfo>()
                                 where s.userName == username
                                 select s).FirstOrDefaultAsync();

            return l.password;

        }

       

        private async Task<bool> DoesDbExist(string DatabaseName)
        {
            bool dbexist = true;
            var file = await localFolder.TryGetItemAsync(_dbName) as IStorageFile;


            if (file != null)
            {
                dbexist = true; // The file exists, "file" variable contains a reference to it.
            }
            else
            {
                dbexist = false; // The file doesn't exist.
            }

            return dbexist;
        }

        async public Task<List<PoleInfo>> LoadPoleIDList(string userid=null)
        {


            List<PoleInfo> l = await (from s in dbAsyncConnection.Table<PoleInfo>()
                                      where userid==null
                                      select s
                                      ).ToListAsync();


            return l;

        }
        async public Task<LoginInfo> CheckLogin(string username, string password)
        {


            var l = await (from s in dbAsyncConnection.Table<LoginInfo>()
                                      where s.userName == username && s.password == password
                                      select s
                                      ).FirstOrDefaultAsync();


            return l;

        }

    }
}
