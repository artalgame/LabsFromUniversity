using System.Linq;
using Entities;

namespace RepositoryInterfaces
{
    public interface IPresentationCRUD
    {
        IQueryable<Presentation> Presentations { get; }
        void CreatePresentation(Presentation presentation);
        void UpdatePresentation(Presentation presentation);
        void DeletePresentation(Presentation presentation);
    }
}
