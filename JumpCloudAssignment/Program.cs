using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JumpCloudAssignment.Services;

namespace JumpCloudAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new ActionService();

            var errorStr = "";
            foreach (var arg in args)
            {
                errorStr = service.AddAction(arg);
                if (false == string.IsNullOrEmpty(errorStr))
                    break;
            }

            if (false == string.IsNullOrEmpty(errorStr))
            {
                Console.WriteLine($"ERROR: Unable to add one or more actions -> {errorStr}");
                return;
            }

            var stats = service.GetStats();

            Console.WriteLine(stats);

            return;
        }
    }
}
