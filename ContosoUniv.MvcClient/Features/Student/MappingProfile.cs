namespace ContosoUniv.MvcClient.Features.Student
{
	using AutoMapper;
	using ContosoUniv.MvcClient.Models;

	public class MappingProfile : Profile
    {
		public MappingProfile()
		{
			CreateMap<Student, Edit.Command>().ReverseMap();
		}
    }
}
