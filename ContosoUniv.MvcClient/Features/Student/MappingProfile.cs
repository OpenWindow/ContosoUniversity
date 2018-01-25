namespace ContosoUniv.MvcClient.Features.Student
{
  using AutoMapper;
  using ContosoUniv.MvcClient.Models;

  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Student, Details.Model>();
      CreateMap<Student, Details.Model.Enrollment>();
      CreateMap<Student, Edit.Command>().ReverseMap();
    }
  }
}
