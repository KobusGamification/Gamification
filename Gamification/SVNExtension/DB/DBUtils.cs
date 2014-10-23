using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseAccess;
using MongoDB.Driver.Builders;
using SVNExtension.Model;
using Extension;
namespace SVNExtension.DB
{
    public class DBUtils
    {
        public static bool ReposExists(string url)
        {
            var exists = false;
            var result = GetRepository(url);   
            if (result != null)
            {
                exists = true;
            }

            return exists;
        }

        public static SVNRepository GetRepository(string url)
        {
            SVNRepository repo = null;
            var manager = new DatabaseManager();
            var database = manager.GetDatabase();
            var query = Query.EQ("Url", url);
            var collection = database.GetCollection<SVNRepository>(typeof(SVNRepository).Name);
            repo = collection.FindOne(query);
            return repo;
        }

        public static void UpdateRepositorys(List<SVNRepository> repos)
        {
            var db = new DatabaseManager();
            foreach (var repo in repos)
            {
                db.Update<SVNRepository>(repo);
            }
        }

        public static void InsertUser(IUser user)
        {
            var db = new DatabaseManager();
            db.Insert<IUser>(user);
        }

        public static void UpdateUser(IUser user)
        {
            var db = new DatabaseManager();
            db.Update<IUser>(user);
        }

        internal static bool UserExists(IUser user)
        {
            var exists = false;
            var db = new DatabaseManager();
            var resultUser = GetUser(user.Name);
            if (resultUser != null)
            {
                exists = true;
            }
            return exists;
        }

        internal static IUser GetUser(string name)
        {
            var db = new DatabaseManager();
            var database = db.GetDatabase();
            var collection = database.GetCollection<IUser>(typeof(IUser).Name);
            var query = Query.EQ("Name", name);
            var user = collection.FindOne(query);            
            return user;
        }

        internal static void InsertInfo(SVNInfo info)
        {
            var database = new DatabaseManager();
            database.Insert<SVNInfo>(info);
        }
    }
}
