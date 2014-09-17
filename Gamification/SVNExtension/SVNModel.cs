using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExtension;
namespace SVNExtension
{
    public class SVNModel
    {

        public int Merges { get; private set; }
        public int Modified { get; private set; }
        public int Add { get; private set; }
        public int Deleted { get; private set; }

        private int one = 1;

        public SVNModel()
        {
            Merges = 0;
            Modified = 0;
            Add = 0;
            Deleted = 0;
        }

        public void AddMerge()
        {
            Merges += one;
        }

        public void AddModified()
        {
            Modified += one;
        }

        public void AddAdd()
        {
            Add += one;
        }

        public void AddDeleted()
        {
            Deleted += one;
        }
    }
}
