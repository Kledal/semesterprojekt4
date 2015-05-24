using System;
using FluentScheduler;
using Makerlab.DAL;
using Newtonsoft.Json;

namespace Makerlab.Tasks
{
    public class Fetch3DPrinter : ITask
    {
        public void Execute()
        {
            try
            {
                if (!MvcApplication.Redis.IsConnected) return;
                string machinesJson = MvcApplication.Redis.GetDatabase().StringGet("machines");
                dynamic machines = JsonConvert.DeserializeObject(machinesJson);

                foreach (var machine in machines)
                {
                    string uuid = machine.uuid;
                    if (!(bool) machine.isConnected) continue;

                    var printer = PrinterManager.Read(uuid);
                    if (printer == null) continue;

                    var info = machine.info;
                    var temperatures = info.temperatures;
                    var status = machine.info.status;

                    printer.LastSeen = machine.lastSeen;
                    printer.BedTemperature = (int)temperatures.bed[0];
                    printer.NozzleTemperature = (int)temperatures.nozzle[0];
                    printer.Printing = (bool)status.printing;
                    printer.CurrentLine = (int) status.current_line;

                    PrinterManager.Update(printer);
                }

                string camsJson = MvcApplication.Redis.GetDatabase().StringGet("cams");
                dynamic cams = JsonConvert.DeserializeObject(camsJson);

                foreach (var cam in cams)
                {
                    string uuid = cam.uuid;
                    var printer = PrinterManager.Read(uuid);

                    printer.LastFrame = cam.lastFrame;
                    PrinterManager.Update(printer);
                }
            }
            catch (Exception e)
            {
                
            }
        }
    }
}