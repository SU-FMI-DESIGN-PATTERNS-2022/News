using AutoMapper;

using ApiArticle = NewsAPI.Models.Article;
using ApiSource = NewsAPI.Models.Source;
using EntityArticle = News.Repository.Entities.Article;
using EntitySource = News.Repository.Entities.Source;
namespace News.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            /*ApiArticle article=new ApiArticle();
            EntityArticle art;
            art = new EntityArticle
            {
                Source = null
            };

            ApiSource apiSrc= article.Source;
            EntitySource dtoSrc = new EntitySource()
            {
                Name = apiSrc.Name,
            };

            art.Source = dtoSrc;*/

            CreateMap<ApiSource, EntitySource>()
                .ForMember(dest => dest.Articles, act => act.Ignore());

            CreateMap<ApiArticle, EntityArticle>()
                .ForMember(dest => dest.Picture,
                act => act.MapFrom(src => src.UrlToImage))
                .ForMember(dest => dest.Summary,
                act => act.MapFrom(src => src.Description))
                .ForMember(dest => dest.Timestamp,
                act => act.MapFrom((src) => DateTime.Now.Ticks))
                .ForMember(dest => dest.Tags, act => act.Ignore())
                .ForMember(dest => dest.Source, act => act.MapFrom(src=>src.Source));

            
        }
    }
}
