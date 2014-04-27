using System.Collections.Generic;
using Homoiconicity.Sections;
using Homoiconicity.Specifications;

namespace Program
{
    public static class ResumeComposer
    {
        public static List<IResumeSection> ComposeBasicResume()
        {
            return new List<IResumeSection>()
                   {
                        new PersonalDetailsSection(),
                        new EducationSection(),
                        new CertificationSection(),
                        new EmploymentHistorySection(),
                   };
            
        }

        public static List<IResumeSection> ComposeResumeForTopSecretAgents()
        {
            return new List<IResumeSection>()
                   {
                        new PersonalDetailsSection(),
                        new EducationSection(),
                        new CertificationSection(),
                        new EmploymentHistorySection(),
            
                        new ConditionalSection(
                            new CitizenSecretSpecification(), 
                            new MembershipSection()),

                        new ConditionalSection(
                            new TopSecretSpecification(), 
                            new CompositeSection()
                            {
                                Sections = new List<IResumeSection>()
                                           {
                                               new TopSecretSection(),
                                               new CitizenSecretSection(),                                                    
                                   
                                           }
                            }),
                   };

        }
    }
}
