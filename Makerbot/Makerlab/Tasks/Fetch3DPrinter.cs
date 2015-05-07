using FluentScheduler;
using Newtonsoft.Json;
using Makerlab.DAL;

namespace Makerlab.Tasks
{
    public class Fetch3DPrinter : ITask
    {
        public void Execute()
        {
            if (!MvcApplication.Redis.IsConnected) return;
            string machinesJson = MvcApplication.Redis.GetDatabase().StringGet("machines");
            dynamic machines = JsonConvert.DeserializeObject(machinesJson);

            foreach (var machine in machines)
            {
                string uuid = machine.uuid;

                if ((bool)machine.isConnected)
                {
                    var printer = PrinterManager.Read(uuid);
                    if (printer != null)
                    {
                        printer.LastSeen = machine.lastSeen;

                        PrinterManager.Update(printer);
                    }
                }
            }
            
            //var printer = DAL.PrinterManager.Read(3);
            //printer.LastSeen = DateTime.Now;
            //DAL.PrinterManager.Update(printer);
        }
    }
}