namespace Inventory_Management.Features.Common.BackGround_jobs
{
    public class SampleJob
    {
        public void ExecuteJob()
        {
            // Job logic here
            Console.WriteLine("Executing background job at: " + DateTime.Now);
        }
    }
}
