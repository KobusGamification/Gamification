using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extension.API.Timeline.Model;
using DatabaseAccess;
namespace Extension.API.Timeline
{
    public class TimeLine
    {    
        static log4net.ILog log = log4net.LogManager.GetLogger(typeof(TimeLine));
        public static void PublishFeed(TimeLineIcon icon, string title, string feed)
        {
            var db = new DatabaseManager();
            log.InfoFormat("Inserting feed : {0} -> {1}", title, feed);
            db.Insert<TimeLineModel>(new TimeLineModel(icon, title, feed));
        }

    }
}
