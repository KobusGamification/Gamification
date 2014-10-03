using Extension;
namespace SVNExtension
{
    public class SVNModel : IExtension 
    {

        public int Merges { get; private set; }
        public int Modified { get; private set; }
        public int Add { get; private set; }
        public int Deleted { get; private set; }
        public int CurrentRevision { get; set; }        

        public SVNModel()
        {
            Merges = 0;
            Modified = 0;
            Add = 0;
            Deleted = 0;
        }

        public void AddMerge(int n)
        {
            Merges += n;
        }

        public void AddModified(int n)
        {
            Modified += n;
        }

        public void AddAdd(int n)
        {
            Add += n;
        }

        public void AddDeleted(int n)
        {
            Deleted += n;
        }

        public IExtension Merge(IExtension model)
        {
            return SVNBuilder.AddModel(this, (SVNModel) model);
        }
    }
}
