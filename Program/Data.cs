using System;
using System.Collections.Generic;
using Homoiconicity.Data;

namespace Program
{
    public class Data
    {
        public static ResumeData JamesBond = new ResumeData()
                {
                    ReaderClearanceLevel = ClearanceLevel.TopSecret,
                    FirstName = "James",
                    LastName = "Bond",
                    Telephone = "007",
                    Email = "Jamed@bond.com",
                    ExperienceYears = 57,
                    ResidenceCountry = "UK",
                    EmploymentHistories = new List<EmploymentHistory>()
                                          {
                                              new EmploymentHistory()
                                              {
                                                  EmployerName = "MI6",
                                                  Position = "Junior Special Agent",
                                                  StartDate = new DateTime(1968, 1, 1),
                                                  EndDate = new DateTime(1972, 12, 31),
                                                  Description = "Just learning the stuff",
                                              },
                                              new EmploymentHistory()
                                              {
                                                  EmployerName = "MI6",
                                                  Position = "Special Agent",
                                                  StartDate = new DateTime(1973, 1, 1),
                                                  EndDate = new DateTime(1980, 12, 31),
                                                  Description = "Not being the best, but good enough to mix with other agents",
                                              },
                                              new EmploymentHistory()
                                              {
                                                  EmployerName = "MI6",
                                                  Position = "Super Agent",
                                                  StartDate = new DateTime(1981, 1, 1),
                                                  EndDate = new DateTime(1990, 12, 31),
                                                  Description = "Super star of the agents",
                                              },
                                          },
                    Educations = new List<Education>()
                             {
                                 new Education()
                                 {
                                     Establishment = "MI6 Training School",
                                     AcheivementLevel = "Passed",
                                     CourseName = "Social Engineering",
                                     DegreeLevel = DegreeLevel.HighSchool,
                                     StartDate = new DateTime(1968, 1, 1),
                                     EndDate = new DateTime(1968, 5, 5),
                                 },
                                 new Education()
                                 {
                                     Establishment = "MI6 Training School",
                                     AcheivementLevel = "Never ending",
                                     CourseName = "Martial Arts",
                                     DegreeLevel = DegreeLevel.HighSchool,
                                     StartDate = new DateTime(1968, 1, 1),
                                     EndDate = new DateTime(2014, 1, 1),
                                 },
                                 new Education()
                                 {
                                     Establishment = "MI6 Training School",
                                     AcheivementLevel = "2.1",
                                     CourseName = "Algebra and Applcation Math",
                                     DegreeLevel = DegreeLevel.BSc,
                                     StartDate = new DateTime(1970, 9, 1),
                                     EndDate = new DateTime(1974, 6, 1),
                                 },
                             },
                    Certifications = new List<Certification>()
                                 {
                                     new Certification()
                                     {
                                         CertificationName = "First Aid",
                                         StartDate = new DateTime(1970, 1, 1),
                                         ExpiryDate = new DateTime(1974, 1, 1),
                                     },
                                     new Certification()
                                     {
                                         CertificationName = "Offshore Survival",
                                         StartDate = new DateTime(1973, 1, 1),
                                         ExpiryDate = new DateTime(1979, 1, 1),
                                     },
                                 },
                    OrganisationsMemberships = new List<OrganisationsMembership>()
                                           {
                                               new OrganisationsMembership()
                                               {
                                                   OrganisationName = "Specials Agents Worfkorce",
                                                   MembershipName = "Senior Agent"
                                               },
                                               new OrganisationsMembership()
                                               {
                                                   OrganisationName = "Union of MI6 Veterans",
                                                   MembershipName = "none"
                                               }
                                           },
                    TopSecrets = new List<StateSecret>()
                             {
                                 new StateSecret()
                                 {
                                     SecretInformation = "Rock'n'Roll is dead, long live Rock'n'Roll",
                                 },
                                 new StateSecret()
                                 {
                                     SecretInformation = "The state secrets privilege is an evidentiary rule created by United States legal precedent. Application of the privilege results in exclusion of evidence from a legal case based solely on affidavits submitted by the government stating that court proceedings might disclose sensitive information which might endanger national security. United States v. Reynolds, which involved military secrets, was the first case that saw formal recognition of the privilege."
                                 }
                             },
                    CitizenSecrets = new List<StateSecret>()
                                 {
                                     new StateSecret()
                                     {
                                         SecretInformation = "Lawrence’s first task at his new job would be an easy one. “All you gotta do is carry this across the finish line. It’s practically done already,” Chris the Costly Contractor informed him. Costly Chris was nearing the end of his contract and the company didn’t want to keep paying his jacked-up rates. That’s where Lawrence, the cheaper, full-time alternative to Chris, came in. “But, there are some recent change requests that we need to do. You’ll have the pleasure of talking to Becky about that,” Chris said with a sly grin. Poster of Alexander Crystal Seer The software was a simple CRM with a PHP front end. It was a straight-forward MVC application with a slew of stored procedures responsible for managing the data. Lawrence’s group worked on the UI layer. Shepherd, guru, and leader of the UI effort was Becky, the designer. Becky’s background was in graphic design for print, and someone up the management tree had decided that design was design, and appointed her head of the user interface and experience group.",
                                     },
                                     new StateSecret()
                                     {
                                         SecretInformation = "Jonathan D. was the system administrator for a school nestled in a war-ravaged city somewhere in the middle of the desert. What with bombings here, explosions there, and the odd RPG whizzing by, dealing with a converted bathroom as an office/datacenter just didn't seem to be all that big of a deal. The school had roughly 100 computers split between two buildings, along with the laptops everyone used. His office, ...erm... converted bathroom housed all of the servers, and the main computer room for the high school/middle school (grades 6 and up) building was located right outside the door. One morning, as the sun came up over the desert, he found that he was unable to connect to the internet. After trying to ping the gateway and getting no response, Jonathan tried pinging the data servers. Nada. When he did a visual inspection of the servers which revealed that they looked fine, he thought that maybe the problem was with his desktop. While it was rebooting, he heard the volume of the students in the computer room explode. The computers in there were no longer able to see the server and had hung.",
                                     }
                                 },
                };
    }
}
