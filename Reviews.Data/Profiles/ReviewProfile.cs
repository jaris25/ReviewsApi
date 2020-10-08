using AutoMapper;
using Reviews.Data.Entities;
using Reviews.Data.Models;

namespace Reviews.Data.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewForCreationDto, Review>();
            CreateMap<Review, ReviewDto>();
        }
    }
}
