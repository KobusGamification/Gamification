using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Extension;
using DatabaseAccess.Configuration;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Linq;
namespace DatabaseAccess
{
    public class DatabaseUsers
    {

        public DatabaseUsers()
        {

        }
        public void VerifyAndMerge()
        {         
            var dbManager = new DatabaseManager();
            var database = dbManager.GetDatabase();
            var collection = database.GetCollection<IUser>(typeof(IUser).Name);
            var config = (MapUserConfiguration)ConfigurationManager.GetSection("databasemap");
                          
            foreach (UserMap user in config.Users)
            {
                var names = user.SubNames.Split(',');
                foreach (var name in names)
                {
                    var query = Query.EQ("Name", name);                    
                    IUser subUser = collection.FindOne(query);                    
                    if (subUser != null)
                    {
                        query = Query.EQ("Name", user.MainName);
                        IUser mainUser =    collection.FindOne(query);
                        if (mainUser == null)
                        {
                            subUser.Name = user.MainName;
                            mainUser = subUser;
                        }
                        else
                        {
                            foreach (var key in subUser.ExtensionPoint.Keys)
                            {
                                IExtension value = null;
                                if (mainUser.ExtensionPoint.TryGetValue(key, out value))
                                {
                                    var merge = value.Merge(subUser.ExtensionPoint[key]);
                                    mainUser.ExtensionPoint[key] = merge;
                                }
                                else
                                {
                                    mainUser.ExtensionPoint.Add(key, subUser.ExtensionPoint[key]);
                                }
                            }
                        }
                        dbManager.Delete<IUser>(subUser.Id);
                        dbManager.Update<IUser>(mainUser);                        
                    }
                }
            }
        }
    }
}
