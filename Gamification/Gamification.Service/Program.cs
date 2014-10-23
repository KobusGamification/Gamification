using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            System.ServiceProcess.ServiceBase.Run(new GamificationService());
        }
    }
}
