using AutoMapper;
using VolunteerConnect.Application.Features.Categories.Commands.CreateCategory;
using VolunteerConnect.Application.Features.Categories.Queries.GetCategoriesList;
using VolunteerConnect.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using VolunteerConnect.Application.Features.Events.Commands.CreateEvent;
using VolunteerConnect.Application.Features.Events.Commands.UpdateEvent;
using VolunteerConnect.Application.Features.Events.Queries.GetEventDetail;
using VolunteerConnect.Application.Features.Events.Queries.GetEventsList;
using VolunteerConnect.Application.Features.ParticipationOrder.Commands.CreateParticipationOrder;
using VolunteerConnect.Application.Features.ParticipationOrder.Commands.UpdateParticipationOrder;
using VolunteerConnect.Application.Features.ParticipationOrder.Queries;
using VolunteerConnect.Application.Features.ParticipationOrder.Queries.GetAllParticipationOrders;
using VolunteerConnect.Application.Features.ParticipationOrder.Queries.GetParticipationsByUser;
using VolunteerConnect.Domain.Entities;

namespace VolunteerConnect.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventListVm>().ReverseMap();
            CreateMap<Event, EventDetailVm>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryListVm>().ReverseMap();
            CreateMap<Category, CategoryEventListVm>().ReverseMap();
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, CategoryEventListVm>()
            .ForMember(dest => dest.CategoryEventsList,
                       opt => opt.MapFrom(src => src.Events));
            CreateMap<Event, CategoryEventDto>().ReverseMap();

            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, CategoryEventDto>().ReverseMap();

            CreateMap<ParticipationOrder, ParticipationOrderListByUserVm>().ReverseMap();
            CreateMap<ParticipationOrder, ParticipationStatusDto>().ReverseMap();
            CreateMap<ParticipationOrder, CategoryEventDto>().ReverseMap(); 
            CreateMap<ParticipationOrder, GetAllParticipationOrdersVm>().ReverseMap();
            CreateMap<ParticipationOrder, CreateParticipationOrderCommand>().ReverseMap();
            CreateMap<ParticipationOrder, UpdateParticipationOrderCommand>().ReverseMap();



        }
    }
}
