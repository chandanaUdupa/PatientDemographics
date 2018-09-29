using PatientDemo.BusinessObject.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientDemo.BusinessObject.Adapter;

namespace PatientDemo.BusinessObject.DAL
{
    public class PatientDAL
    {
        /// <summary>
        /// To fetch the Patient's core details
        /// from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>PatientRecord</returns>
        public static PatientRecord GetPatientCoreDetails(int id)
        {
            using (PatientContext entities = new PatientContext())
            {
                var patient = (from m in entities.Patients
                               where m.Id == id
                               select new PatientRecord
                               {
                                   Id = m.Id,
                                   Forenames = m.Forenames,
                                   Surname = m.Surname,
                                   Date_of_Birth = m.Date_of_Birth,
                                   Gender = m.Gender
                               }).FirstOrDefault();

                return patient;
            }
        }

        /// <summary>
        /// To fetch the Patient's Contact details
        /// from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List<PatientElectronicAddress></returns>
        public static List<PatientElectronicAddress> GetPatientContactDetails(int id)
        {
            using (PatientContext entities = new PatientContext())
            {
                List<PatientElectronicAddress> patientElecAddressCollection = new List<PatientElectronicAddress>();
                var patientElecAddressCollectionFromDB = (from patientTelNumbers in entities.PatientElectronicAddresses
                                                          where patientTelNumbers.PatientId == id
                                                          select new 
                                                            {
                                                                ElecType = patientTelNumbers.ElecType,
                                                                Telephone_numbers = patientTelNumbers.Telephone_numbers
                                                            }
                                                          ).ToList();
                foreach (var item in patientElecAddressCollectionFromDB)
                {
                    PatientElectronicAddress patientElectronicAddress = new PatientElectronicAddress();
                    patientElectronicAddress.ElecType = item.ElecType;
                    patientElectronicAddress.Telephone_numbers = item.Telephone_numbers;
                    patientElecAddressCollection.Add(patientElectronicAddress);
                }

                return patientElecAddressCollection;
            }
        }

        /// <summary>
        /// To create a new patient record
        /// in the database
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public static int SetPatientDetails(Patient patient)
        {
            using (PatientContext entities = new PatientContext())
            {
                entities.Patients.Add(patient);
                try
                {
                    entities.SaveChanges();
                    return patient.Id;
                }
                catch (DbUpdateException)
                {
                    if (PatientExists(patient.Id))
                    {
                        throw new Exception(string.Format("Patient already exists"));
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// To save patient details
        /// in database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patient"></param>
        /// <returns>int</returns>
        public static int SavePatientCDetails(int id, PatientRecord patient)
        {
            using (PatientContext entities = new PatientContext())
            {
                try
                {
                    if (patient.TelephoneNumbers != null) { 
                    foreach (PatientElectronicAddressCustom item in patient.TelephoneNumbers)
                    {
                        int count = entities.PatientElectronicAddresses.Count(x => x.PatientId == id && x.ElecType == item.ElectronicAddressType);
                        if (count == 0)
                        {
                            PatientElectronicAddress patientElectronicAddress = new PatientElectronicAddress();
                            patientElectronicAddress.ElecType = item.ElectronicAddressType;
                            patientElectronicAddress.Telephone_numbers = item.Telephone_number;
                            patientElectronicAddress.PatientId = id;
                            entities.PatientElectronicAddresses.Add(patientElectronicAddress);
                            patient.TelephoneNumbers.Remove(item);
                        }
                        else
                        {
                            PatientElectronicAddress pea = new PatientElectronicAddress { ElecType = item.ElectronicAddressType, PatientId = id, Telephone_numbers = item.Telephone_number };
                            pea.Id = entities.PatientElectronicAddresses.Where(x => x.PatientId == id && x.ElecType == item.ElectronicAddressType).Select(x => x.Id).FirstOrDefault();
                            entities.Entry(pea).State = EntityState.Detached;
                            entities.Entry(pea).State = EntityState.Modified;
                        }
                    }
                    entities.SaveChanges();
                    }
                    patient.TelephoneNumbers = null;
                    entities.Entry(PatientBusinessEntityToDBModel.ConvertPatientRecordToPatient(patient, id)).State = EntityState.Modified;

                    entities.SaveChanges();
                    return 1;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(id))
                    {
                        return 0;
                    }
                    else
                    {
                        throw;
                    }
                }

            }
        }

        /// <summary>
        /// To check if Patient record exists
        /// in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        private static bool PatientExists(int id)
        {
            using (PatientDemoEntities1 entities = new PatientDemoEntities1())
            {
                {
                    return entities.Patients.Count(e => e.Id == id) > 0;
                }
            }
        }
    }
}
