namespace HospitalApp
{
    internal class Doctor : User, IUser
    {
        internal Doctor()
        {

        }
        internal void CreatePrescription(string text,Client client)
        {
            using (EFContext db = new EFContext())
            {
                db.Prescriptions.Add(new Prescription(this,text,client));
                db.SaveChanges();
            }
        }
    }


}
