using Homoiconicity.Data;

namespace Homoiconicity.Specifications
{
    public interface IResumeSectionSpecification
    {
        bool IsSatisfiedBy(ResumeData data);
    }
}
