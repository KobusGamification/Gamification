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
        public static void PublishFeed(TimeLineIcon icon, string title, string feed)
        {
            var db = new DatabaseManager();
            db.Insert<TimeLineModel>(new TimeLineModel(icon, title, feed));
        }

    }
}
