using AutoMapper;
using TeamTaskManager.Domain;
using TeamTaskManager.Dtos;

public class TaskProfile : Profile
{
    public TaskProfile()
    {

        CreateMap<CreateTeamMemberDto, TeamMember>()
           .ForMember(dest => dest.Tasks, opt => opt.Ignore())
           .ReverseMap();

        CreateMap<TeamMember, TeamMemberResponseDto>();
        CreateMap<CreateTaskDto, TaskItem>();

        CreateMap<TaskItem, TaskResponseDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()));

        CreateMap<TeamMember, TeamMemberDto>();


    }
}

