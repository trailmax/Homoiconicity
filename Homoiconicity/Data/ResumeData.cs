using System;
using System.Collections.Generic;

namespace Homoiconicity.Data
{
    public class ResumeData
    {
        public string DocumentTitle
        {
            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Telephone { get; set; }
        public String Email { get; set; }
        public int ExperienceYears { get; set; }
        public string ResidenceCountry { get; set; }

        public List<EmploymentHistory> EmploymentHistories { get; set; }

        public List<Education> Educations { get; set; }

        public List<Certification> Certifications { get; set; }

        public List<OrganisationsMembership> OrganisationsMemberships { get; set; }

        public List<StateSecret> TopSecrets { get; set; }
        public List<StateSecret> CitizenSecrets { get; set; }
        public ClearanceLevel ReaderClearanceLevel { get; set; }
    }


    public class StateSecret
    {
        public String SecretInformation { get; set; }
    }


    public class EmploymentHistory
    {
        public String EmployerName { get; set; }
        public String Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Description { get; set; }
    }


    public class Education
    {
        public String Establishment { get; set; }
        public String CourseName { get; set; }
        public String AcheivementLevel { get; set; }
        public DegreeLevel DegreeLevel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public enum DegreeLevel
    {
        HighSchool,
        BSc,
        MSc,
        MSEng,
        PHd,
    }

    public class Certification
    {
        public String CertificationName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }


    public class OrganisationsMembership
    {
        public String OrganisationName { get; set; }
        public String MembershipName { get; set; }
    }
}
