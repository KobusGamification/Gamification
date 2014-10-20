using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
namespace Extension.API.Timeline.Model
{
    public class TimeLineModel
    {
        private ObjectId id { get; set; }
        public string Icon { get; private set; }
        public string Title { get; private set; }
        public string Feed { get; private set; }

        public TimeLineModel(TimeLineIcon icon, string title, string feed)
        {            
            Icon = GetIcon(icon);
            Title = title;
            Feed = feed;
        }

        private string GetIcon(TimeLineIcon icon)
        {
            switch (icon)
            {
                case TimeLineIcon.Badge:
                    return "badge";
                case TimeLineIcon.Danger:
                    return "danger";
                case TimeLineIcon.Info:
                    return "info";
                case TimeLineIcon.Success:
                    return "success";
                case TimeLineIcon.Warning:
                    return "warning";
                default:
                    return "";
            }
        }
    }
}
