using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVNExtension.Model
{
    public class SVNRepository
    {
        public ObjectId Id { get; set; }
        public string Url { get; private set; }
        public int CurrentVersion { get; set; }

        public SVNRepository(string url)
        {
            Url = url;            
        }

    }
}
