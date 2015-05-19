using FluentScheduler;

namespace Makerlab.Tasks
{
    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
            // Schedule an ITask to run at an interval
            Schedule<Fetch3DPrinter>().ToRunNow().AndEvery(5).Seconds();
        }
    } 
}