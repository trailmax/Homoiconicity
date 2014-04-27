using System;
using System.Collections.Generic;
using Homoiconicity.Data;
using Homoiconicity.Elements;

namespace Homoiconicity.Sections
{
    [Serializable]
    public class MembershipSection : IResumeSection
    {
        public IEnumerable<IResumeElement> ProduceElements(ResumeData data)
        {
            yield return new ResumeParagraph("Membership").SetSectionHeaderStyle();

            var bulletedList = new ResumeBulletedList();

            foreach (var membership in data.OrganisationsMemberships)
            {
                bulletedList.AddParagraph(String.Format("{0} - {1}", membership.OrganisationName, membership.MembershipName));
            }

            yield return bulletedList;
        }
    }
}
