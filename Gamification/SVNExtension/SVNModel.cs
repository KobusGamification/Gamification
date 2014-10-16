using Extension;
namespace SVNExtension
{
    public class SVNModel : IExtension 
    {        
        public int Modified { get; private set; }
        public int Add { get; private set; }
        public int Deleted { get; private set; }        

        public SVNModel()
        {            
            Modified = 0;
            Add = 0;
            Deleted = 0;
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
