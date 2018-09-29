using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDemo.BusinessObject.DAL
{
    public class PatientContext : PatientDemoEntities1
    {
        //private static string CONNECTION_STRING = @"Database=CRPM-live;Server=fmdvCPMNG3DB01\ZONE01,3180;User ID=crpm_user;Password=crpm_user;Trusted_Connection=false";
        private static string CONNECTION_STRING = ReadConnectionString();

        public PatientContext() : base() { Database.Connection.ConnectionString = CONNECTION_STRING; }

        private static string ReadConnectionString()
        {
            try
            {
                ///read the config app settings from common file erpmConfig.config present in common folder.                
                var conectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=c:\users\cudupa\Source\Repos\PatientDemo\PatientDemo.BusinessObject\PatientDemo.mdf;Integrated Security=True";
                //conectionstring = ConfigurationSettings.AppSettings["ERPMEFConnectionString"].ToString();
                return conectionstring;
            }
            catch (System.Exception ex)
            {
                if (ex.InnerException != null) throw new Exception("Error while reading the EF connection string", ex.InnerException);
                else throw new Exception("Error while reading the EF connection string");
            }
        }

        public System.Data.Entity.DbSet<PatientDemo.BusinessObject.BusinessEntities.PatientRecord> PatientRecords { get; set; }
    }
}
