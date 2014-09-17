using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVNExtension
{
    public class SVNBuilder
    {
        public static SVNModel AddAction(string Action, SVNModel model)
        {
            switch (Action)
            {
                case "M":
                    model.AddModified();
                    break;
                case "A":
                    model.AddAdd();
                    break;
                case "D":
                    model.AddDeleted();
                    break;
                default:
                    break;
            }
            return model;
        }
    }
}
