using System.Linq;
using SimPressDomainModel.Entities;

namespace SimPressDomainModel.Interfaces
{
    public interface IPresentationCRUD
    {
        IQueryable<Presentation> Presentations { get; }
        void CreatePresentation(Presentation presentation);
        void UpdatePresentation(Presentation presentation);
        void DeletePresentation(Presentation presentation);
    }
}
