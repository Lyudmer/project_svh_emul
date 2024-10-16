using AutoMapper;
using ClientSVH.Core.Models;
using ClientSVH.DataAccess.Entities;

namespace EmulatorSVH.DataAccess.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DocumentEntity, Document>().ReverseMap();
            CreateMap<Document, DocumentEntity>().ReverseMap();
            CreateMap<PackageEntity, Package>().ReverseMap();

            CreateMap<Package, PackageEntity>().ReverseMap();



        }
    }
}
