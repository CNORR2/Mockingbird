using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskCheckConsole.Class
{
    public enum LogTypes
    {
        DiskCheck,
        TaskScheduller
    }

    public enum InitialisationStatus
    {
        ProgramStarted,
        ProgramCompleted,
        ProgramFailure
    }

}
