using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    internal class Prescription
    {
        internal int DoctorId { get; set; }
        internal Doctor Creator { get; set; }
        internal string Text { get; set; }
        internal int ClientId { get; set; }
        internal Client Receiver { get; set; }
        internal Prescription()
        {}
        internal Prescription(Doctor doctor, string text, Client recevier)
        { 
            Creator = doctor;
            Text = text;
            Receiver = recevier;
        }
    }
}
