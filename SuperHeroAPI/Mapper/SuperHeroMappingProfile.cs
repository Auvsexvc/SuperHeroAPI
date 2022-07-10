using AutoMapper;
using SuperHeroAPI.Entities;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Mapper
{
    public class SuperHeroMappingProfile : Profile
    {
        public SuperHeroMappingProfile()
        {
            CreateMap<SuperHero, SuperHeroDto>()
                .ForMember(m => m.Strength, c => c.MapFrom(s => s.BaseStrength + s.SuperPowers.Select(p => p.AdditionToSuperHeroStrength).Sum()));

            CreateMap<SuperPower, SuperPowerDto>()
                .ForMember(m=>m.UsedBySuperHeroes, c=> c.MapFrom(s=>string.Join(", ",s.SuperHeroes.Select(s=>s.Name))));

            CreateMap<CreateSuperHeroDto, SuperHero>();

            CreateMap<UpdateSuperHeroDto, SuperHero>();

            CreateMap<CreateSuperPowerDto, SuperPower>();

            CreateMap<UpdateSuperPowerDto, SuperPower>();
        }
    }
}
