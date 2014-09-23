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
                    model.AddModified(1);
                    break;
                case "A":
                    model.AddAdd(1);
                    break;
                case "D":
                    model.AddDeleted(1);
                    break;
                default:
                    break;
            }
            return model;
        }

        public static SVNModel AddModel(SVNModel m1, SVNModel m2)
        {
            m2.AddAdd(m1.Add);
            m2.AddDeleted(m1.Deleted);
            m2.AddMerge(m1.Merges);
            m2.AddModified(m1.Modified);
            return m2;
        }
    }
}
