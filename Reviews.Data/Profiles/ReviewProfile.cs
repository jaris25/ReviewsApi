using AutoMapper;
using Reviews.Data.Entities;
using Reviews.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reviews.Data.Profiles
{
    public class ReviewProfile: Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewForCreationDto, Review>();
        }
    }
}
